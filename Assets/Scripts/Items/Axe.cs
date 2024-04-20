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
            var playersPos = Vector2.zero;
            var players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 0)
            {
                playersPos = players[0].transform.position;
            }
            var blockpos = hit.transform.GetChild(2).transform.position;

            if (hit.transform.tag == "Tree" && Vector2.Distance(blockpos, playersPos) < 4f) 
            {
                
                hit.transform.gameObject.GetComponent<TreeCutDown>().IncreaseCutDown();
            }

        }
    }

}