using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    [SerializeField] private GameObject front;
    [SerializeField] private GameObject side;


    // Start is called before the first frame update
    void Start()
    {
        side.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            front.SetActive(!front.activeSelf);
            side.SetActive(!side.activeSelf);
        }
    }
}
