using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Slider sliderVolume;
    [SerializeField] private Slider sliderFXVolume;
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private Text scoreText;

    private string sceneOnPlayName = "Game";

    static private float volumePower;
    static private float volumeFXPower;

    public float VolumePower    => volumePower;
    public float VolumeFXPower  => volumeFXPower;

    private void Start()
    {
        UpdateScore();
        LoadSetttings();
        SoundManager.instance.StopBattleMusic();
        SoundManager.instance.PlayMenuMusic();
    }

    private void LoadSetttings()
    {
        volumePower     = PlayerPrefs.GetFloat("VolumeMusic", 1);
        volumeFXPower   = PlayerPrefs.GetFloat("VolumeFX", 1);
        sliderVolume.value      = volumePower;
        sliderFXVolume.value    = volumeFXPower;
    }

    // ------------- main menu
    public void StartGame()
    {
        SoundManager.instance.Play("ButtonPress");
        sceneFader.FadeTo(sceneOnPlayName);
    }

    public void ResetGame()
    {
        SoundManager.instance.Play("ButtonPress");
        PlayerPrefs.DeleteAll();
        volumePower     = 1;
        volumeFXPower   = 1;
        sliderVolume.value      = 1;
        sliderFXVolume.value    = 1;
        SoundManager.instance.SetAllFXVolume(volumeFXPower);
        SoundManager.instance.SetAllMusicVolume(volumeFXPower);
        UpdateScore();
    }

    public void TurnSettings()
    {
        SoundManager.instance.Play("ButtonPress");
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
        SoundManager.instance.Play("ButtonPress");
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);

        PlayerPrefs.SetFloat("VolumeMusic", volumePower);
        PlayerPrefs.SetFloat("VolumeFX", volumeFXPower);
    }

    public void SliderVolumeChange()
    {
        volumePower = sliderVolume.value;
        SoundManager.instance.SetAllMusicVolume(volumePower);
    }
    public void SliderVolumeVFXChange()
    {
        volumeFXPower = sliderFXVolume.value;
        SoundManager.instance.SetAllFXVolume(volumeFXPower);
        SoundManager.instance.Play("Hit");
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: "+PlayerPrefs.GetInt("Score", 0).ToString();
    }
}
