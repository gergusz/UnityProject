using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Item
{

    [SerializeField] private GameObject arrowPrefab;
    private float arrowSpeed = 15f;

    private Transform firePoint;
    private List<GameObject> arrows = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        firePoint = GameObject.Find("Fej").transform;
        useCd = 0.3f;

        for (var i = 0; i < 10; i++)
        {
            var arrow = Instantiate(arrowPrefab, firePoint.position, arrowPrefab.transform.rotation);
            arrow.gameObject.SetActive(false);
            arrows.Add(arrow);
        }
    }

    // Update is called once per frame
    public override void Use()
    {
        bool _facingLeft = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>()._facingLeft;
        
        var arrow = GetArrow();

        if (arrow)
        {
            arrow.gameObject.SetActive(true);
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var playerPos = GameObject.FindGameObjectWithTag("Player").transform;

            if (_facingLeft &&mousepos.x>transform.position.x)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().Flip();
            }
            else if(!_facingLeft && mousepos.x < transform.position.x)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().Flip();
            }

            _facingLeft = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>()._facingLeft;
            arrow.transform.localRotation = Quaternion.identity;
            arrow.transform.position = new Vector3(playerPos.position.x + (_facingLeft ? -1f : +1f) , playerPos.position.y, playerPos.position.z);
            
            //arrow.transform.rotation = _facingLeft ? arrowPrefab.transform.rotation * Quaternion.Euler(0, 0, 180) : arrowPrefab.transform.rotation;
            //rb.velocity = (_facingLeft ? Vector2.left : Vector2.right) * arrowSpeed;

            var destination = mousepos -playerPos.position;
            destination.z = 0f;
            float angle = Mathf.Atan2(destination.y, destination.x);
            angle *= Mathf.Rad2Deg;
            arrow.transform.eulerAngles = new Vector3(0, 0, angle-45);

            
            rb.AddForce(destination.normalized * arrowSpeed, ForceMode2D.Impulse);
        }
    }

    private GameObject GetArrow()
    {
        foreach (GameObject arrow in arrows)
        {
            if (!arrow.gameObject.activeSelf)
            {
                return arrow;
            }
        }

        return null;
    }   
}
