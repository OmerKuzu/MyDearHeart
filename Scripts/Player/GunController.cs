

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunController : MonoBehaviour
{
  [SerializeField] private PlayerBullet mermiPrefab;
  [SerializeField] private Transform mermiCikisNoktasi;
  [Range(0.01f, 2f)] [SerializeField] private float mermiAtisSuresi = 25f;
  private float mermiAtisSayac;
  [SerializeField] private GameObject atesEtmeEfekti;

  private Camera mainCamera;
  [SerializeField] private bool firstMermi, secondMermi,laserMermi;

  [SerializeField] private GameObject laserPrefab;

  private void Start()
  {
    mainCamera = Camera.main;
  }

  private void Update()
  {
    if (firstMermi)
    {
      if (Input.GetMouseButton(0) && Time.time > mermiAtisSayac)
      {
        //Instantiate(mermiPrefab,mermiCikisNoktasi.position,mermiCikisNoktasi.rotation);
        mermiPrefab = ObjectPool.instance.FirstMermiCikarFNC();

        if (mermiPrefab)
        {
          mermiPrefab.transform.position = mermiCikisNoktasi.position;
          mermiPrefab.transform.rotation = mermiCikisNoktasi.rotation;
          mermiPrefab.gameObject.SetActive(true);
        }

        if (atesEtmeEfekti)
        {
          atesEtmeEfekti.SetActive(true);
        }

        mermiAtisSayac = Time.time + mermiAtisSuresi;

        mainCamera.DOShakePosition(duration: 0.1f, strength: 0.1f, fadeOut: true);
      }

      if (Input.GetMouseButtonUp(0))
      {
        if (atesEtmeEfekti)

          atesEtmeEfekti.SetActive(false);
      }

    }else if (secondMermi)
    {
      if (Input.GetMouseButtonDown(0))
      {
        mermiPrefab = ObjectPool.instance.SecondMermiCikarFNC();

        if (mermiPrefab)
        {
          mermiPrefab.transform.position = mermiCikisNoktasi.position;
          mermiPrefab.transform.rotation = mermiCikisNoktasi.rotation;
          mermiPrefab.gameObject.SetActive(true);
        }

        mainCamera.DOShakePosition(duration: 0.1f, strength: 0.05f, fadeOut: true);
      }

    }else if (laserMermi)
    {
      if (Input.GetMouseButtonDown(0))
      {
        float randomMesafe = Random.Range(10f, 15f);
        
        
        if (laserPrefab)
        {
          GameObject laser = Instantiate(laserPrefab,mermiCikisNoktasi.position,Quaternion.identity);
          laser.GetComponent<LaserBullet>().LaserMesafeGuncelle(randomMesafe);
        }
       
      }
    }


  }

}
  

  

