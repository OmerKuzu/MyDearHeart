
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float mermiHizi = 10f;
    [SerializeField] private int  mermiHasarGucu = 10 ;
    
    
    private Rigidbody2D rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb != null)
        {
            rb.velocity = (Vector2)(transform.right * mermiHizi);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealthController>().hasarAlFNC(mermiHasarGucu);
            this.gameObject.SetActive(false);
        }
        if (other.CompareTag("Wall"))
        {
            GameObject duvarEfecti = null;
            duvarEfecti = ObjectPool.instance.DuvarMermiEfectCikar();
            duvarEfecti.transform.position = this.transform.position;
            duvarEfecti.transform.rotation = Quaternion.identity;
            duvarEfecti.gameObject.SetActive(true);
            
            this.gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}

