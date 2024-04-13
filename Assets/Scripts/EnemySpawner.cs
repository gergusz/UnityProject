using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBox : MonoBehaviour
{
    private Transform playerTrans;
    private float spawntime=0f;
    [SerializeField]
    private GameObject[] enemies = new GameObject[2];
    [SerializeField]
    private List<GameObject> enemyPool = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        foreach (var enemyType in enemies)
        {
            for (int i = 0; i < 10; i++)
            {
                enemyPool.Add(Instantiate(enemyType));
                enemyPool[^1].gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (spawntime < 0f)
        {
            spawntime = Random.Range(1f, 3f);
            SpawnSomething();
        }
        else
        {
            spawntime -= Time.fixedDeltaTime;
        }
    }
    void SpawnSomething()
    {
        var tospawn = Mathf.RoundToInt(Random.Range(0f, enemyPool.Count-1));
        if (!enemyPool[tospawn].gameObject.activeSelf)
        {
            float xPos = playerTrans.position.x;
            xPos += Random.Range(3f, 15f) *(Random.Range(0f, 1f)>0.5f?1f:-1f);
            Vector3 whereToSpawn = new Vector3(xPos, playerTrans.position.y, 0f) ;
            enemyPool[tospawn].transform.position = whereToSpawn;
            enemyPool[tospawn].gameObject.SetActive(true);
        }
    }
}
