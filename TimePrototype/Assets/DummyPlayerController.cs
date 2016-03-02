using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DummyPlayerController : MonoBehaviour {
    
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private bool _isGrounded;

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
    private Dictionary<float, PositionPair> _positionLog = new Dictionary<float, PositionPair>();

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

    private float _currentTime = 0f;

    private Vector3 _initialSpawnPoint;


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

    public void Initialize(Dictionary<int, InputPair> InputLog, Dictionary <int, PositionPair> PositionLog, Vector3 initialSpawnPoint) {

        _initialSpawnPoint = initialSpawnPoint;

        for (int i = 0; i < InputLog.Count; i++)
        {
            _inputLog.Add(i, InputLog[i]);
        }

        for (int i = 0; i < PositionLog.Count; i++)
        {
            _positionLog.Add(i, PositionLog[i]);
        }
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

        if (_inputSpace)
        {
        }

        if (_inputA)
        {
        }

        if (_inputS)
        {

        }

        if (_inputD)
        {
        }
    }
}
