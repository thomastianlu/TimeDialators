using UnityEngine;
using System.Collections;

public class InceptionColorChange : MonoBehaviour {


    [SerializeField]
    private InceptionRecorder _inceptionLog;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
        _spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        _spriteRenderer.color = new Color (_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, (float) 1/(_inceptionLog.GetGeneration() + 1));
	}
}
