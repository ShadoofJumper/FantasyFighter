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
        if (animator != null)
        {
            animator.SetTrigger("Invoke");
        }
    }
}
