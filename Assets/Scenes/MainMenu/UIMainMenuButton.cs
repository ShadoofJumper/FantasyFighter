using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform button;
    public float sizeOnHover = 1.1f;
    public float sizeStandart = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.Play("ButtonOver");
        // change size to 
        button.localScale = sizeOnHover * sizeStandart * Vector3.one;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.localScale = sizeStandart * Vector3.one;
    }

    private void OnDisable()
    {
        button.localScale = sizeStandart * Vector3.one;
    }

}
