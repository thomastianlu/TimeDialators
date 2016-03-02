using UnityEngine;
using System.Collections;

public class DummyFloorActivator : MonoBehaviour {

    [SerializeField]
    private GameObject _groundObject;
    [SerializeField]
    private Transform _parentObj;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "GroundChecker")
        {
            _groundObject.SetActive(true);
            other.GetComponent<GroundChecker>().SetParent(_parentObj);
            other.GetComponent<GroundChecker>().SetGravity(0);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "GroundChecker")
        {
            _groundObject.SetActive(false);

            other.GetComponent<GroundChecker>().SetParent(null);
            other.GetComponent<GroundChecker>().SetGravity(1);
        }
    }
}
