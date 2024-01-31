using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    public  static ObjectPool instance;

    [Header("First Mermiler Pools")] 
    [SerializeField]  private Transform firstMermiHolder;
     [SerializeField] private PlayerBullet firstPlayerBullet;
    public List<PlayerBullet> firstPlayerBulletList = new List<PlayerBullet>();
     [SerializeField] private int firstPlayerBulletAdet = 40;

     [Header("Second Mermiler Pools")] 
     [SerializeField]   private Transform secondMermiHolder;
     [SerializeField] private PlayerBullet secondPlayerBullet;
     public List<PlayerBullet> secondPlayerBulletList = new List<PlayerBullet>();
     [SerializeField] private int secondPlayerBulletAdet = 20;
     
     
    [Header("Enemy Mermiler Pools")] [SerializeField]
    private Transform enemyMermiHolder;

    [SerializeField] private EnemyBullet enemyBullet;
    public List<EnemyBullet> enemyBulletList = new List<EnemyBullet>();
    [SerializeField] private int enemyBulletAdet = 30;
    
    [Header("Boss Mermi Pools")] [SerializeField]
    private Transform bossMermiHolder;
    [SerializeField] private BossBullet bossBullet;
    public List<BossBullet> bossBulletList = new List<BossBullet>();
    [SerializeField] private int bossBulletAdet = 50;



    [Header("DuvarMermiPools")] [SerializeField]
    private Transform duvarMermiEfectHolder;

    [SerializeField] private GameObject duvarEfekt;
    public List<GameObject> duvarMermiEfectList = new List<GameObject>();
    [SerializeField] private int duvarEffectAdet = 15;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


private void Start()
    {
        for (int i = 0; i < firstPlayerBulletAdet; i++)
        {
           PlayerBullet bullet = Instantiate(firstPlayerBullet);
            bullet.gameObject.SetActive(false);
            bullet.gameObject.transform.SetParent(firstMermiHolder);
            firstPlayerBulletList.Add(bullet);
        }
        
        for (int i = 0; i < secondPlayerBulletAdet; i++)
        {
            PlayerBullet bullet = Instantiate(secondPlayerBullet);
            bullet.gameObject.SetActive(false);
            bullet.gameObject.transform.SetParent(secondMermiHolder);
            secondPlayerBulletList.Add(bullet);
        }
        
        
        for (int i = 0; i < enemyBulletAdet; i++)
        {
           EnemyBullet enemy_bullet = Instantiate(enemyBullet);
           enemy_bullet.gameObject.SetActive(false);
           enemy_bullet.gameObject.transform.SetParent(enemyMermiHolder);
           enemyBulletList.Add(enemy_bullet);
        }
        for (int i = 0; i < bossBulletAdet; i++)
        {
            BossBullet boss_bullet = Instantiate(bossBullet);
            boss_bullet.gameObject.SetActive(false);
            boss_bullet.gameObject.transform.SetParent(bossMermiHolder);
            bossBulletList.Add(boss_bullet);
        }
        
        
        for (int i = 0; i <duvarEffectAdet; i++)
        {
           GameObject duvar_Efecti  = Instantiate(duvarEfekt);
           duvar_Efecti.gameObject.SetActive(false);
            duvar_Efecti.gameObject.transform.SetParent(duvarMermiEfectHolder);
            duvarMermiEfectList.Add(duvar_Efecti);
        }
    }

    public PlayerBullet FirstMermiCikarFNC()
    {
        for (int i = 0; i < firstPlayerBulletList.Count; i++)
        {
            if (!firstPlayerBulletList[i].gameObject.activeInHierarchy)
            {
                return firstPlayerBulletList[i];
            }
        }

        return null;
    }
    
    public PlayerBullet SecondMermiCikarFNC()
    {
        for (int i = 0; i < secondPlayerBulletList.Count; i++)
        {
            if (!secondPlayerBulletList[i].gameObject.activeInHierarchy)
            {
                return secondPlayerBulletList[i];
            }
        }

        return null;
    }
    public EnemyBullet EnemyMermiCikarFNC()
    {
        for (int i = 0; i <enemyBulletList.Count; i++)
        {
            if (!enemyBulletList[i].gameObject.activeInHierarchy)
            {
                return enemyBulletList[i];
            }
        }

        return null;
    }
    
    public BossBullet BossMermiCikarFNC()
    {
        for (int i = 0; i <bossBulletList.Count; i++)
        {
            if (!bossBulletList[i].gameObject.activeInHierarchy)
            {
                return bossBulletList[i];
            }
        }

        return null;
    }

    public GameObject DuvarMermiEfectCikar()
    {
        for (int i = 0; i <duvarMermiEfectList.Count; i++)
        {
            if (!duvarMermiEfectList[i].gameObject.activeInHierarchy)
            {
                return duvarMermiEfectList[i];
            }
        }

        return null;
    }
}
