//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] //Popup menus
    private GameObject settingsMenu,
        keybindsMenu;

    [SerializeField] //Settings sliders
    private Slider MVolumeSlider,
        BGMVolumeSlider,
        SFXVolumeSlider,
        TextSpeedSlider;

    [SerializeField] //Settings slider value texts
    private TMP_Text MVolumeText,
        BGMVolumeText,
        SFXVolumeText,
        TextSpeedText;

    [SerializeField] //Main menu background music
    private AudioMixer audioMixer;

    private void Awake()
    {
        //Check if registry key already exists
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            //If not, set standard values and create keys
            PlayerPrefs.SetFloat("MasterVolume", 0.5f);
            PlayerPrefs.SetFloat("BGMVolume", 0.5f);
            PlayerPrefs.SetFloat("SFXVolume", 0.5f);
            PlayerPrefs.SetFloat("TextSpeed", 5);
            PlayerPrefs.Save();
        }

        //Get saved preference for master volume and set slider and value text to it
        MVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(MVolumeSlider.value) * 20);
        MVolumeText.SetText((int)(MVolumeSlider.value * 100) + "%");

        //Get saved preference for music volume and set slider and value text to it
        BGMVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(BGMVolumeSlider.value) * 20);
        BGMVolumeText.SetText((int)(BGMVolumeSlider.value * 100) + "%");

        //Get saved preference for effects volume and set slider and value text to it
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXVolumeSlider.value) * 20);
        SFXVolumeText.SetText((int)(SFXVolumeSlider.value * 100) + "%");

        //Get saved preference for text speed and set slider and value text to it
        TextSpeedSlider.value = PlayerPrefs.GetFloat("TextSpeed");
        TextSpeedText.SetText(((int)(TextSpeedSlider.value)).ToString());
    }

    //Called when value of MVolumeSlider changes
    public void ChangeMasterVolume()
    {
        //Set AudioListener's volume to value
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(MVolumeSlider.value) * 20);
        //Update value text
        MVolumeText.SetText((int)(MVolumeSlider.value * 100) + "%");
        //Save preference
        PlayerPrefs.SetFloat("MasterVolume", MVolumeSlider.value);
        PlayerPrefs.Save();
    }

    //Called when value of BGMVolume changes
    public void ChangeBGMVolume()
    {
        //Set Audiosource's volume to value
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(BGMVolumeSlider.value) * 20);
        //Update value text
        BGMVolumeText.SetText((int)(BGMVolumeSlider.value * 100) + "%");
        //Save preference
        PlayerPrefs.SetFloat("BGMVolume", BGMVolumeSlider.value);
        PlayerPrefs.Save();
    }

    //Called when value of SFXVolume changes
    public void ChangeSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXVolumeSlider.value) * 20);
        //Update value text
        SFXVolumeText.SetText((int)(SFXVolumeSlider.value * 100) + "%");
        //Save preference
        PlayerPrefs.SetFloat("SFXVolume", SFXVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void ChangeTextSpeed()
    {
        //Update value text
        TextSpeedText.SetText(((int)(TextSpeedSlider.value)).ToString());
        PlayerPrefs.SetFloat("TextSpeed", TextSpeedSlider.value);
        PlayerPrefs.Save();
    }

    //Called by Exit Game button
    public void Exit()
    {
        Application.Quit();
    }

    //Called by Start Level button
    public void StartLevel()
    {
        //Load the level
        SceneLoaderData.sceneToLoad = ((Scenes)1).ToString();
        SceneManager.LoadScene("Loading Screen");
    }

    //Called by Keybinds-button
    public void ToggleKeybindsMenu()
    {
        keybindsMenu.SetActive(!keybindsMenu.activeSelf);
    }

    //Called by Settings-button
    public void ToggleSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }
}
