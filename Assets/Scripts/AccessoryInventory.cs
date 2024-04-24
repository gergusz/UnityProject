using System.Collections;
using System.Collections.Generic;
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
}
