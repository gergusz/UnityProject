using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private static Camera _camera;
    private static GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _camera.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _camera.transform.position.z);
    }
}
