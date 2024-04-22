using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Item
{

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 30f;

    private Transform firePoint;
    private List<GameObject> arrows = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        firePoint = GameObject.Find("Fej").transform;
        useCd = 0.1f;

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
            arrow.transform.position = new Vector3(firePoint.position.x + (_facingLeft?-1f:+1f), firePoint.position.y, firePoint.position.z);
            arrow.transform.rotation = _facingLeft ? arrowPrefab.transform.rotation * Quaternion.Euler(0, 0, 180) : arrowPrefab.transform.rotation;
            rb.velocity = (_facingLeft ? Vector2.left : Vector2.right) * arrowSpeed;
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
