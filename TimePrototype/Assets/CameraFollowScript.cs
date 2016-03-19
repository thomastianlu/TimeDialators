using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    public void SetPlayerFocus(Transform followObject)
    {
        _followObject = followObject;
    }
	
	// Update is called once per frame
	void Update () {
        _followPosition = _followObject.position;
        _followPosition.z = _followPositionZ;

        transform.position = Vector3.Slerp(transform.position, _followPosition, Time.deltaTime * _followSpeed);


        if (Input.GetKey(KeyCode.F1))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
