using UnityEngine;
using System.Collections;

public class TimedDoor : MonoBehaviour {

    [SerializeField]
    private TimedDoorButton[] _timedDoorButtons;

    [SerializeField]
    private Transform _doorObj;
    [SerializeField]
    private Transform _doorOpenPosition;

    [SerializeField]
    private bool _openDoor = false;

    [SerializeField]
    private int _currentSwitch = 0;

    private bool _audioPlayOnce = false;
    private AudioSource _audio;
	// Use this for initialization
	void Start () {
        _audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (CanOpenDoor()) { 
            _doorObj.position = Vector3.Lerp(_doorObj.position, _doorOpenPosition.position, Time.deltaTime * 10f);
            if (!_audioPlayOnce) { 
                _audio.Play();
            }
        }
    }

    public bool CanOpenDoor()
    {
        foreach(TimedDoorButton x in _timedDoorButtons)
        {
            if (!x.CanOpen())
            {
                return false;
            }
        }
        return true;
    }
}
