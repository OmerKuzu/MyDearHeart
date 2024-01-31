using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FadeManager : MonoBehaviour
{
  private Image fadeImage;

  private void Awake()
  {
      fadeImage = GetComponent<Image>();
  }

  private void Start()
  {
      StartCoroutine(AlphaKapatRouitine());
  }

  IEnumerator AlphaKapatRouitine()
  {
      fadeImage.GetComponent<CanvasGroup>().DOFade(0, 1f);
      yield return new WaitForSeconds(2f);
  }

  public IEnumerator AlphaAcRouitine()
  {
      fadeImage.GetComponent<CanvasGroup>().DOFade(1, 1f);
      yield return new WaitForSeconds(2f);
  }
}
