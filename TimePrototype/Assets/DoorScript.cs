using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    [SerializeField]
    private Transform _doorObj;
    [SerializeField]
    private Transform _doorOpenPosition;
    [SerializeField]
    private Vector3 _doorClosePosition;

    private bool _doorOpen;

	// Use this for initialization
	void Start () {
        _doorClosePosition = _doorObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	    if (_doorOpen)
        {
            _doorObj.position = Vector3.MoveTowards(_doorObj.position, _doorOpenPosition.position, Time.deltaTime * 100f);
        }
        else
        {
            _doorObj.position = Vector3.MoveTowards(_doorObj.position, _doorClosePosition, Time.deltaTime * 100f);
        }
	}

    public void DoorIsOpened(bool open)
    {
        _doorOpen = open;
    }
}
