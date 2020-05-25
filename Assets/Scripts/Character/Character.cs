using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;

    protected Rigidbody   characterRigidbody;
    protected Animator    characterAnimator;
    protected CharacterCombat characterCombat;

    protected virtual void SetUpCharacterComp()
    {
        characterRigidbody  = GetComponent<Rigidbody>();
        characterAnimator   = GetComponentInChildren<Animator>();
        characterCombat     = GetComponent<CharacterCombat>();
    }



}
