using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float useCd = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Inventory>().AddItem(this))
                this.gameObject.SetActive(false);
        }
    }
    public virtual void Use()
    {

    }
}
