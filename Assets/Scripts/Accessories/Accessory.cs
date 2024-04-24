using UnityEngine;
public class Accessory : Item
{

    public virtual void Equip(GameObject player){}

    public virtual void Unequip(GameObject player){}

    public override void CollisionHandler(Collider2D collision)
    {
        gameObject.SetActive(false);
        var colliderek = gameObject.GetComponentsInChildren<BoxCollider2D>();
        foreach (var collider in colliderek)
        {
            collider.enabled = false;
        }
        collision.gameObject.GetComponent<AccessoryInventory>().AddAccessory(gameObject);
    }
}