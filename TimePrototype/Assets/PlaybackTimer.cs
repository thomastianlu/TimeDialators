using UnityEngine;
using System.Collections;

public class PlaybackTimer : MonoBehaviour {

    [SerializeField]
    private float _playBackTimer = 0f;
    [SerializeField]
    private float _playBackEndTimer = 0f;

    [SerializeField]
    private PlayerManagerScript _playerManager;

    [SerializeField]
    private PlayerControllerTeamWork _P1;
    [SerializeField]
    private PlayerControllerTeamWork _P2;
    [SerializeField]
    private PlayerControllerTeamWork _P3;
    [SerializeField]
    private PlayerControllerTeamWork _P4;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ManageTimeline();
    }

    public float GetCurrentTime()
    {
        return _playBackTimer;
    }

    public void ResetTimer()
    {
        _playBackTimer = 0f;
    }

    public void ResetPlayBackMode()
    {
        _P1.ResetPlayBackMode();
        _P2.ResetPlayBackMode();
        _P3.ResetPlayBackMode();
        _P4.ResetPlayBackMode();
    }

    void ManageTimeline()
    {
        // Start the timeline if one of the objects gets into playback mode
        //if (_P1.GetPlayBackMode() && !_P2.GetPlayBackMode() && !_P3.GetPlayBackMode() && !_P4.GetPlayBackMode())
        //{
        //    _playBackEndTimer = _P1.GetRecordedTime();
        //}

        //if (!_P1.GetPlayBackMode() && _P2.GetPlayBackMode() && !_P3.GetPlayBackMode() && !_P4.GetPlayBackMode())
        //{
        //    _playBackEndTimer = _P2.GetRecordedTime();
        //}

        //if (!_P1.GetPlayBackMode() && !_P2.GetPlayBackMode() && _P3.GetPlayBackMode() && !_P4.GetPlayBackMode())
        //{
        //    _playBackEndTimer = _P3.GetRecordedTime();
        //}

        //if (!_P1.GetPlayBackMode() && !_P2.GetPlayBackMode() && !_P3.GetPlayBackMode() && _P4.GetPlayBackMode())
        //{
        //    _playBackEndTimer = _P4.GetRecordedTime();
        //}

        //if (!_P1.GetPlayBackMode() && !_P2.GetPlayBackMode() && !_P3.GetPlayBackMode() && !_P4.GetPlayBackMode())
        //{
        //    _playBackEndTimer = 0f;
        //}

        //_playBackTimer = _playerManager.GetCurrentPlayerTime();
        //_playBackEndTimer = _playerManager.GetCurrentPlayerEndTime();


        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayBackMode();
        }

        //if (_playBackTimer > _playBackEndTimer)
        //{
        //    _playBackTimer = 0f;
        //    _P1.ResetPlayBackMode();
        //    _P2.ResetPlayBackMode();
        //    _P3.ResetPlayBackMode();
        //    _P4.ResetPlayBackMode();
        //}
    }
}
