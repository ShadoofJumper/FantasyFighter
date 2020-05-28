using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveGameManager : MonoBehaviour
{
    #region Singlton

    public static WaveGameManager instance;

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

    public int countDounStart = 5;
    private int currentWaveLevel = 1;
    private UIController ui;

    public void StartGame()
    {
        SceneController.instance.IsGameProgress = true;
        StartCoroutine(StartGameWithDelay());
    }

    IEnumerator StartGameWithDelay()
    {
        ui = UIController.instance;
        //show gui text
        for (int i = 0; i < countDounStart; i++)
        {
            string text = (countDounStart - i).ToString();
            ui.SetWarningText(text);
            yield return new WaitForSeconds(1);
        }
        ui.SetWarningText("GO!");
        yield return new WaitForSeconds(2);
        ui.SetWarningText("");
        StartWave(currentWaveLevel);
    }

    private void StartWave(int waveLevel)
    {
        Debug.Log("StartWave!");
        SceneController.instance.StartSpawnManager();
    }

    public void CompleteWave()
    {
        currentWaveLevel++;
    }

    public void FailWave()
    {
        SceneController.instance.IsGameProgress = false;
        //TO DO show ui for restart or go mainmenu
        UIController.instance.ShowFailPanel();
        SaveScore();
    }

    public void SaveScore()
    {
        int currentMaxScore = PlayerPrefs.GetInt("Score", 0);
        Debug.Log($"SaveScore: was{currentMaxScore}, have{ScoreManager.instance.EnemiesKilled}");
        if (ScoreManager.instance.EnemiesKilled > currentMaxScore)
        {
            PlayerPrefs.SetInt("Score", ScoreManager.instance.EnemiesKilled);
        }
    }
}
