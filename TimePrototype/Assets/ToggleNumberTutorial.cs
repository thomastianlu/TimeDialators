using UnityEngine;
using System.Collections;

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

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Set1 = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Set2 = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Set3 = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
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

            if (_nextSceneTimer < 0)
            {
                _sceneManager.LoadNextScene();
            }
        }
    }
}
