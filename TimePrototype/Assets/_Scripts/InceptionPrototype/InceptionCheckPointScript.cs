using UnityEngine;
using System.Collections;

public class InceptionCheckPointScript : MonoBehaviour {

    [SerializeField]
    private InceptionPlayerController _player;

    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private InceptionPlayerController[] _allCharacters;

    [SerializeField]
    private bool _hitOnce = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
    {
        if (!_hitOnce) { 
            if (other.tag == "Player") {
                _hitOnce = true;
                _player = other.GetComponent<InceptionPlayerController>();

                _sprite.color = new Color(1f, 1f, 1f);

                foreach (InceptionPlayerController x in _allCharacters)
                {
                    bool isCurrentlyActive = false;

                    if (!x.gameObject.activeSelf)
                    {
                        x.gameObject.SetActive(true);
                    }
                    else
                    {
                        isCurrentlyActive = true;
                    }
                    
                    x.SetRespawnPosition(transform);

                    if (other.gameObject != x.gameObject && !isCurrentlyActive)
                    {
                        x.gameObject.SetActive(false);
                    }
                }

                _player.ResetInception();
            }
        }
    }
}
