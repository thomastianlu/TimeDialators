using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {

    [SerializeField]
    private DeathPlane _deathPlane;

    [SerializeField]
    private PlayerController _player;

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
                _deathPlane.SetRespawnPosition(transform);
                _hitOnce = true;
                _player.ResetTimer();
                _sprite.color = new Color(1f, 1f, 1f);
            }
        }
    }
}
