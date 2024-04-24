using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Accessory
{
    public override void Equip(GameObject player)
    {
        player.GetComponent<PlayerTakeDamage>().maxLife += 1;
    }

    public override void Unequip(GameObject player)
    {
        player.GetComponent<PlayerTakeDamage>().maxLife -= 1;
        if (player.GetComponent<PlayerTakeDamage>().currentLife > player.GetComponent<PlayerTakeDamage>().maxLife)
        {
            player.GetComponent<PlayerTakeDamage>().Hurt();
        }
    }
}
