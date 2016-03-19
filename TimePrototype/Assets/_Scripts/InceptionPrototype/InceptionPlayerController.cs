using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InceptionPlayerController : MonoBehaviour {

    private Rigidbody2D _rigidBody;

    [SerializeField]
    private bool _isGrounded;

    [SerializeField]
    private CameraFollowScript _cameraFollowScript;

    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _moveSpeed;
    private float _moveSpeedReset;

    [SerializeField]
    private GameObject[] _inceptionPlayerList;

    [SerializeField]
    private SpriteRenderer[] _currentObjArt;

    Dictionary<int, PositionPair> _positionLog = new Dictionary<int, PositionPair>();

    private int _positionLogIterator = 0;
    private float _playBackTimer = 0f;

    [SerializeField]
    private bool _playBackMode;

    [SerializeField]
    private float _timer = 0f;

    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private GameObject _ghostPrefab;

    [SerializeField]
    private Transform _currentRespawnPosition;

    [SerializeField]
    private InceptionRecorder _generationChecker;

    private bool _APressOnce = false;
    private bool _DPressOnce = false;
    
    private bool _spawnOnce = false;

    [SerializeField]
    private int _generationNumber = 0;

    [SerializeField]
    private int _currentGeneration;

    private bool _finishedRunThrough = false;


	// Use this for initialization
	void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
        _moveSpeedReset = _moveSpeed;
	}
	
    public void SetGround(bool grounded) {
        _isGrounded = grounded;
    }

	// Update is called once per frame
	void FixedUpdate () {

        if (!_playBackMode) { 
            ManageInputs();
        }
        else
        {
            PlayBackMode();
        }
    }

    public void ResetTimer() {
        _timer = 0f;
    }

    public void SetRespawnPosition(Transform currentPosition)
    {
        _currentRespawnPosition = currentPosition;
    }
    
    public void ClearLog()
    {
        _positionLog.Clear();
        _playBackTimer = 0f;
        _positionLogIterator = 0;
        _timer = 0;
        transform.position = _currentRespawnPosition.position;
    }

    public void SetAsMainPlayer()
    {
        _playBackMode = false;
        _cameraFollowScript.SetPlayerFocus(transform);
        _rigidBody.isKinematic = false;

        gameObject.tag = "Player";
        gameObject.layer = 9;

        foreach (GameObject x in _inceptionPlayerList)
        {
            if (x.activeSelf)
            {
                x.GetComponent<InceptionPlayerController>().SetColorUp();
            }
        }

    }

    public void ResetPlayBackMode()
    {
        _playBackTimer = 0f;
        _positionLogIterator = 0;
        _finishedRunThrough = false;
    }

    void ManageInputs() {

        _timer += Time.deltaTime;

        _positionLog[_positionLogIterator] = new PositionPair { Position = transform.position, TimeStamp = _timer };

        _positionLogIterator++;

        if (Input.GetKeyDown(KeyCode.W) && _isGrounded == true)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce);
            _isGrounded = false;
        }

        if (!_isGrounded) {
            _anim.CrossFade("JumpHold", 0.1f);
        }
        else if (_isGrounded && !_APressOnce && !_DPressOnce) {
            _anim.Play("Idle");
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * _moveSpeed;
            transform.localScale = new Vector3(-1f, 1f, 1f);
            _anim.Play("Walk");

            if (!_APressOnce) { 
                _APressOnce = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            _APressOnce = false;
            _anim.Play("Idle");
        }

        if (Input.GetKey(KeyCode.S))
        {
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
        }

        // ---------------------------- SPAWN NEW PERSON -------------------------------- //

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (!_spawnOnce && _currentGeneration < 3) {
                SetInceptionLevelDown();
            }
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            _spawnOnce = false;
        }
        
        // ---------------------------- SPAWN NEW PERSON -------------------------------- //

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
            _DPressOnce = false;
            _anim.Play("Idle");
        }
    }

    void SetInceptionLevelDown()
    {
        transform.position = _currentRespawnPosition.position;
        _currentGeneration++;

        foreach (GameObject x in _inceptionPlayerList)
        {
            if (x.activeSelf)
            {
                x.transform.position = _currentRespawnPosition.position;
                x.GetComponent<InceptionPlayerController>().ResetPlayBackMode();
                x.GetComponent<InceptionPlayerController>().SetColorDown();
            }
        }

        _generationChecker.IncreaseGeneration();

        _inceptionPlayerList[_generationChecker.GetGeneration()].SetActive(true);
        gameObject.tag = "DummyPlayer";
        gameObject.layer = 8;
        _rigidBody.isKinematic = true;
        _cameraFollowScript.SetPlayerFocus(_inceptionPlayerList[_generationChecker.GetGeneration()].transform);


        ResetPlayBackMode();
        _playBackMode = true;
        _spawnOnce = true;
    }

    public void SetColorDown()
    {
        foreach (SpriteRenderer x in _currentObjArt)
        {
            Color currentColor = x.color;
            currentColor -= new Color(0.25f, 0.25f, 0.25f, 0f);
            x.color = currentColor;
        }
    }

    public void SetColorUp()
    {
        foreach (SpriteRenderer x in _currentObjArt)
        {
            Color currentColor = x.color;
            currentColor += new Color(0.25f, 0.25f, 0.25f, 0f);
            x.color = currentColor;
        }
    }

    void PlayBackMode()
    {
        Debug.Log(_currentObjArt[0].color.r * 2 / 3);
        _playBackTimer += (Time.deltaTime * _currentObjArt[0].color.r * 2/3);

        if (_positionLogIterator < _positionLog.Count - 1)
        {
            if (_positionLog[_positionLogIterator].TimeStamp < _playBackTimer)
            {
                transform.position = _positionLog[_positionLogIterator].Position;
                _positionLogIterator++;
            }
        }
        else
        {
            _inceptionPlayerList[_generationChecker.GetGeneration()].GetComponent<InceptionPlayerController>().ClearLog();
            _inceptionPlayerList[_generationChecker.GetGeneration()].SetActive(false);
            _currentGeneration--;
            _generationChecker.DecreaseGeneration();
            SetAsMainPlayer();
        }
    }
}
