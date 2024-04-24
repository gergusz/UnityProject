using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Accessory
{
    public override void Equip(GameObject player)
    {
        player.GetComponent<Movement>().canDash = true;
    }

    public override void Unequip(GameObject player)
    {
        player.GetComponent<Movement>().canDash = false;
    }
}
