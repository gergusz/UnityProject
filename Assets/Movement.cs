using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    Vector2 _moveVel;
    float _moveDir;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    private bool _jumpPressed;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        _moveDir = Input.GetAxisRaw("Horizontal");
        _jumpPressed |= Input.GetButtonDown("Jump");
    }

    private void Move()
    {
        _moveVel = _rigidbody2D.velocity;
        _moveVel.x = _moveDir * moveSpeed * Time.fixedDeltaTime;
        _rigidbody2D.velocity = _moveVel;
    }

    private void HandleJump()
    {
        if (_jumpPressed && _isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumpPressed = false;
        }
    }   

    private void FixedUpdate()
    {
        Move();
        HandleJump();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isGrounded = false;
    }
}
