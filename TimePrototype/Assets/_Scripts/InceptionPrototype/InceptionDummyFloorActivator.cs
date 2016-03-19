using UnityEngine;
using System.Collections;

public class InceptionDummyFloorActivator : MonoBehaviour {

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
            other.GetComponent<InceptionGroundChecker>().SetParent(_parentObj);
            other.GetComponent<InceptionGroundChecker>().SetGravity(0);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "GroundChecker")
        {
            _groundObject.SetActive(false);

            other.GetComponent<InceptionGroundChecker>().SetParent(null);
            other.GetComponent<InceptionGroundChecker>().SetGravity(1);
        }
    }
}
