using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSystem : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject optionsPanel;   
    
    void Start()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);        
    }

    public void OptionsButtonPressed()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void MenuToGameScene()
    {
        SceneManager.LoadScene("Deneme");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.None;
    }

    public void OptionBackButton()
    {
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
