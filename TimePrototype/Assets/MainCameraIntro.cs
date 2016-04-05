using UnityEngine;
using System.Collections;

public class MainCameraIntro : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Animator _animator;

    private bool _fadeOut = false;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetFadeOut()
    {
        _animator.Play("CameraFadeOut");
    }


    public void SetFadeIn()
    {
        _animator.Play("CameraFadeIn");
    }
}
