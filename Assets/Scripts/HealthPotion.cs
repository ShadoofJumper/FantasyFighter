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
        if (characterCombat != null && characterCombat.Health < characterCombat.MaxHealth)
        {
            Use();
        }
    }

    private void Use()
    {
        if (spawnPlace)
            spawnPlace.GetComponent<SpawnPotion>().SpawnWithDelay();
        spawnPlace = null;
        SoundManager.instance.Play("PickupHealth");
        characterCombat.HealthCharacter(healthAmound);
        gameObject.SetActive(false);
    }

}
