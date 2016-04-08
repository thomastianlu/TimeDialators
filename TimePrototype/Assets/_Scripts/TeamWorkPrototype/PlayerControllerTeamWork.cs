using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum PlayerState
{
    PlayerMode,
    FollowMode,
    PlaybackMode,
    RecordMode,
    IdleMode
}

public class PlayerControllerTeamWork : MonoBehaviour {

    [SerializeField]
    private PlayerState _playerMode;

    [SerializeField]
    private PlayerManagerScript _playerManager;

    [SerializeField]
    private GameObject _recordArt;

    [SerializeField]
    private PlaybackTimer _playbackTimerGlobal;

    [SerializeField]
    private Transform _followPoint;

    [SerializeField]
    private Transform _playerArt;

    [SerializeField]
    private Transform _myFollowPoint;

    private Rigidbody2D _rigidBody;

    [SerializeField]
    private bool _isGrounded;
    private bool _jumpOnce = false;

    [SerializeField]
    private bool _needJump = false;

    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _moveSpeed;
    private float _moveSpeedReset;

    [SerializeField]
    private List<PositionPair> _positionRecorder = new List<PositionPair>();

    [SerializeField]
    private PositionPair[] _followPosition;

    [SerializeField]
    private float _timer = 0f;

    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private float _thresholdFollowPosition;

    [SerializeField]
    private float _lagTimerTreshold = 10f;
    private float _lagTimer = 0f;

    private bool _APressOnce = false;
    private bool _DPressOnce = false;

    [SerializeField]
    private bool _recordMode = false;
    [SerializeField]
    private bool _playBackMode = false;
    private float _playBackTimer = 0f;
    private bool _finishPlayback = false;

    [SerializeField]
    private bool _isMainCharacter;

    [SerializeField]
    private bool _setRecordOnce = false;

    [SerializeField]
    private bool _enableRecording = false;

    private int _positionRecorderIterator = 0;

    private float _pressSpaceTimer = 0.2f;
    private float _pressSpaceTimerReset;
    private float _pressShiftTimer = 0.2f;
    private float _pressShiftTimerReset;
    
    // Use this for initialization
    void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
        _moveSpeedReset = _moveSpeed;
        _pressSpaceTimerReset = _pressSpaceTimer;
        _pressShiftTimerReset = _pressShiftTimer;
	}
	
    public void SetGround(bool grounded) {
        _isGrounded = grounded;
    }

    public bool GetPlayBackMode()
    {
        return _playBackMode;
    }

    void ClearLog()
    {
        _positionRecorder.Clear();
    }

	// Update is called once per frame
	void FixedUpdate () {
        switch (_playerMode)
        {
            case PlayerState.PlayerMode:
                ManageInputs();
                break;
            case PlayerState.FollowMode:
                FollowMode();
                break;
            case PlayerState.PlaybackMode:
                PlayBackMode();
                break;
            case PlayerState.RecordMode:
                ManageInputs();
                RecordMode();
                break;
        }
    }

    public float GetRecordedTime() {
        if (_positionRecorder.Count > 0) {
            return _positionRecorder[_positionRecorder.Count - 1].TimeStamp;
        }
        return 0f;
    }

    public void ResetTimer() {
        _timer = 0f;
    }

    void ManageInputs() {

        _pressSpaceTimer -= Time.deltaTime;
        _pressShiftTimer -= Time.deltaTime;

        _timer += Time.deltaTime;

        if (!_isGrounded) {
            _anim.CrossFade("JumpHold", 0.1f);
        }
        else if (_isGrounded && !_APressOnce && !_DPressOnce) {
            _anim.Play("Idle");
        }

        if (_enableRecording)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && _pressShiftTimer < 0)
            {
                _pressShiftTimer = _pressShiftTimerReset;

                if (_playBackMode)
                {
                    _playerMode = PlayerState.PlayerMode;
                    _playBackMode = false;
                    _rigidBody.gravityScale = 1f;
                }
                else
                {
                    _playerMode = PlayerState.PlaybackMode;
                    _playBackMode = true;
                    _playbackTimerGlobal.ResetTimer();
                    _rigidBody.gravityScale = 0f;
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                _pressSpaceTimer = _pressSpaceTimerReset;

                if (!_setRecordOnce) {
                    ClearLog();
                    SetTimerRecordMode();
                    _setRecordOnce = true;
                }

                _recordMode = true;
                _playerMode = PlayerState.RecordMode;
                _playerManager.SetRecordMode(true);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _playerMode = PlayerState.PlayerMode;
                _recordMode = false;
                _setRecordOnce = false;
                _playerManager.SetRecordMode(false);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * _moveSpeed;
            transform.localScale = new Vector3(-1f, 1f, 1f);
            _anim.Play("Walk");
            if (!_APressOnce)
            {
                _APressOnce = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            _anim.Play("Idle");
        }

        if (Input.GetKeyDown(KeyCode.W) && _isGrounded == true)
        {
            if (!_jumpOnce) { 
                _rigidBody.AddForce(Vector2.up * _jumpForce);
                _isGrounded = true;
                _jumpOnce = true;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            _jumpOnce = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * _moveSpeed;
            transform.localScale = new Vector3(1f, 1f, 1f);
            _anim.Play("Walk");
            if (!_DPressOnce)
            {
                _DPressOnce = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.D)) {
            _anim.Play("Idle");
        }
    }

    void FollowMode()
    {
        if (Vector3.Distance(_followPoint.position, transform.position) > _thresholdFollowPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _followPoint.position, Time.deltaTime * 20);
            _lagTimer += Time.deltaTime;

            if (_lagTimer > _lagTimerTreshold)
            {
                transform.position = _followPoint.position;
                _lagTimer = 0;
            }


            if (_followPoint.position.x - transform.position.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

        }

        if (_needJump && _isGrounded) {
            _rigidBody.AddForce(Vector2.up * _jumpForce);
            _isGrounded = false;
        }
    }

    void PlayBackMode()
    {
        _recordArt.SetActive(true);
        if (_positionRecorder.Count > 0 && !_finishPlayback) {

            if (_playbackTimerGlobal.GetCurrentTime() > _positionRecorder[_positionRecorderIterator].TimeStamp)
            {
                transform.position = _positionRecorder[_positionRecorderIterator].Position;
                _positionRecorderIterator++;

                if (_positionRecorderIterator > _positionRecorder.Count - 1)
                {
                    _finishPlayback = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _isMainCharacter)
        {
            _playerMode = PlayerState.PlayerMode;
            _playBackMode = false;
            _recordArt.SetActive(false);
        }
    }

    void SetTimerRecordMode()
    {
        _timer = _playbackTimerGlobal.GetCurrentTime();
    }

    void RecordMode()
    {
        _timer += Time.deltaTime;
        _positionRecorder.Add(new PositionPair { TimeStamp = _timer, Position = transform.position });
    }

    public void SetPlayerMode(PlayerState newMode)
    {
        if (_playerMode != PlayerState.PlaybackMode) { 
            _playerMode = newMode;

            if (newMode == PlayerState.FollowMode)
            {
                gameObject.layer = 15;
            }
            else
            {
                gameObject.layer = 9;
            }
        }
    }

    public PlayerState ReturnPlayerState()
    {
        return _playerMode;
    }

    public void ResetPlayBackMode()
    {
        _positionRecorderIterator = 0;
        _playBackTimer = 0f;
        _finishPlayback = false;
    }

    public bool GetPlayBackState()
    {
        return _finishPlayback;
    }

    public void SetMainCharacter(bool set)
    {
        _isMainCharacter = set;
    }

    public void SetFollowPoint (Transform FollowPoint)
    {
        _followPoint = FollowPoint;
    }

    public Transform ReturnFollowPoint()
    {
        return _myFollowPoint;
    }

    public void changeJumpState(bool jumpState)
    {
        _needJump = jumpState;
    }
}
