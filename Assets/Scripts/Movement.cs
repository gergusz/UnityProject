using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int maxJumps = 1;
    private Rigidbody2D _rb;
    private bool _facingLeft;
    private bool _onGround;
    private int _jumps;
    private float _horizontalAxis;
    private bool _jumpButtonHeldDown;
    private bool _canJump = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");
        _jumpButtonHeldDown = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        var currVelocity = _rb.velocity;
        currVelocity.x = _horizontalAxis * speed;
        _rb.velocity = currVelocity;

        if (_horizontalAxis < 0 && !_facingLeft)
        {
            Flip();
        }
        else if (_horizontalAxis > 0 && _facingLeft)
        {
            Flip();
        }

        if (_jumpButtonHeldDown && _canJump)
        {
            TryJump();
            _canJump = false;
        }

        if (!_jumpButtonHeldDown || _onGround)
        {
            _canJump = true;
        }

    }

    private void TryJump()
    {
        if (_onGround) 
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _onGround = false;
            _jumps++;
        } 
        else if (_jumps < maxJumps)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumps++;
        }
    }

    private void Flip()
    {
        _facingLeft = !_facingLeft;
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _onGround = true;
        _jumps = 0;
    }
}
