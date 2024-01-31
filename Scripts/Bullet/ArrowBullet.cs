using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowBullet : MonoBehaviour
{
   [SerializeField] private float arrowSpeed = 10f;
   
   private Rigidbody2D rb;

   [SerializeField] private int hasarMiktari = 1;

   private void Awake()
   {
      rb = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      rb.velocity = (Vector2)(-transform.right * arrowSpeed);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         other.GetComponent<PlayerHealthController>().hasarAlFNC(hasarMiktari);
      }
      Destroy(gameObject);
   }

   private void OnBecameInvisible()
   {
      Destroy(gameObject);
   }
}

