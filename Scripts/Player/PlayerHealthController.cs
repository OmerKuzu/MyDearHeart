using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    [SerializeField] private int toplamCan = 10;
    private int gecerliCan;

    [SerializeField] private int toplamZirh = 5;
    private int gecerliZirh;
    [HideInInspector]
   public  bool zirhVarmi;

    [SerializeField] private SpriteRenderer bodySprite;

    [SerializeField] private GameObject playerDamageEffect,playerDeathEffect;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        zirhVarmi = true;
        gecerliCan = toplamCan;
        gecerliZirh = toplamZirh;
        
        UIManager.instance.StartHealthFNC(toplamCan,gecerliCan,toplamZirh,gecerliZirh);
    }

    public void ZirhiArtirFNC(int artisMiktari)
    {
        gecerliZirh += artisMiktari;
        gecerliZirh = Mathf.Clamp(gecerliZirh, 0, toplamZirh);
        zirhVarmi = true;
        UIManager.instance.UpdatetHealthFNC(toplamCan,gecerliCan,toplamZirh,gecerliZirh);
    }

    public void CaniArtirFNC(int artisMiktari)
    {
        gecerliCan += artisMiktari;
        gecerliCan = Mathf.Clamp(gecerliCan, 0, toplamCan);
        UIManager.instance.UpdatetHealthFNC(toplamCan,gecerliCan,toplamZirh,gecerliZirh);
    }

    public void ToplamCaniArtirFNC()
    {
        toplamCan += 5;
        gecerliCan = toplamCan;

        gecerliCan = Mathf.Clamp(gecerliCan, 0, toplamCan);
        gecerliZirh += 5;
        gecerliZirh = Mathf.Clamp(gecerliZirh, 0, toplamZirh);
        zirhVarmi = true;
        UIManager.instance.UpdatetHealthFNC(toplamCan,gecerliCan,toplamZirh,gecerliZirh);
    }

    public void hasarAlFNC(int hasarMiktari)
    {
        if (gecerliCan<=0)
            return;
        Instantiate(playerDamageEffect, transform.position, Quaternion.identity);
       
        
        if (zirhVarmi)
        {
            gecerliZirh -= hasarMiktari;
            gecerliZirh = Mathf.Clamp(gecerliZirh, 0, toplamZirh);
            UIManager.instance.UpdatetHealthFNC(toplamCan,gecerliCan,toplamZirh,gecerliZirh);
            if (gecerliZirh <=0)
            {
                zirhVarmi = false;
            }
            return;
        }

        gecerliCan -= hasarMiktari;
        gecerliCan = Mathf.Clamp(  gecerliCan, 0, toplamCan);
        UIManager.instance.UpdatetHealthFNC(toplamCan,gecerliCan,toplamZirh,gecerliZirh);
        if (gecerliCan<=0)
        {
            PlayerCanVerdi();
        }
    }

    void PlayerCanVerdi()
    {
        UIManager.instance.KaybettiPanelAc();
        
        Instantiate(playerDeathEffect, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false); 
    }

    public IEnumerator YanipSonmeRouitine()
    {
        for (int i = 0; i < 5; i++)
        {
            bodySprite.color = new Color(
                bodySprite.color.r,
                bodySprite.color.g,
                bodySprite.color.b,
                    .1f

            );
            yield return new WaitForSeconds(.1f);
            bodySprite.color = new Color(
                bodySprite.color.r,
                bodySprite.color.g,
                bodySprite.color.b,
                1f
            );
            yield return new WaitForSeconds(.1f);
        }
    }

    public int ToplamCaniAl()
    {
        return toplamCan;
    }

    public int GecerliCaniAl()
    {
        return gecerliCan;
    }
}
