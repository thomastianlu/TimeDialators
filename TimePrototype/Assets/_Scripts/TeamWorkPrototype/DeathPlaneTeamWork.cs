using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathPlaneTeamWork : MonoBehaviour {

    [SerializeField]
    private Transform _respawnPosition;
    

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.gameObject.transform.position = _respawnPosition.position;
        }
    }

    public void SetRespawnPosition (Transform position)
    {
        _respawnPosition = position;
    }
}
