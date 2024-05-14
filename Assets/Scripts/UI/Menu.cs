//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] //Popup menus
    private GameObject mainMenu,
        settingsMenu,
        bg,
        lvlEndBox;

    [SerializeField] //Settings sliders
    private Slider MVolumeSlider,
        BGMVolumeSlider,
        SFXVolumeSlider;

    [SerializeField] //Settings slider value texts
    private TMP_Text MVolumeText,
        BGMVolumeText,
        SFXVolumeText;

    [SerializeField]
    private Player player;

    [SerializeField] //Sound sources
    private AudioSource bgm,
        sfx;

    public static bool gameIsPaused = false;

    //Called by Exit Game button
    public void Exit()
    {
        Application.Quit();
    }

    void Awake()
    {
        gameIsPaused = false;
        //Get saved preference for master volume and set slider and value text to it
        MVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        AudioListener.volume = MVolumeSlider.value;
        MVolumeText.SetText((int)(MVolumeSlider.value * 100) + "%");

        //Get saved preference for music volume and set slider and value text to it
        BGMVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        bgm.volume = BGMVolumeSlider.value;
        BGMVolumeText.SetText((int)(BGMVolumeSlider.value * 100) + "%");

        //Get saved preference for effects volume and set slider and value text to it
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        sfx.volume = SFXVolumeSlider.value;
        SFXVolumeText.SetText((int)(SFXVolumeSlider.value * 100) + "%");
    }

    //Called when value of MVolumeSlider changes
    public void ChangeMasterVolume()
    {
        //Set AudioListener's volume to value
        AudioListener.volume = MVolumeSlider.value;
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
        bgm.volume = BGMVolumeSlider.value;
        //Update value text
        BGMVolumeText.SetText((int)(BGMVolumeSlider.value * 100) + "%");
        //Save preference
        PlayerPrefs.SetFloat("BGMVolume", BGMVolumeSlider.value);
        PlayerPrefs.Save();
    }

    //Called when value of SFXVolume changes
    public void ChangeSFXVolume()
    {
        //Set Audiosource's volume to value
        sfx.volume = SFXVolumeSlider.value;
        //Update value text
        SFXVolumeText.SetText((int)(SFXVolumeSlider.value * 100) + "%");
        //Save preference
        PlayerPrefs.SetFloat("SFXVolume", SFXVolumeSlider.value);
        PlayerPrefs.Save();
    }

    //Called by the BackToMenu button
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Called by Escape key
    public void OnToggle(InputValue value)
    {
        //if EndBox isn't active
        if (!lvlEndBox.activeSelf)
        {
            //If Settings are shown
            if (settingsMenu.activeSelf)
            {
                gameIsPaused = !gameIsPaused;
                //Hide all menus
                settingsMenu.SetActive(false);
                bg.SetActive(false);
                mainMenu.SetActive(false);
                ToggleCursor();
            }
            else //If Settings aren't shown
                ToggleMenu();
        }
    }

    //Checks if either menu or settings are open
    public bool IsMenuOpen()
    {
        return mainMenu.activeSelf || settingsMenu.activeSelf;
    }

    //Called by Resume button
    public void Resume()
    {
        ToggleMenu();
    }

    // Turns menu on/off
    public void ToggleMenu()
    {
        gameIsPaused = !gameIsPaused;
        mainMenu.SetActive(!mainMenu.activeSelf);
        bg.SetActive(!bg.activeSelf);
        ToggleCursor();
    }

    //Hides/Shows cursor and locks/unlocks it
    private void ToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = !Cursor.visible;
    }

    //Called by Settings button
    public void ToggleSettings()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }
}
