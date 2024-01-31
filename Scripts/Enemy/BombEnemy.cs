using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombEnemy : MonoBehaviour
{
   [SerializeField] private GameObject bombaPrefab;
   [SerializeField] private float hareketHizi = 10f;

   [SerializeField] private Transform[] bombPositions;

   private Transform hedefPos;

   [SerializeField] private float beklemeSuresi=2f;

   private bool hareketEtsinmi = true;

   private int kacinciPos;
   
   private Camera mainCamera;

   private void Awake()
   {
      mainCamera = Camera.main;
   }

   private void Start()
   {
      yeniHedefOlusturFNC();
   }

   private void Update()
   {
      if(!hareketEtsinmi)
         return;
      
      Vector3 enemyScreenPos = mainCamera.WorldToScreenPoint(transform.position);
      if (enemyScreenPos.x > 0 && enemyScreenPos.x < Screen.width && enemyScreenPos.y > 0 &&
          enemyScreenPos.y < Screen.height)
      {
         transform.position = Vector3.MoveTowards(transform.position, hedefPos.position, hareketHizi * Time.deltaTime);

         if (Vector3.Distance(transform.position, hedefPos.position) < .1f && hareketEtsinmi)
         {
            hareketEtsinmi = false;
            StartCoroutine(AzBekleHareketEtRoutine());
         }
      }



   }

   IEnumerator AzBekleHareketEtRoutine()
   {
      yeniHedefOlusturFNC();
      yield return new WaitForSeconds(beklemeSuresi);
      hareketEtsinmi = true;
      
      bombaOlusturFNC();
   }


   void yeniHedefOlusturFNC()
   {
      hedefPos = bombPositions[kacinciPos];
      kacinciPos++;
      if (kacinciPos >= bombPositions.Length)
         kacinciPos = 0;
      {
         
      }
   }

   void bombaOlusturFNC()
   {
      float rand = Random.value;
      if (rand<1f)
      {
         GameObject bomba = Instantiate(bombaPrefab, transform.position, Quaternion.identity);
         StartCoroutine(bomba.GetComponent<BombaManager>().BombaPatlatFNC(bomba.transform));
      }
   }
}
