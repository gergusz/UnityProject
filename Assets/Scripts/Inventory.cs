using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{    
    [SerializeField]
    public GameObject[] mainInventory = new GameObject[20];
    public int activeItem = 0;
    Transform firePoint;

    private float itemsUseCd = 1f;
    [SerializeField] private float useCdRemaining = 0f;

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
  
            if (mainInventory[activeItem])
            {
                mainInventory[activeItem].SetActive(false);
            }

            if (activeItem==0 && scrollDelta.y < 0f)
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
        if (Input.GetMouseButton(0)&& gameController.GetComponent<PlayerRespawn>().IsPlayerAlive() && mainInventory[activeItem] && useCdRemaining<=0.01)
        {
            mainInventory[activeItem].GetComponent<Item>().Use();
            useCdRemaining = itemsUseCd;
        }
        if (useCdRemaining > 0.01)
        {
            useCdRemaining -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.G) && mainInventory[activeItem])
        {
           ThrowItem();
        }
    }

    public void ChangeItem()
    {
        
        if (mainInventory[activeItem] != null)
        {
            mainInventory[activeItem].SetActive(true);
            itemsUseCd = mainInventory[activeItem].GetComponent<Item>().useCd;
        }

    }


    public bool AddItem(Item item)
    {
        for (var i = 0; i < mainInventory.Length; i++)
        {
            if (mainInventory[i]==null)
            {
                mainInventory[i] = item.gameObject;
                mainInventory[i].transform.parent = firePoint;
                mainInventory[i].transform.localPosition = new Vector3(0,0,-1);
                mainInventory[i].GetComponent<Rigidbody2D>().isKinematic = true;
                
                
                return true;
            }
        }
        return false;
    }

    
    public void ThrowItem()
    {
        var thrown = mainInventory[activeItem];
        mainInventory[activeItem] = null;
        ChangeItem();

        thrown.transform.parent = null;
        thrown.GetComponent<Rigidbody2D>().isKinematic = false;
        thrown.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        var colliderek = thrown.GetComponentsInChildren<BoxCollider2D>();
        foreach (var collider in colliderek)
        {
            collider.enabled = true;
        }
        thrown.SetActive(true);

        bool _facingLeft = gameObject.GetComponent<Movement>()._facingLeft;
        thrown.transform.position = new Vector3(transform.position.x + (_facingLeft?-1.5f:1.5f) , firePoint.position.y, transform.position.z);
        thrown.GetComponent<Rigidbody2D>().AddForce(new Vector2(5f*(_facingLeft?-1:+1), 1f), ForceMode2D.Impulse);
        
    }
}
