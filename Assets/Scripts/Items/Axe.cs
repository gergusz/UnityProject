using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Item
{

    private void Start()
    {
        useCd = 0.5f;
    }
    public override void Use()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit)
        {  
            if (hit.transform.tag == "Tree")
            {
                hit.transform.gameObject.GetComponent<TreeCutDown>().IncreaseCutDown();
            }

        }
    }
}
