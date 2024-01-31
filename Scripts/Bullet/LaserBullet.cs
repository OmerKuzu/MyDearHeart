using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class LaserBullet : MonoBehaviour
{
 private float laserRange ;
 private SpriteRenderer spriteRenderer;
 private CapsuleCollider2D capsuleCollider2D;
 [SerializeField] private float laserBuyumeZamani=2f;
 [SerializeField] private float fadeZamani = .4f;

 private bool buyumeOlsunmu = true;

 [SerializeField] private int laserHasarGucu=1000;

 private void Awake()
 {
  spriteRenderer = GetComponent<SpriteRenderer>();
  capsuleCollider2D = GetComponent<CapsuleCollider2D>();
 }

 private void Start()
 {
  LaserFirlatFNC();
 }


 void LaserFirlatFNC()
 {
  Vector3 mousePosition = Input.mousePosition;
  mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

  Vector2 direction = (Vector2)(mousePosition - transform.position);
  transform.right=(Vector3) direction;
 }

 public void LaserMesafeGuncelle(float _laserRange)
 {
  this.laserRange = _laserRange;
  StartCoroutine(LaserArtirRoutine());
 }

 IEnumerator LaserArtirRoutine()
 {
  float gecenZaman = 0f;
  while (spriteRenderer.size.x< laserRange && buyumeOlsunmu)
  {
   gecenZaman += Time.deltaTime;

   float laserT = gecenZaman / laserBuyumeZamani;
   spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, laserT), spriteRenderer.size.y);

   capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, laserRange, laserT), capsuleCollider2D.size.y);
   capsuleCollider2D.offset= new Vector2(Mathf.Lerp(1f, laserRange, laserT) / 2, capsuleCollider2D.offset.y);

   yield return null;
  }
  StartCoroutine(LaserFadeRoutine());
  
  
 }

 IEnumerator LaserFadeRoutine()
 {
  float gecenZaman = 0f;

  float baslangicAlpha = spriteRenderer.color.a;
  while (gecenZaman<fadeZamani)
  {
   gecenZaman += Time.deltaTime;

   float _alpha = Mathf.Lerp(baslangicAlpha, 0, gecenZaman / fadeZamani);
   spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, _alpha);
   yield return null;
  }
  Destroy(gameObject);
 }

 private void OnTriggerEnter2D(Collider2D other)
 {
  if (other.CompareTag("Enemy"))
  {
   other.GetComponent<EnemyHealthController>().hasarAlFNC(laserHasarGucu);
  }else if (other.CompareTag("Wall"))
  {
   buyumeOlsunmu = false;
  }
 } 
}
