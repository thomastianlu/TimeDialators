using UnityEngine;
using System.Collections;

public class MainCameraIntro : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float duration = 0.5f;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float magnitude = 0.5f;

    [SerializeField]
    private bool _shake = true;

    private bool _fadeOut = false;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!_shake) { 
            PlayShake();
            _shake = true;
        }
	}

    public void SetFadeOut()
    {
        _animator.Play("CameraFadeOut");
    }


    public void SetFadeIn()
    {
        _animator.Play("CameraFadeIn");
    }

    public void PlayShake()
    {
        StopAllCoroutines();
        StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f + transform.position.x;
            float y = Random.value * 2.0f - 1.0f + transform.position.y;
            x *= magnitude * damper;
            y *= magnitude * damper;

            Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

        Camera.main.transform.position = originalCamPos;
    }
}
