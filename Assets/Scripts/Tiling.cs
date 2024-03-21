using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{
    private bool hasLeft = false;
    private bool hasRight = false;
  
    private Transform tilesTrans;
    private Camera cam;
    private float spriteWidth = 0f;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = Mathf.Abs(spriteRenderer.sprite.bounds.size.x * tilesTrans.localScale.x);
    }
    private void Awake()
    {
        cam = Camera.main;
        tilesTrans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasLeft || !hasRight)
        {
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            float edgeVisiblePositonRight = (tilesTrans.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositonLeft = (tilesTrans.position.x - spriteWidth / 2) + camHorizontalExtend;

            if (cam.transform.position.x >= edgeVisiblePositonRight && !hasRight)
            {
                makeNewBuddy(false);
                hasRight = true;

            }else if (cam.transform.position.x <= edgeVisiblePositonLeft && !hasLeft)
            {
                makeNewBuddy(true);
                hasLeft = true;
            }
        } 
    }

    void makeNewBuddy(bool left)
    {
        Vector3 newPosition = new Vector3(tilesTrans.position.x + spriteWidth * (left ? -1 : 1),
                                            tilesTrans.position.y, tilesTrans.position.z);
        Transform newBuddy = Instantiate(tilesTrans, newPosition, tilesTrans.rotation) as Transform;

        newBuddy.parent = tilesTrans.parent;
        if (left)
        {
            newBuddy.GetComponent<Tiling>().hasRight = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasLeft = true;
        }
    }
}
