using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float useCd = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            CollisionHandler(collision);
    }

    public virtual void CollisionHandler(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Inventory>().AddItem(this))
        {
            this.gameObject.SetActive(false);
            var colliderek = gameObject.GetComponentsInChildren<BoxCollider2D>();
            foreach (var collider in colliderek)
            {
                collider.enabled = false;
            }
            collision.gameObject.GetComponent<Inventory>().ChangeItem();
        }
        
    }

    public virtual void Use()
    {

    }
}
