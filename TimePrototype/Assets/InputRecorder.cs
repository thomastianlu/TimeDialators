using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct InputPair
{
    public InputState Input { get; set; }
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

    private float _timer;
    private bool _logOnce = false;
    private int _iterator = 0;

	// Use this for initialization
	void Start () {
	
	}
	
    public Dictionary<int, InputPair> ReturnDictionary() {
        Dictionary<int, InputPair> newDictionary = new Dictionary<int, InputPair>();
        newDictionary = _inputLog;
        return newDictionary;
    }

    public void ClearLog() {
        _inputLog.Clear();
        _timer = 0f;
        _iterator = 0;
    }

	// Update is called once per frame
	void FixedUpdate () {

        _timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W)) {
            _inputLog.Add(_iterator, new InputPair {Input = InputState.WPressed, TimeStamp = _timer });
            _iterator++;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            _inputLog.Add(_iterator, new InputPair { Input = InputState.WReleased, TimeStamp = _timer });
            _iterator++;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _inputLog.Add(_iterator, new InputPair { Input = InputState.APressed, TimeStamp = _timer });
            _iterator++;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _inputLog.Add(_iterator, new InputPair { Input = InputState.AReleased, TimeStamp = _timer });
            _iterator++;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _inputLog.Add(_iterator, new InputPair { Input = InputState.SPressed, TimeStamp = _timer });
            _iterator++;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            _inputLog.Add(_iterator, new InputPair { Input = InputState.SReleased, TimeStamp = _timer });
            _iterator++;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _inputLog.Add(_iterator, new InputPair { Input = InputState.DPressed, TimeStamp = _timer });
            _iterator++;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _inputLog.Add(_iterator, new InputPair { Input = InputState.DReleased, TimeStamp = _timer });
            _iterator++;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _inputLog.Add(_iterator, new InputPair { Input = InputState.SpacePressed, TimeStamp = _timer });
            _iterator++;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _inputLog.Add(_iterator, new InputPair { Input = InputState.SpaceReleased, TimeStamp = _timer });
            _iterator++;
        }
    }
}
