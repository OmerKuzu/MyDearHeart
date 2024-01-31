using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovePosition : MonoBehaviour
{
   private Rigidbody2D rb;
   public float speed = 200f;

   private void Awake()
   {
      rb = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
       Vector2 yeniPos = (Vector2)(transform.position + (transform.right * Time.deltaTime * speed));
       rb.MovePosition((yeniPos));
           
   }
}
