using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public int maxLife=4;
    [SerializeField]
    public int currentLife=0;

    private Animator _animator;

    private float timeToRegenALife = 2f;
    private float regenProgress = 2f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        FillHealthBar();
    }


    // Update is called once per frame
    void Update()
    {
        if (currentLife < maxLife)
        {
            Regen();
        }
        if (currentLife <= 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerRespawn>().InitiateRespawn();
            _animator.ResetTrigger("isHurt");
            gameObject.SetActive(false);
            

        }
    }

    void Regen()
    {
        if (regenProgress < 0)
        {
            currentLife++;
            regenProgress = timeToRegenALife;
        }
        else
        {
            regenProgress -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentLife--;
            _animator.ResetTrigger("isHurt");
            _animator.SetTrigger("isHurt");
            regenProgress = timeToRegenALife;
            var a = collision.transform.position.x - transform.position.x;
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(a*2f, 2f), ForceMode2D.Impulse);
            gameObject.GetComponent<Movement>().DisableVelocitySetting(0.2f);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-a * 4f, 3f), ForceMode2D.Impulse);
        }
    }

    public void FillHealthBar()
    {
        currentLife = maxLife;
    }
}
