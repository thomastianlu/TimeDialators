using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D _rigidBody;

    [SerializeField]
    private bool _isGrounded;


    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _moveSpeed;
    private float _moveSpeedReset;

    [SerializeField]
    private InputRecorder _inputRecorder;

    [SerializeField]
    private float _timer = 0f;

    [SerializeField]
    private Animator _anim;

    private bool _APressOnce = false;
    private bool _DPressOnce = false;


	// Use this for initialization
	void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
        _moveSpeedReset = _moveSpeed;
	}
	
    public void SetGround(bool grounded) {
        _isGrounded = grounded;
    }

	// Update is called once per frame
	void FixedUpdate () {
        ManageInputs();
    }

    public void ResetTimer() {
        _timer = 0f;
    }

    void ManageInputs() {

        _timer += Time.deltaTime;

        _inputRecorder.LogPosition(_timer, transform.position);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            _inputRecorder.LogKey(InputState.SpacePressed, _timer);
        }

        if (!_isGrounded) {
            _anim.CrossFade("JumpHold", 0.1f);
        }
        else if (_isGrounded && !_APressOnce && !_DPressOnce) {
            _anim.Play("Idle");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _inputRecorder.LogKey(InputState.SpaceReleased, _timer);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * _moveSpeed;
            transform.localScale = new Vector3(-1f, 1f, 1f);
            _anim.Play("Walk");
            if (!_APressOnce) { 
                _inputRecorder.LogKey(InputState.APressed, _timer);
                _APressOnce = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            _inputRecorder.LogKey(InputState.AReleased, _timer);
            _APressOnce = false;
            _anim.Play("Idle");
        }

        if (Input.GetKeyDown(KeyCode.W) && _isGrounded == true)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce);
            _isGrounded = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _inputRecorder.LogKey(InputState.SPressed, _timer);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _inputRecorder.LogKey(InputState.SReleased, _timer);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * _moveSpeed;
            transform.localScale = new Vector3(1f, 1f, 1f);
            _anim.Play("Walk");
            if (!_DPressOnce)
            {
                _inputRecorder.LogKey(InputState.DPressed, _timer);
                _DPressOnce = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.D)) {
            _inputRecorder.LogKey(InputState.DReleased, _timer);
            _DPressOnce = false;
            _anim.Play("Idle");
        }
    }
}
