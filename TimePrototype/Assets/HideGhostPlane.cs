using UnityEngine;
using System.Collections;

public class HideGhostPlane : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DummyPlayer")
        {
            other.gameObject.SetActive(false);
        }
    }
}
