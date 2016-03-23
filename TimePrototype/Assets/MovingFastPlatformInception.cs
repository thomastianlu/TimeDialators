using UnityEngine;
using System.Collections;

public class MovingFastPlatformInception : MonoBehaviour {

    [SerializeField]
    private Transform _movingPlatformEndPosition;

    [SerializeField]
    private float _platformSpeed;

    [SerializeField]
    private InceptionRecorder _inceptionRecorder;

    [SerializeField]
    private bool _moveToStart = false;

    private Vector3 _startingPosition;

	// Use this for initialization
	void Start () {
        _startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        _platformSpeed = 2 / Mathf.Pow (2, _inceptionRecorder.GetGeneration());

        if (transform.position == _startingPosition)
        {
            _moveToStart = false;
        }
        else if (transform.position == _movingPlatformEndPosition.position)
        {
            _moveToStart = true;
        }

        if (_moveToStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startingPosition, _platformSpeed);
        }
        else { 
            transform.position = Vector3.MoveTowards(transform.position, _movingPlatformEndPosition.position, _platformSpeed);
        }
	}
}
