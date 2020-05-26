using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int healthAmound;
    [SerializeField] private Animator potionAnimator;
    private CharacterCombat characterCombat;

    // Start is called before the first frame update
    void Start()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        characterCombat = other.GetComponent<CharacterCombat>();
        if (characterCombat != null)
        {
            Use();
        }
    }

    private void Use()
    {
        characterCombat.HealthCharacter(healthAmound);
        Destroy(gameObject);
    }

}
