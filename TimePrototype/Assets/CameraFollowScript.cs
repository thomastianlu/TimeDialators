﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraFollowScript : MonoBehaviour {

    [SerializeField]
    private Transform _followObject;
    private Vector3 _followPosition;
    private float _followPositionZ;
    [SerializeField]
    private float _followSpeed;

    [SerializeField]
    private Transform[] _allRobots;

	// Use this for initialization
	void Start () {
        _followPositionZ = transform.position.z;
    }

    public void SetPlayerFocus(Transform followObject)
    {
        _followObject = followObject;
    }

    public void SetFollowPlayer(int pos)
    {
        _followObject = _allRobots[pos];
        Debug.Log("HERRO");
    }
	
    public void SetPlayer1(){ _followObject = _allRobots[0]; }
    public void SetPlayer2() { _followObject = _allRobots[1]; }
    public void SetPlayer3() { _followObject = _allRobots[2]; }
    public void SetPlayer4() { _followObject = _allRobots[3]; }

    // Update is called once per frame
    void Update () {
        _followPosition = _followObject.position;
        _followPosition.z = _followPositionZ;

        transform.position = Vector3.Lerp(transform.position, _followPosition, Time.deltaTime * _followSpeed);


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
