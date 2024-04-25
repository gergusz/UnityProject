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
    [SerializeField] private GameObject slimeKingPrefab;
    private float generationDistance = 10f;

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
                float chance = Random.value;

                if (chance < 0.05f) //gen below
                {
                    yOffset = -1;
                }
                else if (chance > 0.95f) //gen above
                {
                    yOffset = 1;
                }
                else
                {
                    float treeChance = Random.value;

                    if (treeChance < 0.09f) //gen tree
                    {
                        Instantiate(treePrefab, new Vector2(transform.position.x, transform.position.y + 6f), treePrefab.transform.rotation);
                    }
                }

                Vector3 generationPosition = generateToLeft ? new Vector3(transform.position.x - 1, transform.position.y + yOffset, transform.position.z) : new Vector3(transform.position.x + 1, transform.position.y + yOffset, transform.position.z);
                Instantiate(objectToGenerate, generationPosition, Quaternion.identity);

                var slimeKingChance = Random.value;

                if (slimeKingChance < 0.003f) //gen kingslime
                {
                    Instantiate(slimeKingPrefab, new Vector3(generationPosition.x, generationPosition.y + 10, generationPosition.z), Quaternion.identity);
                }


                var currentY = transform.position.y + yOffset;

                var amountOfBelow = Random.Range(-7, -3);

                while (currentY > -52)
                {
                    currentY -= 1;

                    if (currentY > amountOfBelow)
                    {
                        Instantiate(belowToGenerate, new Vector3(generationPosition.x, currentY, generationPosition.z), Quaternion.identity);
                    }
                    else
                    {
                        var oreChance = Random.value;
                        if (oreChance < 0.06f) //gen ore
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
