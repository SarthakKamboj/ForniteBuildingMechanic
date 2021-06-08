using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(gameObject.name + " col");
    }
}
