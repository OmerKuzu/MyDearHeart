using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;
public class PickUpManager : MonoBehaviour
{
   private int kazanSayac = 2;
   [SerializeField] private GameObject flyTxtPrefab;

   [Header("Coin Ayarlari")]
   [SerializeField] private float coinUzaklik = 5f;
   [SerializeField] private float moveSpeed = 2f;
   [SerializeField] private float artisHizi = .3f;
   private Rigidbody2D rb;
   private Vector3 coinYonu;

   [Header("Sandik Ayarlari")] 
   [SerializeField] private GameObject coinPrefab;
   [SerializeField] private Sprite sandikAcik, sandikBos;
   private void Awake()
   {
      if (PickUpItem == PickUpItems.Coin)
      {
         rb = GetComponent<Rigidbody2D>();
      }
   }

   public enum PickUpItems
   {
      Zirh,
      Can,
      Kazan,
      Coin,
      Sandik
   }

   public PickUpItems PickUpItem;

   private void Update()
   {
      if (PickUpItem == PickUpItems.Coin)
      {
         Vector3 playerPos = PlayerHareketController.instance.transform.position;
         if (Vector3.Distance(transform.position, playerPos) < coinUzaklik)
         {
            coinYonu = (playerPos - transform.position).normalized;
            moveSpeed += artisHizi;
         }
         else
         {
            coinYonu = Vector3.zero;
            moveSpeed = 0;
         }
      }
   }

   private void FixedUpdate()
   {
      if (rb != null && PickUpItem == PickUpItems.Coin)
      {
         rb.velocity = (coinYonu * moveSpeed);
      }
   }

   public void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         if (PickUpItem == PickUpItems.Zirh)
         {
            PlayerHealthController.instance.ZirhiArtirFNC(1);
         }else if (PickUpItem == PickUpItems.Can)
         {
            PlayerHealthController.instance.CaniArtirFNC(1);
         }else if (PickUpItem == PickUpItems.Coin)
         {
            GameManager.instance.CoinArtir(1);
         }
         {
            
         }
         Destroy(gameObject);
      }

      if (other.CompareTag("PlayerBullet"))
      {
         if (PickUpItem == PickUpItems.Kazan)
         {
            if (flyTxtPrefab)
            {
               Instantiate(flyTxtPrefab, transform.position, Quaternion.identity);
            }
            
            GameManager.instance.PuanArtir(25);
            kazanSayac--;
            
            
            if (kazanSayac == 1)
            {
               GetComponent<Animator>().SetTrigger("kazanSondu");
            }
            else if(kazanSayac==0)
            {
               Destroy(gameObject);
            }
         }

         if (PickUpItem==PickUpItems.Sandik)
         {
            if (coinPrefab)
            {
               for (int i = 0; i < 3; i++)
               {
                  float koordX = (i - 1) * .7f;
                  Vector3 pos = new Vector3(transform.position.x+koordX, transform.position.y, transform.position.z);
                  GameObject coin = Instantiate(coinPrefab, pos, Quaternion.identity);
                  coin.transform.DOMoveY(this.transform.position.y + 1f, .3f);
                  GetComponent<SpriteRenderer>().sprite = sandikAcik;
                  GetComponent<BoxCollider2D>().enabled = false;
               }

               StartCoroutine(SandikBosaltRoutine());
            }
         }
      }
      
   }


   IEnumerator SandikBosaltRoutine()
   {
      yield return new  WaitForSeconds(.3f);
      GetComponent<SpriteRenderer>().sprite = sandikBos;
      yield return new WaitForSeconds(.5f);
      GetComponent<SpriteRenderer>().DOFade(0, .3f);
   }
}
