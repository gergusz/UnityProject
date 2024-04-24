using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlime : EnemyDamageDespawn
{

    [SerializeField] private float invulTime = 0f;

    [SerializeField] private float maxInvulTime = 0.5f;

    [SerializeField] private List<GameObject> lootTable = new();

    [SerializeField] private int lootAmount = -1;

    private Animator anim;

    

    private void Awake()
    {
        maxLife = 10;
        currentLife = maxLife;
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invulTime > 0)
        {
            invulTime -= Time.deltaTime;
        }
    }

    public override void Hurt(int damage)
    {
        if (invulTime > 0)
        {
            anim.SetTrigger("isInvul");
            return;
        }

        invulTime = maxInvulTime;
        anim.ResetTrigger("isInvul");
        anim.ResetTrigger("isHurt");
        anim.SetTrigger("isHurt");

        currentLife -= damage;
        if (currentLife <= 0)
        {
            if (lootAmount == -1)
            {
                lootAmount = UnityEngine.Random.Range(1, lootTable.Count + 1);
            }
            for (int i = 0; i < lootAmount; i++)
            {
                GameObject loot = Instantiate(lootTable[UnityEngine.Random.Range(0, lootTable.Count)], transform.position, Quaternion.identity);
                loot.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f)) * 100);
            }
            gameObject.SetActive(false);
        }
    }
}