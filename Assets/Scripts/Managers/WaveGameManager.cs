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

    public int countDounStart       = 3;
    public int countDounBetweenWave = 5;
    private int currentWaveLevel    = 1;
    private int countWaveToIncreasHP;
    private int countWaveToIncreasDD;
    private int skeletonInWave;
    private int currentSkeletonInWave;
    private int currentSkeletonHealth;
    private int currentSkeletonDamage;

    private UIController ui;

    [SerializeField] private int startEnemyCount;
    [SerializeField] private int enemyIncreaseStep;

    [SerializeField] private int startEnemyDamage;
    [SerializeField] private int startEnemyHealth;

    public int CurrentWaveLevel => currentWaveLevel;
    public int SkeletonInWave => skeletonInWave;
    public int CurrentSkeletonInWave => currentSkeletonInWave;

    private void Start()
    {
        currentSkeletonDamage = startEnemyDamage;
        currentSkeletonHealth = startEnemyHealth;
    }

    public void StartGame()
    {
        SceneController.instance.IsGameProgress = true;
        StartCoroutine(StartGameWithDelay());
    }

    IEnumerator StartGameWithDelay()
    {
        ui = UIController.instance;
        yield return new WaitForSeconds(2.0f);
        SoundManager.instance.StopMenuMusic();
        //show gui text
        for (int i = 0; i < countDounStart; i++)
        {
            string text = (countDounStart - i).ToString();
            ui.SetWarningText(text);
            SoundManager.instance.Play("Error");
            yield return new WaitForSeconds(1);
        }
        SoundManager.instance.Play("ErrorGo");
        ui.SetWarningText("GO!");
        yield return new WaitForSeconds(1);
        ui.SetWarningText("");
        StartWave(currentWaveLevel);
    }

    private void StartWave(int waveLevel)
    {
        SoundManager.instance.PlayBattleMusic();
        skeletonInWave          = startEnemyCount + enemyIncreaseStep * (currentWaveLevel - 1);
        currentSkeletonInWave = skeletonInWave;
        SetUpWaveDifficulty();
        SceneController.instance.StartSpawnManager(currentSkeletonInWave, currentSkeletonHealth, currentSkeletonDamage);
        UIController.instance.UpdateWaveNumber(currentWaveLevel);
        UIController.instance.UpdateWaveLeft(currentSkeletonInWave);
    }

    private void SetUpWaveDifficulty()
    {
        //if (countWaveToIncreasDD == 3)
        //{
        //    currentSkeletonDamage++;
        //    countWaveToIncreasDD = 0;
        //}
        //countWaveToIncreasDD++;

        if (countWaveToIncreasHP == 2)
        {
            currentSkeletonHealth++;
            countWaveToIncreasHP = 0;
        }
        // set up wave difficulty
        countWaveToIncreasHP++;
    }

    public void CompleteWave()
    {
        SoundManager.instance.StopBattleMusic();
        SoundManager.instance.Play("CompleteWave");
        currentWaveLevel++;
        StartCoroutine(ShowWinWave());
    }

    IEnumerator ShowWinWave()
    {
        ui = UIController.instance;
        ui.SetWarningText("COMPLETE WAVE!");
        yield return new WaitForSeconds(2);
        //show gui text
        for (int i = 0; i < countDounBetweenWave; i++)
        {
            string text = $"WAVE {currentWaveLevel} IN: {(countDounBetweenWave - i)}";
            SoundManager.instance.Play("Error");
            ui.SetWarningText(text);
            yield return new WaitForSeconds(1);
        }
        SoundManager.instance.Play("ErrorGo");
        ui.SetWarningText("GO!");
        yield return new WaitForSeconds(1);
        ui.SetWarningText("");
        StartWave(currentWaveLevel);
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

    public void DeincrementSkeletonInWave()
    {
        currentSkeletonInWave--;
        UIController.instance.UpdateWaveLeft(currentSkeletonInWave);
        if (currentSkeletonInWave == 0)
            CompleteWave();
    }
}
