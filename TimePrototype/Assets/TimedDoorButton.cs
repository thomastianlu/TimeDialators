using UnityEngine;
using System.Collections;

public class TimedDoorButton : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private bool _isActive = false;

    [SerializeField]
    private bool _personOn = false;

    [SerializeField]
    private TimedDoor _timedDoor;

    [SerializeField]
    private bool _isFirstDoor;
    [SerializeField]
    private float _timer;
    private float _timerReset;

    private bool _setOnce = false;

    [SerializeField]
    private AudioSource _audio;

	// Use this for initialization
	void Start () {
        _timerReset = _timer;
        _audio = GetComponent<AudioSource>();
	}
	
    void Update(){
        _timer -= Time.deltaTime;

        if (_timer > 0)
        {
            _sprite.color = new Color(135f / 255f, 208f / 255f, 121f / 255f);
        }
        else
        {
            _sprite.color = new Color(234f / 255f, 43f / 255f, 43f / 255f);
        }
    }

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _personOn = true;
            _timer = _timerReset;
            if (!_audio.isPlaying) { 
                _audio.Play();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _personOn = false;
            _timer = 0;
        }
    }

    public bool CanOpen()
    {
        if (_personOn && _timer > 0)
        {
            return true;
        }
        return false;
    }
}
