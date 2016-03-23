using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathPlaneInception : MonoBehaviour {

    [SerializeField]
    private Transform _respawnPosition;
    

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.transform.position = _respawnPosition.position;
        }
    }

    public void SetRespawnPosition (Transform RespawnPosition)
    {
        _respawnPosition = RespawnPosition;
    }
}
