using UnityEngine;
using System.Collections;

public class JumpCheck : MonoBehaviour {

    [SerializeField]
    private PlayerControllerTeamWork _playerController;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 14)
        {
            _playerController.changeJumpState(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 14)
        {
            _playerController.changeJumpState(false);
        }
    }
}
