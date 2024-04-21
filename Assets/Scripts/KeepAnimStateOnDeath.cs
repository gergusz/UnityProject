using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class KeepAnimStateOnDeath : MonoBehaviour
{

    void Awake()
    {
        GetComponent<Animator>().keepAnimatorStateOnDisable = true;
    }
}
