using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelected : MonoBehaviour
{
   public static GunSelected instance;
   [SerializeField] private Transform handTransform;

   private void Awake()
   {
      instance = this;
   }

   public void GunSelectedFNC(int hangiSilah)
   {
      ButunSilahlariKapat();
      switch (hangiSilah)
      {
         case 1:
            handTransform.GetChild(0).gameObject.SetActive(true);
            break;

         case 2:
            handTransform.GetChild(1).gameObject.SetActive(true);
            break;
         case 3:
            handTransform.GetChild(2).gameObject.SetActive(true);
            break;
      }
   }

   void ButunSilahlariKapat()
   {
      foreach (Transform child in handTransform)
      {
         child.gameObject.SetActive(false);


      }
   }
}
