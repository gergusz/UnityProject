using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WorldGen : MonoBehaviour
{
    [SerializeField] private bool generateToLeft = false;
    private bool generated = false;
    [SerializeField] private GameObject objectToGenerate;
    [SerializeField] private GameObject belowToGenerate;
    [SerializeField] private GameObject deepBelowToGenerate;
    [SerializeField] private List<GameObject> oresToGenerate;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private float generationDistance = 10f;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        if (objectToGenerate == null)
        {
            objectToGenerate = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!generated)
        {
            if (Mathf.Abs(cam.transform.position.x - transform.position.x) < generationDistance)
            {
                generated = true;
                float yOffset = 0;
                float randomValue = Random.value;

                if (randomValue < 0.05f)
                {
                    yOffset = -1;
                }
                else if (randomValue > 0.95f)
                {
                    yOffset = 1;
                }
                else
                {
                    float randomValueTree = Random.value;

                    if (randomValueTree < 0.09f)
                    {
                        Instantiate(treePrefab, new Vector2(transform.position.x, transform.position.y + 5.5f), treePrefab.transform.rotation);
                    }
                }

                Vector3 generationPosition = generateToLeft ? new Vector3(transform.position.x - 1, transform.position.y + yOffset, transform.position.z) : new Vector3(transform.position.x + 1, transform.position.y + yOffset, transform.position.z);
                Instantiate(objectToGenerate, generationPosition, Quaternion.identity);

                var currentY = transform.position.y + yOffset;

                var amountOfBelow = Random.Range(-7, -3);

                while (currentY > -50)
                {
                    currentY -= 1;

                    if (currentY > amountOfBelow)
                    {
                        Instantiate(belowToGenerate, new Vector3(generationPosition.x, currentY, generationPosition.z), Quaternion.identity);
                    }
                    else
                    {
                        if (Random.value < 0.06f)
                        {
                            var oreToGenerate = oresToGenerate[Random.Range(0, oresToGenerate.Count)];
                            Instantiate(oreToGenerate, new Vector3(generationPosition.x, currentY, generationPosition.z), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(deepBelowToGenerate, new Vector3(generationPosition.x, currentY, generationPosition.z), Quaternion.identity);
                        }
                    }
                }
            }
        }
        
    }
}
