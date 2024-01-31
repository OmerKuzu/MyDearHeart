using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DiologSystem : MonoBehaviour
{
    [SerializeField] private List<string> metinler;
    [SerializeField] private TextMeshProUGUI metinTxt;

    private int kacinciMetin;

    [SerializeField] private float textDelay;

    private bool metinBittimi;

    private Coroutine dialogRoutine;    

    private void OnEnable()
    {
        kacinciMetin = 0;
      dialogRoutine=StartCoroutine(DialogOlusturFNC());
    }

    private void OnDisable()
    {
        StopCoroutine(dialogRoutine);
        metinTxt.text = "";
        metinBittimi = false;
        kacinciMetin = 0;
    }

    IEnumerator DialogOlusturFNC()
    {
        while (!metinBittimi)
        {
            StartCoroutine(TextiYazdirFNC(metinler[kacinciMetin], textDelay));
            yield return new WaitUntil(() => metinBittimi);
        }

        yield return new WaitForSeconds(2f);
        kacinciMetin++;

        if (kacinciMetin>=metinler.Count)
        {
            kacinciMetin = 0;
        }

        metinBittimi = false;
        StartCoroutine(DialogOlusturFNC());
    }

    IEnumerator TextiYazdirFNC(string metin,float textDelay)
    {
        metinBittimi = false;
        metinTxt.text = "";

        for (int i = 0; i < metin.Length; i++)
        {
            metinTxt.text += metin[i];
            yield return new WaitForSeconds(textDelay);
        }

        metinBittimi = true;

    }
}
