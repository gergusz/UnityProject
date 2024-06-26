using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgParralax : MonoBehaviour
{
    [SerializeField]
    private GameObject characterToMove=null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MoveChar()
    {


         Vector3 vel_of_character= characterToMove.GetComponent<Rigidbody2D>().velocity;
            vel_of_character = new Vector3(vel_of_character.x, 0f, 0f);
            transform.Translate(-vel_of_character * Mathf.Abs(10 / transform.position.z) * Time.deltaTime, Camera.main.transform);
 
    }

    // Update is called once per frame
    void Update()
    {
        if (characterToMove == null)
            transform.Translate(Vector3.right * (10/transform.position.z) * Time.deltaTime, Camera.main.transform);
        else
            MoveChar();

    }
}
