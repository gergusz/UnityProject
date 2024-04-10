using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCutDown : MonoBehaviour
{
    private int life = 3;
    private int cutdownPhase = 0;

    [SerializeField]
    private GameObject proto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cutdownPhase >= life)
        {
            gameObject.SetActive(false);
            for(var i = 0; i < 2; i++)
            {
                Instantiate(proto, transform.position, transform.rotation);
            }
            cutdownPhase = 0;
        }
    }
    public void IncreaseCutDown()
    {

        cutdownPhase++;
    }
}
