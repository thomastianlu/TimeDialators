using UnityEngine;
using System.Collections;

public class InceptionPerpetualRotateScript : MonoBehaviour {

    [SerializeField]
    private float _rotateSpeed;

    [SerializeField]
    private InceptionRecorder _inceptionRecorder;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * _rotateSpeed * 1/(_inceptionRecorder.GetGeneration() + 1));
	}
}
