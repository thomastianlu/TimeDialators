using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathPlane : MonoBehaviour {

    [SerializeField]
    private Transform _respawnPosition;

    [SerializeField]
    private GameObject _dummyPlayer;

    [SerializeField]
    private InputRecorder _inputRecorder;

    [SerializeField]
    private List<GameObject> _listOfGhosts = new List<GameObject>();


    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            foreach (GameObject x in _listOfGhosts)
            {
                x.GetComponent<DummyPlayerController>().Reset();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.gameObject.transform.position = _respawnPosition.position;

            other.gameObject.GetComponent<PlayerController>().ResetTimer();

            //GenerateGhost();
        }

        if (other.tag == "DummyPlayer")
        {
            other.gameObject.SetActive(false);
        }
    }

    public void GenerateGhost()
    {

        GameObject DummyPlayer = Instantiate(_dummyPlayer, _respawnPosition.position, _respawnPosition.rotation) as GameObject;
        DummyPlayer.GetComponent<DummyPlayerController>().Initialize(_inputRecorder.ReturnInputDictionary(), _inputRecorder.ReturnPositionDictionary(), _respawnPosition.position, 1);

        foreach (GameObject x in _listOfGhosts)
        {
            x.SetActive(true);
            x.GetComponent<DummyPlayerController>().Reset();
        }

        _listOfGhosts.Add(DummyPlayer);

        _inputRecorder.ClearLog();
    }

    public void SetRespawnPosition (Transform position)
    {
        _respawnPosition = position;

        foreach (GameObject x in _listOfGhosts)
        {
            Destroy(x);
        }

        _listOfGhosts.Clear();
        _inputRecorder.ClearLog();
    }
}
