using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] private Sprite deathSprite;
    [SerializeField] private GameObject characterSpriteObject;

    protected Rigidbody   characterRigidbody;
    protected Animator    characterAnimator;
    protected CharacterCombat characterCombat;
    protected bool isDead;
    protected bool isPause;

    public Animator CharacterAnimator       => characterAnimator;
    public Rigidbody CharacterRigidbody     => characterRigidbody;
    public GameObject CharacterSpriteObject => characterSpriteObject;
    public Sprite DeathSprite               => deathSprite;
    public bool IsDead { get { return isDead; } set { isDead = value; } }
    public bool IsPause { get { return isPause; } set { isPause = value; } }


    protected virtual void SetUpCharacterComp()
    {
        characterRigidbody  = GetComponent<Rigidbody>();
        characterAnimator   = GetComponentInChildren<Animator>();
        characterCombat     = GetComponent<CharacterCombat>();
    }



}
