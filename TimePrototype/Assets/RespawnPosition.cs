using UnityEngine;
using System.Collections;

public class RespawnPosition : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer _checkPointOrb;

    [SerializeField]
    private Transform[] _players;

	// Use this for initialization
	void Start () {
	    
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        _checkPointOrb.color = new Color(1f, 1f, 1f);

        if (other.tag == "Player")
        {
            foreach (Transform x in _players)
            {
                if (other.transform != x)
                {
                    x.transform.position = transform.position;
                }
            }
        }
    }
}
