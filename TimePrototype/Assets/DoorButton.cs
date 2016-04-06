using UnityEngine;
using System.Collections;

public class DoorButton : MonoBehaviour {

    [SerializeField]
    private DoorScript _doorScript;

    [SerializeField]
    private SpriteRenderer _doorBtn;

	// Use this for initialization
	void Start () {
        _doorBtn = GetComponent<SpriteRenderer>();
        _doorBtn.color = new Color(1f, 0f, 0f);
    }
	
	void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "DummyPlayer")
        {
            _doorScript.DoorIsOpened(true);
            _doorBtn.color = new Color(1f, 1f, 1f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "DummyPlayer")
        {
            _doorScript.DoorIsOpened(false);
            _doorBtn.color = new Color(1f, 0f, 0f);
        }
    }
}
