using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Slider slider;
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private Text scoreText;

    private string sceneOnPlayName = "Game";
    static private float volumePower;

    public float VolumePower => volumePower;

    private void Start()
    {
        UpdateScore();
    }

    // ------------- main menu
    public void StartGame()
    {
        sceneFader.FadeTo(sceneOnPlayName);
    }

    public void TurnSettings()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    // ------------- settings
    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void SliderChange()
    {
        volumePower = slider.value;
        Debug.Log("Volume power: "+ volumePower);
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: "+PlayerPrefs.GetInt("Score", 0).ToString();
    }
}
