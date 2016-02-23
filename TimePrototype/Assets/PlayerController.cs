using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D _rigidBody;

    [SerializeField]
    private bool _isGrounded;


    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _moveSpeed;
    private float _moveSpeedReset;

	// Use this for initialization
	void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
        _moveSpeedReset = _moveSpeed;
	}
	
    public void SetGround(bool grounded) {
        _isGrounded = grounded;
    }

	// Update is called once per frame
	void Update () {
        ManageInputs();
    }

    void ManageInputs() {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce);
            _isGrounded = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * _moveSpeed;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (Input.GetKey(KeyCode.S))
        {

        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * _moveSpeed;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
