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

    [SerializeField] private Sprite emptyHearth;
    [SerializeField] private Sprite fullHearth;
    [SerializeField] private Sprite halfHearth;

    [SerializeField] private Text warningText;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private SceneFader sceneFader;

    [SerializeField] private Text waveNumberText;
    [SerializeField] private Text waveEnemiesLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    // -------- UI info  game --------

    public void UpdateHPBar(int currentHp)
    {
        for (int hearthId = 1; hearthId <= hearts.Length; hearthId++)
        {
            if ((hearthId * 2) <= currentHp)
            {
                hearts[hearthId - 1].sprite = fullHearth;
            }
            else if ((hearthId * 2) - 1 == currentHp)
            {
                hearts[hearthId - 1].sprite = halfHearth;
            }
            else
            {
                hearts[hearthId - 1].sprite = emptyHearth;
            }
        }
    }

    public void UpdateScore(int scoreNumber)
    {
        enemyKillText.text = scoreNumber.ToString();
    }

    public void UpdateWaveNumber(int number)
    {
        waveNumberText.text = "WAVE: "+ number;
    }

    public void UpdateWaveLeft(int number)
    {
        waveEnemiesLeft.text = number + " left";
    }

    public void SetWarningText(string warningString)
    {
        warningText.text = warningString;
    }

    // -------- fail game --------
    public void ShowFailPanel()
    {
        failPanel.SetActive(true);
    }
    public void HideFailPanel()
    {
        failPanel.SetActive(false);
    }

    public void RestartGame()
    {
        HideFailPanel();
        sceneFader.FadeTo("Game");
    }

    public void ReturnMenu()
    {
        HideFailPanel();
        sceneFader.FadeTo("MainMenu");
    }

}
