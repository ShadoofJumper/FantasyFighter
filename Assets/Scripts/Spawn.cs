using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void ShowSpawnVFX()
    {
        animator.SetTrigger("Invoke");
    }
}
