using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Item
{

  
    private CircleCollider2D kardBox;

    private void Start()
    {
        useCd = 0.3f;
        kardBox = gameObject.GetComponent<CircleCollider2D>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !gameObject.GetComponent<Rigidbody2D>().isKinematic)
        {
            CollisionHandler(collision);
        }
        if (collision.CompareTag("Enemy")&&gameObject.GetComponent<Rigidbody2D>().isKinematic)
        {

            collision.gameObject.GetComponent<EnemyDamageDespawn>().Hurt(1);
            var a = collision.transform.position.x - transform.position.x;
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(a*4f, 3f), ForceMode2D.Impulse);
        }
    }

    public override void CollisionHandler(Collider2D collision)
    {
        base.CollisionHandler(collision);
        transform.localRotation = Quaternion.Euler(0f, 0f,  103f);

        transform.localPosition = new Vector3(transform.localPosition.x, -0.1f);
        transform.localScale = new Vector2(1f, 1f);
    }

    public override void Use()
    {
 
        StartCoroutine(Slash());
  
    }
    public IEnumerator Slash()
    {
        kardBox.radius = 1f;
        kardBox.offset = new Vector2(0.5f, 0.5f);
        kardBox.enabled = true;
        kardBox.isTrigger = true;
    
        var players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {

            kardBox.excludeLayers = players[0].layer;
        }
        yield return new WaitForSeconds(useCd-0.1f);

        kardBox.enabled = false;
    }
    private void OnDisable()
    {
        kardBox.enabled = false;
    }


}
