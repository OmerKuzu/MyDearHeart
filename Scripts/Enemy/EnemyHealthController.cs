
using System;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private Image fillImg;
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private float mermiTepkiGucu = 3f;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private int maxCan = 100;
    private int gecerliCan;
    private KnockBack knockBack;
    [SerializeField] private bool bossMu;
    [SerializeField] private bool BombEnemyMi;
    


    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
    }


    private void Start()
    {
        
        gecerliCan = maxCan;
        fillImg.fillAmount = maxCan;
        

    }

    public void hasarAlFNC(int hasarMiktari)
    {

        gecerliCan -= hasarMiktari;
        fillImg.DOFillAmount((float)gecerliCan / maxCan, .5f);

        Instantiate(damageEffect, transform.position, Quaternion.identity);

        if (knockBack)
        {
            knockBack.GeriTepkiFNC(PlayerHareketController.instance.transform, mermiTepkiGucu);
        }



        if (gecerliCan <= 0)
        {
            Instantiate(deathEffect, transform.position, quaternion.identity);

            if (GetComponent<DropManager>())
            {
                GetComponent<DropManager>().NesneyiBirakFNC();
            }

            if (BombEnemyMi)
            {
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }

            gameObject.SetActive(false);

            if (bossMu)
            {
                UIManager.instance.KazandiPanelAc();
            }
        }
    }



}
