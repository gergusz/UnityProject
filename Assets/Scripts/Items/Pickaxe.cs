using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Item
{

    private void Start()
    {
        useCd = 0.1f;
    }

    public override void Use()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit)
        {
            print(hit.transform.name);
            if (hit.transform.tag == "Block")
            {
                
            }
            
        }
    }
}
