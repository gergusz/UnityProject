using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Item
{

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 15f;

    private Transform firePoint;

    // Start is called before the first frame update
    private void Start()
    {
        firePoint = GameObject.Find("Fej").transform;
        useCd = 1.0f;
    }

    // Update is called once per frame
    public override void Use()
    {
        bool _facingLeft = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>()._facingLeft;
        GameObject arrow = Instantiate(arrowPrefab, new Vector3(firePoint.position.x + (_facingLeft?-1f:+1f), firePoint.position.y, firePoint.position.z), arrowPrefab.transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = (_facingLeft ? Vector2.left : Vector2.right) * arrowSpeed;
    }
}
