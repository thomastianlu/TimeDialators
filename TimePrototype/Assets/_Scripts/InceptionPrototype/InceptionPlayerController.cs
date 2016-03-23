using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InceptionPlayerController : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private GameObject _groundChecker;

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
    [SerializeField]
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
    
    [SerializeField]
    private bool _spawnOnce = false;

    [SerializeField]
    private int _generationNumber = 0;

    [SerializeField]
    private int _currentGeneration;

    [SerializeField]
    private Transform _timerObj;
    private float _timerObjCurrentSize;
    private float _timerObjCurrentHeight;
    private float _timerObjCurrentDepth;

    private float _delaySpaceTime = 0.5f;

    [SerializeField]
    private GameObject _robotHat;

    private bool _finishedRunThrough = false;

    [SerializeField]
    private float depthSpeed = 1;

    [SerializeField]
    private Color _playerColor;

	// Use this for initialization
	void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
        _moveSpeedReset = _moveSpeed;
        _timerObjCurrentSize = _timerObj.localScale.x;
        _timerObjCurrentHeight = _timerObj.localScale.y;
        _timerObjCurrentDepth = _timerObj.localScale.z;

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
        //Debug.Log("IAMCLEARINGLOG " + gameObject.name);
        _playBackTimer = 0f;
        _positionLogIterator = 0;
        _timer = 0;
    }

    public void ResetToRespawnPosition()
    {
        transform.position = _currentRespawnPosition.position;
        _delaySpaceTime = 0.5f;
    }

    public void SetAsMainPlayer()
    {
        _playBackMode = false;
        _cameraFollowScript.SetPlayerFocus(transform);
        _rigidBody.isKinematic = false;

        gameObject.tag = "Player";
        _groundChecker.tag = "GroundChecker";
        gameObject.layer = 9;

        foreach (GameObject x in _inceptionPlayerList)
        {
            if (x.activeSelf)
            {
                x.GetComponent<InceptionPlayerController>().SetColorUp();
            }
        }

        foreach (SpriteRenderer x in _currentObjArt)
        {
            Color currentColor = x.color;
            currentColor = new Color (1f, 1f, 1f);
            x.color = currentColor;
        }

        _robotHat.SetActive(false);

    }

    public void ResetPlayBackMode()
    {
        _playBackTimer = 0f;
        _positionLogIterator = 0;
        _finishedRunThrough = false;
    }

    void ManageInputs() {
        
        _timerObj.gameObject.SetActive(false);
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


        // ---------------------------- SPAWN NEW PERSON -------------------------------- //

        _delaySpaceTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (!_spawnOnce && _currentGeneration < 3 && _delaySpaceTime < 0) {
                SetInceptionLevelDown();
            }
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
        
        _playBackMode = true;
        _spawnOnce = true;

        _inceptionPlayerList[_generationChecker.GetGeneration()].SetActive(true);
        gameObject.tag = "DummyPlayer";
        _groundChecker.tag = "DummyGroundChecker";
        gameObject.layer = 8;
        _rigidBody.isKinematic = true;
        _cameraFollowScript.SetPlayerFocus(_inceptionPlayerList[_generationChecker.GetGeneration()].transform);

        _robotHat.SetActive(true);
    }

    public void SetGravity (float Gravity)
    {
        _rigidBody.gravityScale = Gravity;
    }

    public void ResetInception()
    {
        int InceptionIterator = _inceptionPlayerList.Length - 1;
        while (InceptionIterator >= 0)
        {
            Debug.Log("RESET: " + _inceptionPlayerList[InceptionIterator].gameObject.name);
            _inceptionPlayerList[InceptionIterator].GetComponent<InceptionPlayerController>().ResetToRespawnPosition();
            _inceptionPlayerList[InceptionIterator].GetComponent<InceptionPlayerController>().SetGravity(1);
            _inceptionPlayerList[InceptionIterator].transform.SetParent(GameObject.Find("Players").transform);
            _inceptionPlayerList[InceptionIterator].GetComponent<InceptionPlayerController>().SetEndIterator();
            InceptionIterator--;
        }
        
    }

    public void SetColorDown()
    {
        foreach (SpriteRenderer x in _currentObjArt)
        {
            Color currentColor = x.color;
            currentColor = _playerColor;
            x.color = currentColor;
        }
        depthSpeed = depthSpeed/2;
    }

    public void SetColorUp()
    {
        depthSpeed = depthSpeed * 2;
    }
    
    public void SetEndIterator()
    {
        _positionLogIterator = _positionLog.Count - 1;

        _inceptionPlayerList[_generationChecker.GetGeneration()].GetComponent<InceptionPlayerController>().ClearLog();
    }

    public void PlayerSetDownLevel()
    {
        _inceptionPlayerList[_generationChecker.GetGeneration()].GetComponent<InceptionPlayerController>().ClearLog();
        _inceptionPlayerList[_generationChecker.GetGeneration()].GetComponent<InceptionPlayerController>().ResetToRespawnPosition();
        _rigidBody.isKinematic = true;
        _inceptionPlayerList[_generationChecker.GetGeneration()].SetActive(false);
        _currentGeneration--;
        _generationChecker.DecreaseGeneration();

        if (gameObject.name == "PlayerInceptionBase")
        {
            ClearLog();
        }

        SetAsMainPlayer();
    }

    void PlayBackMode()
    {
        if (_positionLog.Count > 0) { 
            _spawnOnce = false;
            _timerObj.gameObject.SetActive(true);
            _playBackTimer += (Time.deltaTime * depthSpeed);
            

            float finalTimeStamp = _positionLog[_positionLog.Count - 1].TimeStamp;

            _timerObj.localScale = new Vector3((finalTimeStamp - _playBackTimer) / finalTimeStamp * _timerObjCurrentSize, _timerObjCurrentHeight, _timerObjCurrentDepth);

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
                PlayerSetDownLevel();
            }
        }
        else
        {
            PlayerSetDownLevel();
        }
    }
}
