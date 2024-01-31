using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FlyTxtController : MonoBehaviour
{
  private void Start()
  {
    this.transform.DOMoveY(this.transform.position.y + 2f, .6f);
    this.GetComponent<CanvasGroup>().DOFade(0, .6f).OnComplete(DestroySelf);
  }

  void DestroySelf()
  {
   Destroy(gameObject);
  }
}
