using UnityEngine;
using System.Collections;

public class PlayerManagerScript : MonoBehaviour {

    [SerializeField]
    private PlayerControllerTeamWork[] _players;

    [SerializeField]
    private CameraFollowScript _mainCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ManagerPlayerSwitching();
    }


    void ManagerPlayerSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _players[0].SetPlayerMode(PlayerState.PlayerMode);
            _players[1].SetPlayerMode(PlayerState.FollowMode);
            _players[2].SetPlayerMode(PlayerState.FollowMode);
            _players[3].SetPlayerMode(PlayerState.FollowMode);

            _players[0].SetMainCharacter(true);
            _players[1].SetMainCharacter(false);
            _players[2].SetMainCharacter(false);
            _players[3].SetMainCharacter(false);

            _mainCamera.SetPlayerFocus(_players[0].transform);
            
            _players[1].SetFollowPoint(_players[0].ReturnFollowPoint());
            _players[2].SetFollowPoint(_players[0].ReturnFollowPoint());
            _players[3].SetFollowPoint(_players[0].ReturnFollowPoint());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _players[0].SetPlayerMode(PlayerState.FollowMode);
            _players[1].SetPlayerMode(PlayerState.PlayerMode);
            _players[2].SetPlayerMode(PlayerState.FollowMode);
            _players[3].SetPlayerMode(PlayerState.FollowMode);

            _players[0].SetMainCharacter(false);
            _players[1].SetMainCharacter(true);
            _players[2].SetMainCharacter(false);
            _players[3].SetMainCharacter(false);

            _mainCamera.SetPlayerFocus(_players[1].transform);

            _players[0].SetFollowPoint(_players[1].ReturnFollowPoint());
            _players[2].SetFollowPoint(_players[1].ReturnFollowPoint());
            _players[3].SetFollowPoint(_players[1].ReturnFollowPoint());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _players[0].SetPlayerMode(PlayerState.FollowMode);
            _players[1].SetPlayerMode(PlayerState.FollowMode);
            _players[2].SetPlayerMode(PlayerState.PlayerMode);
            _players[3].SetPlayerMode(PlayerState.FollowMode);

            _players[0].SetMainCharacter(false);
            _players[1].SetMainCharacter(false);
            _players[2].SetMainCharacter(true);
            _players[3].SetMainCharacter(false);

            _mainCamera.SetPlayerFocus(_players[2].transform);

            _players[0].SetFollowPoint(_players[2].ReturnFollowPoint());
            _players[1].SetFollowPoint(_players[2].ReturnFollowPoint());
            _players[3].SetFollowPoint(_players[2].ReturnFollowPoint());
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _players[0].SetPlayerMode(PlayerState.FollowMode);
            _players[1].SetPlayerMode(PlayerState.FollowMode);
            _players[2].SetPlayerMode(PlayerState.FollowMode);
            _players[3].SetPlayerMode(PlayerState.PlayerMode);

            _players[0].SetMainCharacter(false);
            _players[1].SetMainCharacter(false);
            _players[2].SetMainCharacter(false);
            _players[3].SetMainCharacter(true);

            _mainCamera.SetPlayerFocus(_players[3].transform);

            _players[0].SetFollowPoint(_players[3].ReturnFollowPoint());
            _players[1].SetFollowPoint(_players[3].ReturnFollowPoint());
            _players[2].SetFollowPoint(_players[3].ReturnFollowPoint());
        }
    }
}
