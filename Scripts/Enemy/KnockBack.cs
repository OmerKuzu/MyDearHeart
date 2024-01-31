using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
 [SerializeField] private float knockBackTime = .2f;
 private Rigidbody2D rb;
 public bool geriTepkiOlsunmu; 

 private void Awake()
 {
  rb=GetComponent<Rigidbody2D>();
 }
 public void GeriTepkiFNC(Transform hasarTransform,float hasarGucu)
 {
  geriTepkiOlsunmu = true;
  Vector2 geriTepkiVectoru = (Vector2)((transform.position - hasarTransform.position).normalized * hasarGucu);
  rb.AddForce(geriTepkiVectoru,ForceMode2D.Impulse);
  StartCoroutine(KnockRoutineFNC());
 }

 IEnumerator KnockRoutineFNC()
 {
  yield return new WaitForSeconds(knockBackTime);
  rb.velocity = Vector2.zero;
  geriTepkiOlsunmu = false;
 }
}

