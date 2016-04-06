using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManagerScript : MonoBehaviour {

    [SerializeField]
    private bool _recordMode;

    [SerializeField]
    private bool _playBackMode;

    [SerializeField]
    private SpriteRenderer[] _backDrop;

    [SerializeField]
    private PlayerControllerTeamWork[] _players;

    [SerializeField]
    private List<PlayerControllerTeamWork> _playersPlaybackList = new List<PlayerControllerTeamWork>();

    [SerializeField]
    private CameraFollowScript _mainCamera;

    private float _longestRecordedTime;

    [SerializeField]
    private float _playBackTimer = 0f;

    [SerializeField]
    private float _playBackTimerLimit = 0f;

    [SerializeField]
    private bool _allowSwitching = false;
    [SerializeField]
    private bool _followEnabled = false;

	// Use this for initialization
	void Start () {
	
	}

    // Playback Timer needs to be in the playback timer script


    public void SetRecordMode(bool recordMode)
    {
        _recordMode = recordMode;
    }
	
	// Update is called once per frame
	void Update () {
        ManageRecordMode();
        ManagerPlayerSwitching();
        ManagePlayBackMode();
    }

    public void ResetAllTimers() {
        foreach (PlayerControllerTeamWork x in _players) {
            x.ResetTimer();
        }
    }

    public void ResetAllPlayBack()
    {
        foreach (PlayerControllerTeamWork x in _players)
        {
            x.ResetPlayBackMode();
        }

        _playBackTimer = 0f;
        _playBackTimerLimit = GetLongestTime();
    }

    // Need to return the longest time in the pool
    public float GetLongestTime() {
        float largestNumber = 0;

        foreach(PlayerControllerTeamWork x in _players) {

            if (largestNumber < x.GetRecordedTime())
            {
                largestNumber = x.GetRecordedTime();
            }
        }

        return largestNumber;
    }

    public float GetCurrentPlayerTime()
    {
        return _playBackTimer;
    }

    public float GetCurrentPlayerEndTime()
    {
        return _playBackTimerLimit;
    }

    void ManageRecordMode()
    {
        if (_recordMode)
        {
            foreach (SpriteRenderer x in _backDrop) { 
                x.color = new Color(0.6f, 0.6f, 0.6f);
            }
        }
        else
        {
            foreach (SpriteRenderer x in _backDrop)
            {
                x.color = new Color(0.3f, 0.3f, 0.3f);
            }
        }
    }

    void ManagePlayBackMode()
    {
        _playBackTimer += Time.deltaTime;

        if (_playBackTimer > _playBackTimerLimit)
        {
            ResetAllPlayBack();
        }
    }

    void ManagerPlayerSwitching()
    {
        if (_allowSwitching) { 
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetP1();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetP2();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetP3();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetP4();
            }
        }

        if (_followEnabled) { 
            if (Input.GetKeyDown(KeyCode.F))
            {
                foreach(PlayerControllerTeamWork x in _players)
                {
                    if (x.ReturnPlayerState() != PlayerState.PlayerMode)
                    {
                        x.SetPlayerMode(PlayerState.FollowMode);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (PlayerControllerTeamWork x in _players)
            {
                if (x.ReturnPlayerState() != PlayerState.PlayerMode)
                {
                    x.SetPlayerMode(PlayerState.IdleMode);
                }
            }
        }
    }

    public void SetP1()
    {
        _players[0].SetPlayerMode(PlayerState.PlayerMode);
        _players[1].SetPlayerMode(PlayerState.IdleMode);
        _players[2].SetPlayerMode(PlayerState.IdleMode);
        _players[3].SetPlayerMode(PlayerState.IdleMode);

        _players[0].SetMainCharacter(true);
        _players[1].SetMainCharacter(false);
        _players[2].SetMainCharacter(false);
        _players[3].SetMainCharacter(false);

        _mainCamera.SetPlayerFocus(_players[0].transform);

        _players[1].SetFollowPoint(_players[0].ReturnFollowPoint());
        _players[2].SetFollowPoint(_players[0].ReturnFollowPoint());
        _players[3].SetFollowPoint(_players[0].ReturnFollowPoint());
    }

    public void SetP2()
    {
        _players[0].SetPlayerMode(PlayerState.IdleMode);
        _players[1].SetPlayerMode(PlayerState.PlayerMode);
        _players[2].SetPlayerMode(PlayerState.IdleMode);
        _players[3].SetPlayerMode(PlayerState.IdleMode);

        _players[0].SetMainCharacter(false);
        _players[1].SetMainCharacter(true);
        _players[2].SetMainCharacter(false);
        _players[3].SetMainCharacter(false);

        _mainCamera.SetPlayerFocus(_players[1].transform);

        _players[0].SetFollowPoint(_players[1].ReturnFollowPoint());
        _players[2].SetFollowPoint(_players[1].ReturnFollowPoint());
        _players[3].SetFollowPoint(_players[1].ReturnFollowPoint());
    }

    public void SetP3()
    {
        _players[0].SetPlayerMode(PlayerState.IdleMode);
        _players[1].SetPlayerMode(PlayerState.IdleMode);
        _players[2].SetPlayerMode(PlayerState.PlayerMode);
        _players[3].SetPlayerMode(PlayerState.IdleMode);

        _players[0].SetMainCharacter(false);
        _players[1].SetMainCharacter(false);
        _players[2].SetMainCharacter(true);
        _players[3].SetMainCharacter(false);

        _mainCamera.SetPlayerFocus(_players[2].transform);

        _players[0].SetFollowPoint(_players[2].ReturnFollowPoint());
        _players[1].SetFollowPoint(_players[2].ReturnFollowPoint());
        _players[3].SetFollowPoint(_players[2].ReturnFollowPoint());
    }

    public void SetP4()
    {
        _players[0].SetPlayerMode(PlayerState.IdleMode);
        _players[1].SetPlayerMode(PlayerState.IdleMode);
        _players[2].SetPlayerMode(PlayerState.IdleMode);
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
