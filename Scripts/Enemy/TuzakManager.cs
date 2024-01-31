using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TuzakManager : MonoBehaviour
{
    [SerializeField] private int hasarMiktari;
    private bool hasarVerebilirmi;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasarVerebilirmi = true;
            GetComponent<Animator>().SetTrigger("tuzakCalissin");
        }
    }

   private void OnTriggerExit2D(Collider2D other)
    {
        hasarVerebilirmi = false;
    }

    public void HasarVer()
    {
        if (hasarVerebilirmi)
        {
            PlayerHealthController.instance.hasarAlFNC(hasarMiktari);
            StartCoroutine( PlayerHealthController.instance.YanipSonmeRouitine());
        }
    }
}
