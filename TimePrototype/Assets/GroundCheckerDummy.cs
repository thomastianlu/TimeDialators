using UnityEngine;
using System.Collections;

public class GroundCheckerDummy : MonoBehaviour {

    [SerializeField]
    private DummyPlayerController _parent;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            _parent.SetGround(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            _parent.SetGround(false);
        }
    }
}
