using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour
{
    private GameObject[] mainInventory;
    private int activeItem;
    public GameObject[] items;
    private Inventory inventoryComponent;
    public GameObject panePrefab;
    private List<GameObject> accPanes = new();

    void Awake()
    {
        mainInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().mainInventory;
        inventoryComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        activeItem = inventoryComponent.activeItem;
        for (int i = 0; i < mainInventory.Length; i++)
        {
            if (mainInventory[i] != null)
            {
                items[i].GetComponent<Image>().enabled = true;
                items[i].GetComponent<Image>().sprite = mainInventory[i].GetComponent<SpriteRenderer>().sprite;
            } else
            {
                items[i].GetComponent<Image>().enabled = false;
            }

            if (i == activeItem)
            {
                items[i].transform.parent.gameObject.GetComponent<Image>().color = Color.red;
            } else
            {
                items[i].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            }
        }


    }

    public void SwitchToItem(int slotNumber)
    {
        if (mainInventory[inventoryComponent.activeItem])
        {
            mainInventory[inventoryComponent.activeItem].SetActive(false);
        }
        inventoryComponent.activeItem = slotNumber;
        inventoryComponent.ChangeItem();
    }

    public void CreateAccessoryPane(GameObject accessory)
    {
        var pane = Instantiate(panePrefab);
        accPanes.Add(pane);
        pane.transform.SetParent(transform, false);
        pane.transform.GetChild(0).GetComponent<Image>().sprite = accessory.GetComponent<SpriteRenderer>().sprite;
        ((RectTransform)pane.transform).anchorMin = Vector2.right;
        ((RectTransform)pane.transform).anchorMax = Vector2.right;
        ((RectTransform)pane.transform).anchoredPosition = new Vector2(-55, accPanes.Count()*55);
    }

    public void DestroyAccessoryPane(GameObject accessory)
    {
        Destroy(accPanes.Last());
        accPanes.Remove(accPanes.Last());
    }
}
