using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InceptionDummyPlayerController : MonoBehaviour {
    
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private bool _isGrounded;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _moveSpeed;
    private float _moveSpeedReset;

    public GameObject Hat;
    
    [SerializeField]
    private int _inputIterator = 0;

    private int _positionIterator = 0;


    [SerializeField]
    private Dictionary<int, InputPair> _inputLog = new Dictionary<int, InputPair>();
    [SerializeField]
    private Dictionary<int, PositionPair> _positionLog = new Dictionary<int, PositionPair>();

    [SerializeField]
    private bool _inputW = false;
    [SerializeField]
    private bool _inputA = false;
    [SerializeField]
    private bool _inputS = false;
    [SerializeField]
    private bool _inputD = false;
    [SerializeField]
    private bool _inputSpace = false;
    private bool _inputOnce = false;

    [SerializeField]
    private float _scale;

    [SerializeField]
    private Transform _art;

    private float _currentTime = 0f;

    private Vector3 _initialSpawnPoint;

    [SerializeField]
    private int _generation;

    [SerializeField]
    private Animator _animator;


    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _moveSpeedReset = _moveSpeed;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        ManageInputs();
        ManageMovement();
    }

    public void Initialize(Dictionary<int, InputPair> InputLog, Dictionary <int, PositionPair> PositionLog, Vector3 initialSpawnPoint, int generation, Transform player) {

        _initialSpawnPoint = initialSpawnPoint;
        _generation = generation;
        _scale = (float)1/generation;
        _player = player;

        _animator.speed = (float) 1/generation;

        for (int i = 0; i < InputLog.Count; i++)
        {
            _inputLog.Add(i, InputLog[i]);
        }

        _positionLog = PositionLog;
    }

    public void IncreaseGeneration()
    {
        _generation++;
        _scale = (float) 1 / _generation;
    }

    public void SetGround(bool grounded)
    {
        _isGrounded = grounded;
    }

    public void Reset() {
        _currentTime = 0f;
        _inputIterator = 0;
        _positionIterator = 0;
        transform.position = _initialSpawnPoint;

        _inputW = false;
        _inputA = false;
        _inputS = false;
        _inputD = false;
        _inputSpace = false;
        _inputOnce = false;
    }

    void ManageInputs() {
        _currentTime += Time.deltaTime * _scale;
        _rigidBody.gravityScale = _scale;

        if (_inputIterator < _inputLog.Count - 1)
        {
            if (_currentTime >= _inputLog[_inputIterator].TimeStamp)
            {
                SetInputState();
                _inputIterator++;
            }
        }
    }

    void SetInputState() {
        switch (_inputLog[_inputIterator].Input)
        {
            case InputState.WPressed:
                _inputW = true;
                break;

            case InputState.WReleased:
                _inputW = false;
                break;

            case InputState.APressed:
                _inputA = true;
                break;

            case InputState.AReleased:
                _inputA = false;
                break;

            case InputState.SPressed:
                _inputS = true;
                break;

            case InputState.SReleased:
                _inputS = false;
                break;

            case InputState.DPressed:
                _inputD = true;
                break;

            case InputState.DReleased:
                _inputD = false;
                break;

            case InputState.SpacePressed:
                if (!_inputOnce) { 
                    _inputSpace = true;
                    _inputOnce = true;
                }
                break;

            case InputState.SpaceReleased:
                _inputOnce = false;
                break;
        }
    }

    void ManageMovement()
    {

        if (_positionIterator < _positionLog.Count - 1) { 
            if (_positionLog[_positionIterator].TimeStamp < _currentTime) { 
                transform.position = _positionLog[_positionIterator].Position;
                _positionIterator++;
            }
        }
        else
        {
            _player.position = transform.position;
            Destroy(gameObject);
        }

        if (_inputSpace)
        {
        }

        if (_inputA)
        {
            _art.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (_inputS)
        {

        }

        if (_inputD)
        {
            _art.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
