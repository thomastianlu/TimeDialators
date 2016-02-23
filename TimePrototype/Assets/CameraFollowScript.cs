using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {

    [SerializeField]
    private Transform _followObject;
    private Vector3 _followPosition;
    private float _followPositionZ;
    [SerializeField]
    private float _followSpeed;

	// Use this for initialization
	void Start () {
        _followPositionZ = transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        _followPosition = _followObject.position;
        _followPosition.z = _followPositionZ;

        transform.position = Vector3.Slerp(transform.position, _followPosition, Time.deltaTime * _followSpeed);
	}
}
