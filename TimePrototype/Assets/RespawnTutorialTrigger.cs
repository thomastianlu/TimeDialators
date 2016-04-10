using UnityEngine;
using System.Collections;

public class RespawnTutorialTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject _tutorialObj;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _tutorialObj.SetActive(true);
        }
    }
}
