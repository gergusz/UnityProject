using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDespawn : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxLife = 3;
    public int currentLife = 0;
    void Start()
    {
        
    }
    private void Awake()
    {
        currentLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        Despawn();
    }

    void Despawn()
    {
        var checkPos = Vector3.zero;

        var players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            checkPos = players[0].transform.position;
        }
        if (Vector2.Distance(transform.position, checkPos) > 20f)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void Hurt(int damage)
    {
        currentLife -= damage;
        if (currentLife <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
