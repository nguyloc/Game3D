using System;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
     private Animator anim;
     private void Awake()
     {
          anim = GetComponent<Animator>();
     }

     private void Update()
     {
          if (Input.GetKeyDown(KeyCode.Mouse0))
          {
               anim.SetTrigger("Slash");
          }
     }
}
