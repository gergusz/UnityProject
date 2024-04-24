using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AccessoryInventory : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> accessoryInventory = new();

    public void AddAccessory(GameObject accessory)
    {
        accessoryInventory.Add(accessory);
        accessory.GetComponent<Accessory>().Equip(gameObject);
    }

    public void RemoveAccessory(GameObject accessory)
    {
        accessoryInventory.Remove(accessory);
        accessory.GetComponent<Accessory>().Unequip(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && accessoryInventory.Count > 0)
        {
            RemoveAccessory(accessoryInventory.Last());
        }
    }
}
