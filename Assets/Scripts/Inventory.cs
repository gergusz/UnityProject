using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    private Item[] mainInventory = new Item[10];
    [SerializeField]
    private int activeItem = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < mainInventory.Length; i++)
        {
            mainInventory[i] = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scrollDelta = Input.mouseScrollDelta;
        if(scrollDelta.y != 0f)
        {


            if(activeItem==0 && scrollDelta.y < 0f)
            {
                 activeItem = mainInventory.Length-1;
            }
            else if(activeItem == mainInventory.Length-1 && scrollDelta.y > 0f)
            {
                activeItem = 0;
            }
            else
            {
                activeItem += scrollDelta.y > 0f ? 1 : -1;
            }
            
            ChangeItem();
        }
        
        
    }

    void ChangeItem()
    {
        var kid = transform.Find("Point");
        var kid_sb = kid.GetComponent<SpriteRenderer>();
        if (mainInventory[activeItem] != null)
        {
            kid_sb.sprite = mainInventory[activeItem].gameObject.GetComponent<SpriteRenderer>().sprite;
            kid_sb.flipX = true;
        }
        else
        {
            kid_sb.sprite = null;
        }
    }


    public bool AddItem(Item item)
    {
        for (var i = 0; i < mainInventory.Length; i++)
        {
            if (mainInventory[i]==null)
            {
                mainInventory[i] = item;
                ChangeItem();
                return true;
            }
        }
        return false;
    }
}
