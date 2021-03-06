﻿using UnityEngine;
using System.Collections;

public class InceptionDummyFloorActivator : MonoBehaviour {

    [SerializeField]
    private GameObject _groundObject;
    [SerializeField]
    private Transform _parentObj;

    [SerializeField]
    private Transform _playerField;

    [SerializeField]
    private InceptionGroundChecker _groundChecker;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "GroundChecker")
        {
            _groundObject.SetActive(true);
            other.GetComponent<InceptionGroundChecker>().SetParent(_parentObj);
            other.GetComponent<InceptionGroundChecker>().SetGravity(0);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "GroundChecker")
        {
            _groundObject.SetActive(false);

            other.GetComponent<InceptionGroundChecker>().SetParent(_playerField);
            other.GetComponent<InceptionGroundChecker>().SetGravity(1);
        }
    }

    public void UnSetParent()
    {
        _groundChecker.SetParent(_playerField);
        _groundChecker.SetGravity(1);
    }
}
