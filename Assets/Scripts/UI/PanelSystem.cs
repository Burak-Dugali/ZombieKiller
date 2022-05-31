using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSystem : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject ResetPanel;

    [Header("Options")]
    public GameObject optionsPanelPopUp;
    public Animator optionsanim;

    [Header("Exit")]
    public GameObject exitpopUpPanel;
    public Animator exitAnim;
    //public string exitpopUp;
    
    [Header("Reset")]
    public GameObject resetSurepopUpPanel;
    public Animator resetSureAnim;
    
    [Header("Game Reseted")]
    public GameObject gamereseted;
    public Animator gameResetedanim;
    //public string resetSurepopUp;

    void Start()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        ResetPanel.SetActive(false);
        gamereseted.SetActive(false);
        exitpopUpPanel.SetActive(false);
        resetSurepopUpPanel.SetActive(false);
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

    public void OptionsButtonPressed()
    {
        optionsPanel.SetActive(true);
        mainPanel.SetActive(false);
        optionsanim.SetTrigger("popOptions");
    }

    public void OptionsPanelClosee()
    {
        StartCoroutine(OptionsPanelClose());
    }

    public IEnumerator OptionsPanelClose()
    {
        optionsanim.SetTrigger("closeOptions");
        yield return new WaitForSeconds(0.6f);
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ResetGamePanelOpen()
    {
        resetSurepopUpPanel.SetActive(true);
        optionsPanel.SetActive(false);
        resetSureAnim.SetTrigger("popReset");
    }

    public void ResetGamePanelClose()
    {
        ResetPanel.SetActive(false);
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ExitAreYouSurePanelOpen()
    {
        exitpopUpPanel.SetActive(true);
        mainPanel.SetActive(false);
        exitAnim.SetTrigger("popExit");
    }

    public void ResetAreYouSurePanelOpen()
    {
        resetSurepopUpPanel.SetActive(true);
        optionsPanel.SetActive(false);
        resetSureAnim.SetTrigger("popReset");
    }

    public void ExitAreYouSurePanelClose()
    {
        StartCoroutine(ExitSureClose());
        
    }

    public void ResetAreYouSurePanelClose()
    {
        StartCoroutine(ResetSureClose());
    }

    private IEnumerator ExitSureClose()
    {
        exitAnim.SetTrigger("closeExit");
        yield return new WaitForSeconds(0.6f);
        exitpopUpPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    private IEnumerator ResetSureClose()
    {
        resetSureAnim.SetTrigger("closeReset");
        yield return new WaitForSeconds(0.6f);
        resetSurepopUpPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    /// <summary>
    
    public IEnumerator GameResetedOK()
    {
        gameResetedanim.SetTrigger("closeReseted");
        yield return new WaitForSeconds(0.6f);
        gamereseted.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void GameReseted()
    {
        gamereseted.SetActive(true);
        resetSurepopUpPanel.SetActive(false);
        gameResetedanim.SetTrigger("popReseted");
    }

    public void GameResetedOk()
    {
        StartCoroutine(GameResetedOK());
    }

    /// </summary>

    public void ExitGame()
    {
        Application.Quit();
    }

}
