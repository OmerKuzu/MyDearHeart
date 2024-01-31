using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
      [SerializeField] private float takipMesafesi = 5f;
      [SerializeField] private float mermiAtisMesafesi = 10f;
      [SerializeField] private float hareketHizi = 5f;
      [SerializeField] private bool atesEdebilirmi;
    
      [SerializeField] private Transform mermiCikisNoktasi;
      [SerializeField] private float atesEtmeAraligi = .25f;
      private float atesEtmeSayac;
     private EnemyBullet enemyBullet;

private Vector3 hareketYonu;
      private Rigidbody2D rb;
      private Animator anim;
      private KnockBack knockBack;
      private Camera mainCamera;

      
      private void Awake()
      {
            
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            knockBack = GetComponent<KnockBack>();
            mainCamera = Camera.main;
      }

      private void Update()
      
      {
            if (!PlayerHareketController.instance || knockBack.geriTepkiOlsunmu)    
            {
                  return;
            }
            
            Vector3 enemyScreenPos = mainCamera.WorldToScreenPoint(transform.position);
            if (enemyScreenPos.x > 0 && enemyScreenPos.x < Screen.width && enemyScreenPos.y > 0 &&
                enemyScreenPos.y < Screen.height)
            {
                  if (MesafeOlcFNC() <takipMesafesi)
                  {
                        hareketYonu = PlayerHareketController.instance.transform.position - transform.position;
                  }
                  else
                  {
                        hareketYonu = Vector3.zero;
                  }
                  hareketYonu.Normalize();
                  rb.velocity = (hareketYonu * hareketHizi);

                  if (PlayerHareketController.instance.transform.position.x>transform.position.x)
                  {
                        transform.localScale = new Vector3(-1, 1, 1);
                  }
                  else
                  {
                        transform.localScale = Vector3.one;
                  }
            
            
            
            

                  if (rb.velocity!=Vector2.zero)
                  {
                        anim.SetBool("hareketEtsinmi",true);
                  }
                  else
                  {
                        anim.SetBool("hareketEtsinmi",false);
                  }
            
                  //Mermi İle İlgili İşlemler
                  if (MesafeOlcFNC() < mermiAtisMesafesi)
                  {
                        if (Time.time > atesEtmeSayac && PlayerHareketController.instance.gameObject.activeInHierarchy)
                        {
                              atesEtmeSayac = Time.time + atesEtmeAraligi;
                              ShootBullet();
                        }
                  }  
            }
            
            
          
                  
          
      }

      void ShootBullet()
      {
            enemyBullet = ObjectPool.instance.EnemyMermiCikarFNC();
            if (enemyBullet)
            {
                  enemyBullet.transform.position = mermiCikisNoktasi.position;
                  enemyBullet.transform.rotation = mermiCikisNoktasi.rotation;
                  enemyBullet.gameObject.SetActive(true);
            }
      }

      private void OnDrawGizmosSelected()
      {
            Gizmos.color=Color.green;
            Gizmos.DrawWireSphere(transform.position,takipMesafesi);
            
            Gizmos.color=Color.red;
            Gizmos.DrawWireSphere(transform.position,mermiAtisMesafesi);
      }

      float MesafeOlcFNC()
      {
            return Vector2.Distance(PlayerHareketController.instance.transform.position, transform.position);
      }
}