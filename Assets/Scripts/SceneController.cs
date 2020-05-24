using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private GameObject[] billboardSprites;

    // Start is called before the first frame update
    void Start()
    {
        ActivateBillboardsEffect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void ActivateBillboardsEffect()
    {
        billboardSprites = GameObject.FindGameObjectsWithTag("Billboard");
        foreach (GameObject spriteObject in billboardSprites)
        {
            Debug.Log("bejct name: "+ spriteObject.name);
            spriteObject.AddComponent<Billboards>();
        }
    }
}
