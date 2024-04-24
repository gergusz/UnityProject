using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] public int maxJumps = 1;
    [SerializeField] public bool canDash = false;
    private Rigidbody2D _rb;
    public bool _facingLeft;
    private bool _onGround;
    private int _jumps;
    private float _horizontalAxis;
    private bool _jumpButtonHeldDown;
    private bool _canJump = true;
    private Animator _animator;

    [SerializeField]
    private int dashCounter = 0;
    private float lastDashPress = 0f;
    [SerializeField]
    private float dashCD = 0f;
    private float velocitySetterCd = 0f;

    private KeyCode lastDashKey = KeyCode.None;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
        if (velocitySetterCd <= 0f)
        {
            var currVelocity = _rb.velocity;
            currVelocity.x = _horizontalAxis * speed;
            _rb.velocity = currVelocity;
            _animator.SetFloat("speed", Mathf.Abs(_horizontalAxis));

        }
        else
        {
            velocitySetterCd -= Time.fixedDeltaTime;
        }

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

        if (canDash)
        {
            if (dashCD > -0.01 && dashCD < 0.01)
            {
                Dasher();
            }
            else if (dashCD > 0)
            {
                dashCD -= Time.fixedDeltaTime;
            }
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

    private void Dasher()
    {
        if (lastDashPress <= 0)
        {
            dashCounter = 0;
        }
        else
        {
            lastDashPress -= Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (dashCounter % 2 == 0)
            {
                KeyCode key_down = Input.GetKey(KeyCode.A) ? KeyCode.A : KeyCode.D;

                if (lastDashKey == KeyCode.None || lastDashKey == key_down)
                {
                    dashCounter++;
                }
                else
                {
                    dashCounter = 1;
                }
                lastDashPress = 0.2f;
                lastDashKey = key_down;
            }
        }
        else
        {
            if (dashCounter % 2 == 1)
                dashCounter++;
        }
        
        if (dashCounter == 3)
        {
            DisableVelocitySetting(0.1f);
            _rb.AddForce(new Vector2(40f * (_facingLeft ? -1 : 1), 1f), ForceMode2D.Impulse);
            
            lastDashPress = 0f;
            dashCounter = 0;
            dashCD = 3f;
        }
     
    }

    public void Flip()
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

    public void DisableVelocitySetting(float disableTime)
    {
        velocitySetterCd = disableTime;
    }
}
