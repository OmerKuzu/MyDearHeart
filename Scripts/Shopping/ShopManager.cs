using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Canvas canvasMesaj;
    private bool satinAlmaAlanindami;

    enum ShopTye {healthShop,zirhShop,healthUpgradeShop}

    [SerializeField] private ShopTye shopType;

    [SerializeField] private int itemFiyat;

    private void Update()
    {
        if (satinAlmaAlanindami && Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.instance.coinAdet >= itemFiyat)
            {
                switch (shopType)
                {
                 case  ShopTye.healthShop:
                     if (PlayerHealthController.instance.GecerliCaniAl()<PlayerHealthController.instance.ToplamCaniAl())
                     {
                         PlayerHealthController.instance.CaniArtirFNC(10);
                         GameManager.instance.CoinAzalt(itemFiyat);
                     }
                     break;
                 
                 
                 case ShopTye.zirhShop:
                     if (!PlayerHealthController.instance.zirhVarmi)
                     {
                         PlayerHealthController.instance.ZirhiArtirFNC(5);
                         GameManager.instance.CoinAzalt(itemFiyat);
                     }
                     break;
                    case ShopTye.healthUpgradeShop:
                        PlayerHealthController.instance.ToplamCaniArtirFNC();
                        GameManager.instance.CoinAzalt(itemFiyat);
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasMesaj.gameObject.SetActive(true);
            satinAlmaAlanindami = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasMesaj.gameObject.SetActive(false);
            satinAlmaAlanindami = false;
        }
    }
}
