using UnityEngine;
using System.Collections;

public class CageScript : MonoBehaviour {

    [SerializeField]
    private bool _hasPrisoner = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool GetPrisonerStatus()
    {
        return _hasPrisoner;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _hasPrisoner = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _hasPrisoner = false;
        }
    }
}
