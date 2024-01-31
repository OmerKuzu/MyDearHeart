

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHareketController : MonoBehaviour
{
    [Header("Hareket Ayarlari")]
    [SerializeField] private float normalHareketHizi = 10f;
    [SerializeField] private float kosmaHareketHizi = 20f;
    
    private float hareketHizi;
    
    public static PlayerHareketController instance;
    private Vector2 hareketVectoru;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private Transform handTransform;
     
    [HideInInspector]
    public bool geriTepkiOlsunmu = false;
   
    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
    }

    private void Start()
    {
        hareketHizi = normalHareketHizi;
    }

    private void Update()
    {
        if (UIManager.instance.oyunDurdumu)
        return;
            
        
        if (!geriTepkiOlsunmu)
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                hareketHizi = kosmaHareketHizi;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                hareketHizi = normalHareketHizi;
            }
        
            HareketFNC();
            SilahiDondurFNC();
        }

   



    void HareketFNC()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        hareketVectoru = new Vector2(h,v);
        hareketVectoru.Normalize();
       rb.velocity = hareketVectoru * hareketHizi; 
       //print(rb.velocity);
       if (rb.velocity != Vector2.zero) 
       {
           anim.SetBool("hareketEtsinmi",true);
       }
       else
       {
           anim.SetBool("hareketEtsinmi",false);
       }    
    }

    void SilahiDondurFNC()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPoint = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 hareketYonu = new Vector2(mousePos.x - playerPoint.x, mousePos.y - playerPoint.y);

        float angle = Mathf.Atan2(hareketYonu.y, hareketYonu.x) * Mathf.Rad2Deg;
        handTransform.rotation = Quaternion.Euler(0, 0, angle);

        if (mousePos.x <playerPoint.x)
        {   
            transform.localScale = new Vector3(-1, 1, 1);
            handTransform.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            handTransform.localScale = new Vector3(1, 1, 1);
        }
    }
   
    
    public IEnumerator GeriTepkiFNC(float forceX, float forceY, float duration, Transform otherObject)
    {
        geriTepkiOlsunmu = true;
        
        int geriTepkiDirection;
        if (transform.position.x < otherObject.position.x)
        {
            geriTepkiDirection = -1;
        }
        else
        {
            geriTepkiDirection = 1;
        }

        rb.velocity = Vector2.zero;
        Vector2 force = new Vector2(geriTepkiDirection * forceX, forceY);
        rb.AddForce(force,ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        geriTepkiOlsunmu = false;
        rb.velocity = Vector2.zero;

    }
    
}




