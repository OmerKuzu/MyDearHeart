using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float mermiHizi = 10f;
    [SerializeField] private int hasarMiktari = 1;

    private Vector3 hareketYonu;

    private void Start()
    {
        hareketYonu = PlayerHareketController.instance.transform.position - transform.position;
           hareketYonu.Normalize();
    }

    private void Update()
    {

        transform.position += hareketYonu * mermiHizi * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthController>().hasarAlFNC(hasarMiktari);
        }
        gameObject.SetActive(false);
    }
}
