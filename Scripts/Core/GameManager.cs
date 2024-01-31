 using System;
 using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 public static GameManager instance;
 public int toplamPuan;
 public int coinAdet;

 public bool gameOver;
 private void Awake()
 {
  
   instance = this;
 
 }

 private void Start()
 {
  toplamPuan = 0;
  coinAdet = 0;
 }

 public void PuanArtir(int gelenPuan)
 {
  toplamPuan += gelenPuan;
  UIManager.instance.PuaniYazdir(toplamPuan);
 }

 public void CoinArtir(int gelenAdet)
 {
  coinAdet+=gelenAdet;
  UIManager.instance.CoinAdetYazdir(coinAdet);
 }
 public void CoinAzalt(int azaltmaAdet)
 {
  coinAdet-=azaltmaAdet;
  if (coinAdet<=0)
  {
   coinAdet = 0;
  }
  UIManager.instance.CoinAdetYazdir(coinAdet);
 }

 //silinecek kısım
 private void Update()
 {
  if (Input.GetKeyDown(KeyCode.B))
  {
   CoinArtir(20);
  }

  if (Input.GetKeyDown(KeyCode.M))
  {
   PlayerHealthController.instance.hasarAlFNC(5);
  }
  
   
  
 }
}
