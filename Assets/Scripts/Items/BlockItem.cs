using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItem : Item
{
    [SerializeField] GameObject blockPrefab;

    private void Start()
    {
        useCd = 0.5f;
    }

    public override void Use()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var normalisedX = Mathf.Floor(mousePos.x) + 0.5f;
        var normalisedY = Mathf.Floor(mousePos.y) + 0.5f;

        if (Vector2.Distance(mousePos, new(normalisedX, normalisedY))> 4.5f)
        {
            return;
        }

        if (Physics2D.Raycast(new Vector2(normalisedX, normalisedY), Vector2.zero))
        {
            return;
        }

        Instantiate(blockPrefab, new Vector2(normalisedX, normalisedY), blockPrefab.transform.rotation);

        GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().mainInventory[GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().activeItem] = null;
        gameObject.transform.parent = null;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().ChangeItem();
        Destroy(gameObject);
    }
}
