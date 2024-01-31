using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   [SerializeField] private Transform GunItemPanel;


   private void Start()
   {
      TumSecimleriKapat();
      GunItemPanel.GetChild(0).GetChild(0).gameObject.SetActive(true);
   }

   private void Update()
   {
      GunOpenFNC();
   }

   void GunOpenFNC()
   {
      if (Input.GetKeyDown(KeyCode.Alpha1))
      {
         TumSecimleriKapat();
         GunItemPanel.GetChild(0).GetChild(0).gameObject.SetActive(true);
         if (GunSelected.instance)
         {
            GunSelected.instance.GunSelectedFNC(1);
         }
      }else if (Input.GetKeyDown(KeyCode.Alpha2))
      {
         TumSecimleriKapat();
         GunItemPanel.GetChild(1).GetChild(0).gameObject.SetActive(true);
         if (GunSelected.instance)
         {
            GunSelected.instance.GunSelectedFNC(2);
         }
      }
      else if (Input.GetKeyDown(KeyCode.Alpha3))
      {
         TumSecimleriKapat();
         GunItemPanel.GetChild(2).GetChild(0).gameObject.SetActive(true);
         if (GunSelected.instance)
         {
            GunSelected.instance.GunSelectedFNC(3);
         }
      }
   }


   void TumSecimleriKapat()
   {
      foreach (Transform child in GunItemPanel)
      {
         child.GetChild(0).gameObject.SetActive(false);
            
      }
   }
}
