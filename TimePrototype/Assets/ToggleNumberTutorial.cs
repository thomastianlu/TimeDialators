using UnityEngine;
using System.Collections;
using Rewired;

public class ToggleNumberTutorial : MonoBehaviour {

    [SerializeField]
    private bool Set1 = false;
    [SerializeField]
    private bool Set2 = false;
    [SerializeField]
    private bool Set3 = false;
    [SerializeField]
    private bool Set4 = false;

    [SerializeField]
    private float _nextSceneTimer = 5.0f;

    [SerializeField]
    private SceneManagement _sceneManager;

    [SerializeField]
    private MainCameraIntro _mainCamera;
    private bool _setFadeOnce = false;


    private bool _DUp;
    private bool _DDown;
    private bool _DLeft;
    private bool _DRight;
    private Player _player;

    private AudioSource _audio;

    // Use this for initialization
    void Start () {
        _audio = GameObject.Find("GameAudioSystem").GetComponent<AudioSource>();
	}


    private void GetInput()
    {
        _player = ReInput.players.GetPlayer(0);
        _DUp = _player.GetButtonDown("D-Up");
        _DDown = _player.GetButtonDown("D-Down");
        _DLeft = _player.GetButtonDown("D-Left");
        _DRight = _player.GetButtonDown("D-Right");
    }


    // Update is called once per frame
    void Update () {
        GetInput();
        if (_DUp)
        {
            Set1 = true;
        }

        if (_DLeft)
        {
            Set2 = true;
        }

        if (_DDown)
        {
            Set3 = true;
        }

        if (_DRight)
        {
            Set4 = true;
        }

        if (Set1 && Set2 && Set3 && Set4)
        {
            _nextSceneTimer -= Time.deltaTime;

            if (_nextSceneTimer < 3 && !_setFadeOnce)
            {
                _mainCamera.SetFadeOut();
                _setFadeOnce = true;
            }

            if (_setFadeOnce)
            {
                _audio.volume -= 0.003f;
            }

            if (_nextSceneTimer < 0)
            {
                _sceneManager.LoadNextScene();
            }
        }
    }
}
