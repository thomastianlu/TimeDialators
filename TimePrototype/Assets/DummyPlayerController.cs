﻿using UnityEngine;
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
    
    [SerializeField]
    private int _inputIterator = 0;


    [SerializeField]
    private Dictionary<int, InputPair> _inputLog = new Dictionary<int, InputPair>();

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

    public void Initialize(Dictionary<int, InputPair> InputLog, Vector3 initialSpawnPoint) {

        _initialSpawnPoint = initialSpawnPoint;

        for (int i = 0; i < InputLog.Count; i++)
        {
            _inputLog.Add(i, InputLog[i]);
        }
    }

    public void SetGround(bool grounded)
    {
        _isGrounded = grounded;
    }

    public void Reset() {
        _currentTime = 0f;
        _inputIterator = 0;
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
        if (_inputSpace)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce * _scale);
            _inputSpace = false;
        }

        if (_inputA)
        {
            transform.position += Vector3.left * _moveSpeed * _scale;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (_inputS)
        {

        }

        if (_inputD)
        {
            transform.position += Vector3.right * _moveSpeed * _scale;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
