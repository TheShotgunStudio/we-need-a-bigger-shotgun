using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingsMenu;
    public UpgradeDisplay UpgradeDisplay;

    //Just for now we eventually want to pause everything in the game manager.
    private bool _isPaused = false;

    /// <summary>
    /// Called whenever the on-screen pause button is pressed
    /// </summary>
    public void OpenPauseMenu() {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        _isPaused = true;
    }

    /// <summary>
    /// Called whenever the "Resume Game" button is pressed within the pause menu
    /// </summary>
    public void ClosePauseMenu() {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        _isPaused = false;
    }

    /// <summary>
    /// Called whenever the "Settings" button is pressed within the pause menu
    /// </summary>
    public void OpenSettings() {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    /// <summary>
    /// Called whenever the "Return" button is pressed within the settings menu
    /// </summary>
    public void CloseSettings() {
        SettingsMenu.SetActive(false);
        PauseMenu.SetActive(true);
    }

    /// <summary>
    /// Called whenever the "Return to menu" button is pressed within the pause menu
    /// </summary>
    public void ReturnToMenu() {
        // Add implementation
    }

    /// <summary>
    /// Called whenever the "Volume" slider is adjusted within the pause menu
    /// </summary>
    /// <param name="volumeValue">New desired volume level on a scale of 0-100</param>
    public void ChangeVolume(float volumeValue) {
        // Add implementation
    }

    /// <summary>
    /// Called whenever the "SFX" slider is adjusted within the pause menu
    /// </summary>
    /// <param name="sfxValue">New desired Sound effects volume level on a scale of 0-100</param>
    public void ChangeSFXVolume(float sfxValue) {
        // Add implementation
    }

    public void OpenUpgradeDisplay()
    {
        UpgradeDisplay.gameObject.SetActive(true);
        UpgradeDisplay.Initialize();
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!_isPaused){
                OpenPauseMenu();
            }
            else{
                ClosePauseMenu();
            }
        }


    }
}
