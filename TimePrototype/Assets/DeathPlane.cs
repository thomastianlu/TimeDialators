﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathPlane : MonoBehaviour {

    [SerializeField]
    private Transform _respawnPosition;

    [SerializeField]
    private GameObject _dummyPlayer;

    [SerializeField]
    private InputRecorder _inputRecorder;

    [SerializeField]
    private List<GameObject> _listOfGhosts = new List<GameObject>();


    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            foreach (GameObject x in _listOfGhosts)
            {
                x.GetComponent<DummyPlayerController>().Reset();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.gameObject.transform.position = _respawnPosition.position;

            other.gameObject.GetComponent<PlayerController>().ResetTimer();

            GameObject DummyPlayer = Instantiate(_dummyPlayer, _respawnPosition.position, _respawnPosition.rotation) as GameObject;
            DummyPlayer.GetComponent<DummyPlayerController>().Initialize(_inputRecorder.ReturnDictionary(), _respawnPosition.position);

            foreach (GameObject x in _listOfGhosts) {
                x.GetComponent<DummyPlayerController>().Reset();
            }

            _listOfGhosts.Add(DummyPlayer);

            _inputRecorder.ClearLog();
        }
    }
}
