using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
    
    private Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayAnimation()
    {
        _animator.Play("CameraShake");
    }
}
