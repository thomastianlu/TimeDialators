using UnityEngine;
using System.Collections;

public class DoorButtonInception : MonoBehaviour {

    [SerializeField]
    private DoorScriptInception _doorScript;

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
