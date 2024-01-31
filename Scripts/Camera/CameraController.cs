using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public static CameraController instance;

  public Transform hedefPos;
  public float smootTime = .3f;

  private Vector3 velocity = Vector3.zero;


  private void Awake()
  {
    instance = this;
  }

  private void LateUpdate()
  {
    if (hedefPos)
    {
      Vector3 hedefPosition = new Vector3(hedefPos.position.x, hedefPos.position.y, transform.position.z);
      Vector3 newPosition = Vector3.SmoothDamp(transform.position, hedefPosition, ref velocity, smootTime);
      transform.position = newPosition;
    }
  }

  public void HedefTransform(Transform newTransform)
  {
    hedefPos = newTransform;
  }
    
}
