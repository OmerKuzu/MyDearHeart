using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyVelocity : MonoBehaviour
{
   private Rigidbody2D rb;
   public float speed = 10f;

   private void Awake()
   {
      rb = GetComponent<Rigidbody2D>();
      
   }

   private void Update()
   {
       rb.velocity=(Vector2)(transform.right*speed);  
   }
}
