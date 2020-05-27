using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int healthAmound;
    [SerializeField] private Animator potionAnimator;
    private CharacterCombat characterCombat;
    private Transform spawnPlace;
    public Transform SpawnPlace { get { return spawnPlace; } set { spawnPlace = value; } }

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
        if (spawnPlace)
            spawnPlace.GetComponent<SpawnPotion>().SpawnWithDelay();
        spawnPlace = null;

        characterCombat.HealthCharacter(healthAmound);
        gameObject.SetActive(false);
    }

}
