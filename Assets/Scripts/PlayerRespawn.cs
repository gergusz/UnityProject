using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField]
    private float respawnTimer = 1f;
    private bool respawnSequenceStarted = false;
    GameObject playa;
    // Start is called before the first frame update
    void Start()
    {
        playa = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (respawnSequenceStarted && respawnTimer < 0f)
        {
            RespawnPlayer();
        }
        else if (respawnSequenceStarted)
            respawnTimer -= Time.deltaTime;
    }

    private void RespawnPlayer()
    {

        playa.gameObject.SetActive(true);
        playa.GetComponent<PlayerTakeDamage>().FillHealthBar();
        playa.transform.position = new Vector2(0f, 0f);
        respawnTimer = 2f;
        respawnSequenceStarted = false;
    }
    public void InitiateRespawn()
    {
        respawnSequenceStarted = true;
    }
    public bool IsPlayerAlive()
    {
        return !respawnSequenceStarted;
    }

}
