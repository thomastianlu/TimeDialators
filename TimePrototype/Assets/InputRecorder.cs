using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct InputPair
{
    public InputState Input { get; set; }
    public float TimeStamp { get; set; }
}

public struct PositionPair
{
    public Vector3 Position { get; set; }
    public float TimeStamp { get; set; }
}


public enum InputState
{
    WPressed,
    WReleased,
    APressed,
    AReleased,
    SPressed,
    SReleased,
    DPressed,
    DReleased,
    SpacePressed,
    SpaceReleased
}

public class InputRecorder : MonoBehaviour {

    [SerializeField]
    Dictionary<int, InputPair> _inputLog = new Dictionary<int, InputPair>();

    Dictionary<int, PositionPair> _positionLog = new Dictionary<int, PositionPair>();

    private float _timer;
    private bool _logOnce = false;
    private int _iteratorInput = 0;
    private int _iteratorPosition = 0;

    // Use this for initialization
    void Start () {
	
	}
	
    public Dictionary<int, InputPair> ReturnInputDictionary() {
        Dictionary<int, InputPair> newDictionary = new Dictionary<int, InputPair>();
        newDictionary = _inputLog;
        return newDictionary;
    }

    public Dictionary<int, PositionPair> ReturnPositionDictionary()
    {
        Dictionary<int, PositionPair> newDictionary = new Dictionary<int, PositionPair>();
        newDictionary = _positionLog;
        return newDictionary;
    }

    public void ClearLog() {
        _inputLog.Clear();
        _positionLog.Clear();
        _timer = 0f;
        _iteratorInput = 0;
        _iteratorPosition = 0;
    }

    public void LogKey(InputState pressedButton, float timer) {
        _inputLog.Add(_iteratorInput, new InputPair { Input = pressedButton, TimeStamp = timer });
        _iteratorInput++;
    }

    public void LogPosition(float timer, Vector3 PlayerPosition)
    {
        _positionLog.Add(_iteratorPosition, new PositionPair { Position = PlayerPosition, TimeStamp = timer });
        _iteratorPosition++;
    }

	// Update is called once per frame
	void FixedUpdate () {

        //_timer += Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.W)) {
        //    _inputLog.Add(_iterator, new InputPair {Input = InputState.WPressed, TimeStamp = _timer });
        //    _iterator++;
        //}
        //else if (Input.GetKeyUp(KeyCode.W))
        //{
        //    _inputLog.Add(_iterator, new InputPair { Input = InputState.WReleased, TimeStamp = _timer });
        //    _iterator++;
        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    _inputLog.Add(_iterator, new InputPair { Input = InputState.APressed, TimeStamp = _timer });
        //    _iterator++;
        //}
        //else if (Input.GetKeyUp(KeyCode.A))
        //{
        //    _inputLog.Add(_iterator, new InputPair { Input = InputState.AReleased, TimeStamp = _timer });
        //    _iterator++;
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    _inputLog.Add(_iterator, new InputPair { Input = InputState.SPressed, TimeStamp = _timer });
        //    _iterator++;
        //}
        //else if (Input.GetKeyUp(KeyCode.S))
        //{
        //    _inputLog.Add(_iterator, new InputPair { Input = InputState.SReleased, TimeStamp = _timer });
        //    _iterator++;
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    _inputLog.Add(_iterator, new InputPair { Input = InputState.DPressed, TimeStamp = _timer });
        //    _iterator++;
        //}
        //else if (Input.GetKeyUp(KeyCode.D))
        //{
        //    _inputLog.Add(_iterator, new InputPair { Input = InputState.DReleased, TimeStamp = _timer });
        //    _iterator++;
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    _inputLog.Add(_iterator, new InputPair { Input = InputState.SpacePressed, TimeStamp = _timer });
        //    _iterator++;
        //}
        //else if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    _inputLog.Add(_iterator, new InputPair { Input = InputState.SpaceReleased, TimeStamp = _timer });
        //    _iterator++;
        //}
    }
}
