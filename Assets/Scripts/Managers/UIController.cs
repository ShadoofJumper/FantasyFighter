using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Singlton

    public static UIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Try create another instance of game manager!");
        }

    }
    #endregion

    [SerializeField] private Text enemyKillText;
    [SerializeField] private Image[] hearts;

    [SerializeField] private Image emptyHearth;
    [SerializeField] private Image fullHearth;
    [SerializeField] private Image halfHearth;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreNumber)
    {
        enemyKillText.text = scoreNumber.ToString();
    }


}
