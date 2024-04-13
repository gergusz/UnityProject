using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public int maxLife=4;
    [SerializeField]
    public int currentLife=0;

    private float timeToRegenALife = 2f;
    private float regenProgress = 2f;
 
    // Start is called before the first frame update
    void Start()
    {
        FillHealthBar();
    }


    // Update is called once per frame
    void Update()
    {
        if (currentLife < maxLife)
        {
            Regen();
        }
        if (currentLife <= 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerRespawn>().InitiateRespawn();
            gameObject.SetActive(false);

        }
    }

    void Regen()
    {
        if (regenProgress < 0)
        {
            currentLife++;
        }
        else
        {
            regenProgress -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentLife--;
            regenProgress = timeToRegenALife;
        }
    }

    public void FillHealthBar()
    {
        currentLife = maxLife;
    }
}
