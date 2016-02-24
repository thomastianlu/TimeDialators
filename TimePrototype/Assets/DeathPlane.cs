using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathPlane : MonoBehaviour {

    [SerializeField]
    private Transform _respawnPosition;

    [SerializeField]
    private GameObject _dummyPlayer;

    [SerializeField]
    private InputRecorder _inputRecorder;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.gameObject.transform.position = _respawnPosition.position;

            GameObject DummyPlayer = Instantiate(_dummyPlayer, _respawnPosition.position, _respawnPosition.rotation) as GameObject;
            DummyPlayer.GetComponent<DummyPlayerController>().Initialize(_inputRecorder.ReturnDictionary());

            _inputRecorder.ClearLog();
        }
    }
}
