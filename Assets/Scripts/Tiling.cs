using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{
    private bool hasLeft = false;
    private bool hasRight = false;
  
    private Transform tilesTrans;
    private Camera cam;
    private float checkingWidth = 100f;
    private float checkingHeight = 100f;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Collider2D collider = GetComponent<Collider2D>();
            checkingWidth = Mathf.Abs(collider.bounds.size.x * tilesTrans.localScale.x);
            checkingHeight = Mathf.Abs(collider.bounds.size.y * tilesTrans.localScale.y);
        }
        else
        {
            checkingWidth = Mathf.Abs(spriteRenderer.sprite.bounds.size.x * tilesTrans.localScale.x);
            checkingHeight = Mathf.Abs(spriteRenderer.bounds.size.y * tilesTrans.localScale.y);
        }
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


            float edgeVisiblePositonRight = (tilesTrans.position.x + checkingWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositonLeft = (tilesTrans.position.x - checkingWidth / 2) + camHorizontalExtend;
            
            float camVertical = cam.orthographicSize * Screen.height / Screen.width;

            float topOfCam =(tilesTrans.position.y+checkingHeight/2) - camVertical;
            float bottomOfCam = (tilesTrans.position.y - checkingHeight / 2) + camVertical;
            if (cam.transform.position.y-20f >= topOfCam || cam.transform.position.y+20f<=bottomOfCam )
            {

            }
            else if (cam.transform.position.x >= edgeVisiblePositonRight && !hasRight)
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
        Vector3 newPosition = new Vector3(tilesTrans.position.x + checkingWidth * (left ? -1 : 1),
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
