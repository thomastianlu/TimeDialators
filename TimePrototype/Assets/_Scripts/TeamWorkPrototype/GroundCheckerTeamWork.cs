﻿using UnityEngine;
using System.Collections;

public class GroundCheckerTeamWork : MonoBehaviour {

    [SerializeField]
    private PlayerControllerTeamWork _parent;


    public void SetParent(Transform newParent)
    {
        _parent.transform.SetParent(newParent);
    }

    public void SetGravity(float GravityPower)
    {
        _parent.GetComponent<Rigidbody2D>().gravityScale = GravityPower;
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Ground" || other.tag == "DummyPlayer") {
            _parent.SetGround(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Ground" || other.tag == "DummyPlayer")
        {
            _parent.SetGround(false);
        }
    }
}
