using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTranslate : MonoBehaviour
{
   //fps 20
   private void Update()
   {
      if (Input.GetKey(KeyCode.Space))
      {
         transform.Translate(translation: (Vector3)(new Vector2(x: 0f, y: 5f) * Time.deltaTime));
      }
   }
}