using UnityEngine;
using System.Collections;

public class DoorScriptInception : MonoBehaviour {

    [SerializeField]
    private Transform _doorObj;
    [SerializeField]
    private Transform _doorOpenPosition;
    [SerializeField]
    private Vector3 _doorClosePosition;
    [SerializeField]
    private InceptionRecorder _inceptionRecorder;

    [SerializeField]
    private float _inceptionSpeed;

    private bool _doorOpen;

	// Use this for initialization
	void Start () {
        _doorClosePosition = _doorObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        _inceptionSpeed = 1/Mathf.Pow(2, _inceptionRecorder.GetGeneration());

	    if (_doorOpen)
        {
            _doorObj.position = Vector3.MoveTowards(_doorObj.position, _doorOpenPosition.position, Time.deltaTime * 200f * _inceptionSpeed);
        }
        else
        {
            _doorObj.position = Vector3.MoveTowards(_doorObj.position, _doorClosePosition, Time.deltaTime * 200f * _inceptionSpeed);
        }
	}

    public void DoorIsOpened(bool open)
    {
        _doorOpen = open;
    }
}
