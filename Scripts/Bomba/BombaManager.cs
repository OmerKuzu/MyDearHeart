
using System.Collections;

using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class BombaManager : MonoBehaviour
{
  [SerializeField] private GameObject bombaEfectPrefab;
  [SerializeField] private float patlamaSuresi = 1f;
  [SerializeField] private float radius = 3f;
  [SerializeField] private int hasarMiktari = 2;

  private Animator anim;

  private void Awake()
  {
    anim = GetComponent<Animator>();
  }
 

  public IEnumerator BombaPatlatFNC(Transform center)
  {
    yield return new WaitForSeconds(patlamaSuresi);
    anim.SetTrigger("ateslendi");
    yield return new WaitForSeconds(.4f);
    Camera.main.DOShakePosition(.8f,1f,fadeOut:true);
    RastgeleBombaOlusturFNC(center);

  }

  void RastgeleBombaOlusturFNC(Transform center)
  {
    for (int i = 0; i < 5; i++)
    {
      Vector2 randomPos = Random.insideUnitCircle * radius;
      Vector3 bombaPos = new Vector3(center.position.x + randomPos.x, center.position.y + randomPos.y, 0);

      GameObject bombEfect = Instantiate(bombaEfectPrefab, bombaPos, Quaternion.identity);

      float scale = Random.Range(.5f, 1f);
      bombEfect.transform.localScale = new Vector3(scale, scale, scale);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
      if (rb!=null)
      {
        rb.GetComponent<PlayerHealthController>().hasarAlFNC(hasarMiktari);
        StartCoroutine(rb.GetComponent<PlayerHareketController>().GeriTepkiFNC(forceX: 20, 15, .2f, this.transform));
      }
    }
  }
}
