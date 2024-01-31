using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransform : MonoBehaviour
{
    public float speed = 5f;
    private void Update()
    {
        transform.position += transform.right*speed*Time.deltaTime; //(1,0,0)
    }
}


