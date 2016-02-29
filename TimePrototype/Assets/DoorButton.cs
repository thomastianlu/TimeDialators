using UnityEngine;
using System.Collections;

public class DoorButton : MonoBehaviour {

    [SerializeField]
    private DoorScript _doorScript;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "DummyPlayer")
        {
            _doorScript.DoorIsOpened(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "DummyPlayer")
        {
            _doorScript.DoorIsOpened(false);
        }
    }
}
