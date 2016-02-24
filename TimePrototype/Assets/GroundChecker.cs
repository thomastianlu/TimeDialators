using UnityEngine;
using System.Collections;

public class GroundChecker : MonoBehaviour {

    [SerializeField]
    private PlayerController _parent;


    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground" || other.tag == "DummyPlayer") {
            Debug.Log(other.tag);
            _parent.SetGround(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Ground" || other.tag == "DummyPlayer")
        {
            _parent.SetGround(false);
        }
    }
}
