using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InceptionInputRecorder : MonoBehaviour {

    [SerializeField]
    Dictionary<int, InputPair> _inputLog = new Dictionary<int, InputPair>();

    Dictionary<int, PositionPair> _positionLog1 = new Dictionary<int, PositionPair>();
    Dictionary<int, PositionPair> _positionLog2 = new Dictionary<int, PositionPair>();
    Dictionary<int, PositionPair> _positionLog3 = new Dictionary<int, PositionPair>();
    Dictionary<int, PositionPair> _positionLog4 = new Dictionary<int, PositionPair>();

    [SerializeField]
    private List<Dictionary<int, PositionPair>> _listOfLogs = new List<Dictionary<int, PositionPair>>();
    private int ListGeneration = 0;

    private float _timer;
    private bool _logOnce = false;
    private int _iteratorInput = 0;
    private int _iteratorPosition = 0;

    // Use this for initialization
    void Start () {
        _listOfLogs.Add(_positionLog1);
        _listOfLogs.Add(_positionLog2);
        _listOfLogs.Add(_positionLog3);
        _listOfLogs.Add(_positionLog4);
    }
	
    public Dictionary<int, InputPair> ReturnInputDictionary() {
        Dictionary<int, InputPair> newDictionary = new Dictionary<int, InputPair>();
        newDictionary = _inputLog;
        return newDictionary;
    }

    public Dictionary<int, PositionPair> ReturnPositionDictionary(int DictionaryNumber)
    {
        Dictionary<int, PositionPair> newDictionary = new Dictionary<int, PositionPair>();
        newDictionary = _listOfLogs[DictionaryNumber];
        return newDictionary;
    }

    public void ClearLog(int DictionaryNumber) {
        _inputLog.Clear();
        _listOfLogs[DictionaryNumber].Clear();
    }

    public void RemoveLog(int iteratorPosition)
    {
        _listOfLogs.Remove(_listOfLogs[iteratorPosition]);
        ClearLog(iteratorPosition);
    }
    

    public void SetLog(int LogNumber)
    {
        _timer = _listOfLogs[LogNumber][_listOfLogs[LogNumber].Count - 1].TimeStamp;
        _iteratorInput = _listOfLogs[LogNumber].Count - 1;
    }

    public void LogKey(InputState pressedButton, float timer) {
        _inputLog.Add(_iteratorInput, new InputPair { Input = pressedButton, TimeStamp = timer });
        _iteratorInput++;
    }

    public void LogPosition(float timer, Vector3 PlayerPosition, int logGeneration)
    {
        _listOfLogs[logGeneration].Add(_iteratorPosition, new PositionPair { Position = PlayerPosition, TimeStamp = timer });
        _iteratorPosition++;
    }
   
}
