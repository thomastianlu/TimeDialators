using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNextSceneStart : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        LoadNextScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextScene()
    {
        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
