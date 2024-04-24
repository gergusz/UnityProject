using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Accessory
{
    public override void Equip(GameObject player)
    {
        player.GetComponent<Movement>().maxJumps += 1;
    }

    public override void Unequip(GameObject player)
    {
        player.GetComponent<Movement>().maxJumps -= 1;
    }
}
