using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{    
    [SerializeField]
    private Item[] mainInventory = new Item[10];
    private int activeItem = 0;
    Transform firePoint;

    private float itemsUseCd = 1f;
    private float useCdRemaining = 0f;

    private GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        firePoint = GameObject.Find("Point").transform;
        gameController = GameObject.FindGameObjectWithTag("GameController");
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
        if (Input.GetMouseButton(0)&& gameController.GetComponent<PlayerRespawn>().IsPlayerAlive() && mainInventory[activeItem] && Mathf.Abs(useCdRemaining)<0.01)
        {
            mainInventory[activeItem].Use();
            useCdRemaining = itemsUseCd;
        }
        if (Mathf.Abs(useCdRemaining) > 0.01)
        {
            useCdRemaining -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.G) && mainInventory[activeItem])
        {
            ThrowItem();
        }
    }

    void ChangeItem()
    {
        
        var kid_sb = firePoint.GetComponent<SpriteRenderer>();
        if (mainInventory[activeItem] != null)
        {
            kid_sb.sprite = mainInventory[activeItem].gameObject.GetComponent<SpriteRenderer>().sprite;
            kid_sb.flipX = true;
            itemsUseCd = mainInventory[activeItem].useCd;
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

    public void ThrowItem()
    {
        var thrown = mainInventory[activeItem].gameObject;
        mainInventory[activeItem] = null;
        ChangeItem();
     
        thrown.SetActive(true);
        bool _facingLeft = gameObject.GetComponent<Movement>()._facingLeft;
        thrown.transform.position = new Vector3(transform.position.x + (_facingLeft?-2f:2f) , firePoint.position.y, transform.position.z);
        thrown.GetComponent<Rigidbody2D>().AddForce(new Vector2(5f*(_facingLeft?-1:+1), 1f), ForceMode2D.Impulse);
        
    }
}
