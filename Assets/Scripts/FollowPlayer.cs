using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset; 
    // Start is called before the first frame update
    void Start()
    {
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 anchorPos = transform.position+offset;
            Vector3 movement = target.position - anchorPos;

            Vector3 newCamPos = transform.position + movement * speed * Time.deltaTime;

            if (newCamPos.y > 34.0f)
            {
                newCamPos.y = 34.0f;
            } else if (newCamPos.y < -45.0f)
            {
                newCamPos.y = -45.0f;
            }

            transform.position = newCamPos;
        }
    }
    private void FixedUpdate()
    {

    }

}
