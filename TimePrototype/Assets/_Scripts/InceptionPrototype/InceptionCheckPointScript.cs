using UnityEngine;
using System.Collections;

public class InceptionCheckPointScript : MonoBehaviour {

    [SerializeField]
    private InceptionPlayerController _player;

    [SerializeField]
    private SpriteRenderer _sprite;

    private bool _hitOnce = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
    {
        if (!_hitOnce) { 
            if (other.tag == "PlayerCheck") {
                _hitOnce = true;
                _player.ResetTimer();
                _sprite.color = new Color(1f, 1f, 1f);
            }
        }
    }
}
