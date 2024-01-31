using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Image healthFillImg;
    [SerializeField] private Image zirhFillImg;

    [SerializeField] private TextMeshProUGUI healthTxt, zirhTxt;
    [SerializeField] private TextMeshProUGUI toplamPuanTxt, coinAdetTxt;

    [SerializeField] private GameObject pausePanel;

    [SerializeField] private string levelName;

    [SerializeField] private GameObject kazandiPanel, kaybettiPanel;

    [HideInInspector]
    public bool oyunDurdumu;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        toplamPuanTxt.text = "0 P";
        coinAdetTxt.text = "0 ";
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            PausePanelAcKapat();
        }

        void PausePanelAcKapat()
        {
            if (GameManager.instance.gameOver)
                return;

            oyunDurdumu = !oyunDurdumu;

            if (pausePanel)
            {
                pausePanel.SetActive(oyunDurdumu);
                Time.timeScale = (oyunDurdumu) ? 0 : 1;
            }

        }

        public void KazandiPanelAc()
        {
            GameManager.instance.gameOver = true;
            kazandiPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        public void KaybettiPanelAc()
        {
            GameManager.instance.gameOver = true;
            kaybettiPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        public void OyunaDonFNC()
        {
            if (pausePanel)
            {
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
              
            }
        }

        public void AnaMenuFNC()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(levelName);
        }

        public void OyundanCikFNC()
        {
            Application.Quit();
        }
    

    public void StartHealthFNC(int toplamCan, int gecerliCan, int toplamZirh, int gecerliZirh)
        {
            healthFillImg.fillAmount = (float)gecerliCan / toplamCan;
            zirhFillImg.fillAmount = (float)gecerliZirh / toplamZirh;

            healthTxt.text = gecerliCan + "/" + toplamCan;
            zirhTxt.text = gecerliZirh + "/" + toplamZirh;
        }

        public void UpdatetHealthFNC(int toplamCan, int gecerliCan, int toplamZirh, int gecerliZirh)
        {
            healthFillImg.DOFillAmount(endValue: (float)gecerliCan / toplamCan, duration: .3f);
            zirhFillImg.DOFillAmount(endValue: (float)gecerliZirh / toplamZirh, duration: 3f);



            healthTxt.text = gecerliCan + "/" + toplamCan;
            zirhTxt.text = gecerliZirh + "/" + toplamZirh;
        }

        public void PuaniYazdir(int gelenPuan)
        {
            toplamPuanTxt.text = gelenPuan.ToString() + " P ";
        }

        public void CoinAdetYazdir(int gelenAdet)
        {
            coinAdetTxt.text = gelenAdet.ToString();
        }
    }
