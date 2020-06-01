using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject TutorialPanel;
    [SerializeField] private GameObject TutorialBlockFirst;
    [SerializeField] private GameObject TutorialBlockSecond;

    private int isTutorialWasShowed;
    // Start is called before the first frame update
    void Start()
    {
        isTutorialWasShowed = PlayerPrefs.GetInt("TutorialShowed", 0);
        if (isTutorialWasShowed == 0)
        {
            ShowTutorial();
        }
        else
        {
            WaveGameManager.instance.StartGame();
        }
    }

    private void ShowTutorial()
    {
        TutorialPanel.SetActive(true);
        TutorialBlockFirst.SetActive(true);
    }

    public void NextTutiralSlide()
    {
        TutorialBlockFirst.SetActive(false);
        TutorialBlockSecond.SetActive(true);
    }

    public void CompleteTutorial()
    {
        PlayerPrefs.SetInt("TutorialShowed", 1);
        TutorialBlockSecond.SetActive(false);
        TutorialPanel.SetActive(false);
        WaveGameManager.instance.StartGame();
    }

}
