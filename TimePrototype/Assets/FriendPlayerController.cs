using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendPlayerController : MonoBehaviour {

    [SerializeField]
    private Transform _leadPlayer;
    [SerializeField]
    private int _numberInLine;

    private float _timer = 0;

    private int _logIterator = 0;
    private int _positionIterator = 0;

    private Vector3 _lastLoggedPosition;

    Dictionary<int, PositionPair> _positionLog = new Dictionary<int, PositionPair>();

    // Use this for initialization
    void Start () {
        _lastLoggedPosition = _leadPlayer.position;
	}

	// Update is called once per frame
	void Update () {

        if (_leadPlayer.position != _lastLoggedPosition) {

            _timer += Time.deltaTime;

            _positionLog.Add(_logIterator, new PositionPair { Position = _leadPlayer.position, TimeStamp = _timer });

            if (_positionLog[_positionIterator].TimeStamp < _timer - (float)_numberInLine/2)
            {
                transform.position = _positionLog[_positionIterator].Position;
                _positionIterator++;
            }

            _logIterator++;
            _lastLoggedPosition = _leadPlayer.position;
        }
	}
}
