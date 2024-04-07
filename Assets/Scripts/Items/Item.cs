using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<Inventory>().AddItem(this))
                this.gameObject.SetActive(false);
        }
    }
    protected virtual void Use()
    {

    }
}
