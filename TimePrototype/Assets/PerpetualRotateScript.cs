using UnityEngine;
using System.Collections;

public class PerpetualRotateScript : MonoBehaviour {

    [SerializeField]
    private float _rotateSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * _rotateSpeed);
	}
}
