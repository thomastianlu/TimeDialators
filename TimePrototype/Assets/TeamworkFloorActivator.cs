using UnityEngine;
using System.Collections;

public class TeamworkFloorActivator : MonoBehaviour {

    [SerializeField]
    private GameObject _headObject;
    
    [SerializeField]
    private Transform _parentObj;

    [SerializeField]
    private Transform _playerField;

    [SerializeField]
    private GroundCheckerTeamWork _groundChecker;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "GroundChecker")
        {
            _headObject.SetActive(true);
            other.GetComponent<GroundCheckerTeamWork>().SetParent(_parentObj);
            other.GetComponent<GroundCheckerTeamWork>().SetGravity(0);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "GroundChecker")
        {
            _headObject.SetActive(false);
            other.GetComponent<GroundCheckerTeamWork>().SetParent(_playerField);
            other.GetComponent<GroundCheckerTeamWork>().SetGravity(1);
        }
    }
}
