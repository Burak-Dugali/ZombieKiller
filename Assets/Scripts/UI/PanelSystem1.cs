using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSystem1 : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject DiedPanel;
    public bool escPressed = false;
    public GameObject Crosshair;
    private Gun gun;

    private void Start()
    {
        PausePanel.SetActive(false);
        DiedPanel.SetActive(false);
        //LockCursor();
        Crosshair.SetActive(true);
    }

    public void Update()
    {
        EscapeReferences();
    }

    public virtual void EscapeReferences()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && escPressed == false)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            escPressed = true;
            UnLockCursor();
            Crosshair.SetActive(false);
            gun.canShoot = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && escPressed == true)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            escPressed = false;
            LockCursor();
            Crosshair.SetActive(true);
            gun.canShoot = false;
        }
    }

    public void GameToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        LockCursor();
        escPressed = false;
    }

    public void DiedPanelRestart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnLockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
