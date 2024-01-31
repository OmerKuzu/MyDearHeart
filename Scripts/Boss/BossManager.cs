
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Object = System.Object;


public class BossManager : MonoBehaviour
{

   [SerializeField] private float hareketHizi = 10f;

   [SerializeField] private Transform[] positions;

   private Transform hedefPos;
   [SerializeField] private float beklemeSuresi = 2f;

   private bool hareketEtsinmi = true;

   private int kacinciPos;

   private Animator anim;

   [Header("Duzenli Mermi Ayarları")] 
  
   [SerializeField] private float mermiHizi = 10f;

   [SerializeField] private int mermiAdet;
   [SerializeField] private float ikiMermiArasiSure;
   [SerializeField] private float gecikmeZamani = 1f;

   [Header("Belli bir açı ile mermi fırlatma ayarları")] [SerializeField]
   private int mermiPatlamaAdet;

   [SerializeField] [Range(0, 359)] private float yayilmaAngle;
   [SerializeField] private float baslangicUzakligi = .1f;



   private bool atesEtsinmi = false;
   private Camera mainCamera;


   private void Awake()
   {
      anim = GetComponent<Animator>();
      mainCamera = Camera.main;
   }

   private void Start()
   {
      yeniHedefOlusturFNC();
   }

   private void Update()
   {
      if (!hareketEtsinmi)
         return;
     
      
      Vector3 enemyScreenPos = mainCamera.WorldToScreenPoint(transform.position);
      if (enemyScreenPos.x > 0 && enemyScreenPos.x < Screen.width && enemyScreenPos.y > 0 &&
          enemyScreenPos.y < Screen.height)
      {
         HareketEtFNC();
      }
   }
   
   

   IEnumerator BelirliBirAciIleMermiFirlatFNC()
   {
      atesEtsinmi = true;
      Vector2 hareketYonu = (Vector2)(PlayerHareketController.instance.transform.position - transform.position);

      float hedefAngle = Mathf.Atan2(hareketYonu.y, hareketYonu.x) * Mathf.Rad2Deg;

      float gecerliAngle = hedefAngle;
      float baslangicAngle = hedefAngle;
      float yarimYayilmaAngle = 0f;
      float angleSayac = 0f;

      if (yayilmaAngle != 0)
      {
         angleSayac = yayilmaAngle / (mermiAdet - 1);
         yarimYayilmaAngle = yayilmaAngle / 2f;
         baslangicAngle = hedefAngle - yarimYayilmaAngle;
         gecerliAngle = baslangicAngle;
      }

      for (int i = 0; i < mermiAdet; i++)
      {
         for (int j = 0; j < mermiPatlamaAdet; j++)
         {
            Vector2 pos = MermiFirlatmaPosBelirle(gecerliAngle);


            BossBullet yeniMermi = ObjectPool.instance.BossMermiCikarFNC();
            if (yeniMermi!=null)
               
               yeniMermi.transform.position = (Vector3)pos;
               yeniMermi.gameObject.SetActive(true);
               yeniMermi.MermiHiziniDegistir(mermiHizi);
               yeniMermi.transform.right = yeniMermi.transform.position - transform.position;
            


            gecerliAngle += angleSayac;
         }

         gecerliAngle = baslangicAngle;
         yield return new WaitForSeconds(ikiMermiArasiSure);
      }

      yield return new WaitForSeconds(gecikmeZamani);
      atesEtsinmi = false;
   }

   Vector2 MermiFirlatmaPosBelirle(float gecerliAngle)
   {
      float x = transform.position.x + baslangicUzakligi * Mathf.Cos(gecerliAngle * Mathf.Deg2Rad);
      float y = transform.position.y + baslangicUzakligi * Mathf.Sin(gecerliAngle * Mathf.Deg2Rad);
      Vector2 pos = new Vector2(x, y);
      return pos;
   }




void MermiFirlatFNC()
   {
      if (!PlayerHareketController.instance.gameObject.activeInHierarchy)
         return;

      float randomBullet = Random.value;
      if (!atesEtsinmi)
      {
         if (randomBullet<=0.5f)
         {
            StartCoroutine(BelirliBirAciIleMermiFirlatFNC());
         }
         else
         {
            StartCoroutine(DuzenliAtesEtRoutine());
         }
      }

   }

   IEnumerator DuzenliAtesEtRoutine()
   {
      atesEtsinmi = true;
      for (int i = 0; i < mermiAdet; i++)
      {
         Vector2 hareketYonu = (Vector2)(PlayerHareketController.instance.transform.position - transform.position);

         BossBullet yeniMermi = ObjectPool.instance.BossMermiCikarFNC();
      yeniMermi.transform.position = transform.position;
         yeniMermi.transform.right = (Vector3)hareketYonu;
         yeniMermi.MermiHiziniDegistir(mermiHizi);
         yeniMermi.gameObject.SetActive(true);

         yield return new WaitForSeconds(ikiMermiArasiSure);
      }

      yield return new WaitForSeconds(gecikmeZamani);
      atesEtsinmi = false;
   }


void HareketEtFNC()
   {
      if (PlayerHareketController.instance.gameObject.activeInHierarchy)
      {
         transform.position = Vector3.MoveTowards(transform.position, hedefPos.position, hareketHizi * Time.deltaTime);

         if (Vector3.Distance(transform.position, hedefPos.position) < .1f && hareketEtsinmi)
         {
            hareketEtsinmi = false;
            transform.position = hedefPos.position;
            StartCoroutine(routine: AzBekleHareketEtRoutine());
         }
      }
      else
      {
         GetComponent<Rigidbody2D>().velocity = Vector2.zero;
         anim.Play("UpDownAnim");
      }
   }

IEnumerator AzBekleHareketEtRoutine()
       {
          anim.Play("UpDownAnim");
          MermiFirlatFNC();
          yield return new WaitForSeconds(beklemeSuresi);
          hareketEtsinmi = true;
          yeniHedefOlusturFNC();
          
      
       }
    
    
       void yeniHedefOlusturFNC()
       {
          hedefPos = positions[kacinciPos];

          if (hedefPos.position.x>transform.position.x)
          {
             anim.Play("RightAnim");
          }else if (hedefPos.position.x<transform.position.x)
          {
               anim.Play("LeftAnim");
          }
          
          
          kacinciPos++;
          if (kacinciPos >= positions.Length)
             kacinciPos = 0;
          {
             
          }
       }
       
     
    }


