using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSystem1 : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject DiedPanel;
    public GameObject SaveText;
    public GameObject Crosshair;

    
    public bool escPressed = false;
    public Gun gun;
    public PlayerHUD hud;
    public CharacterStats stats;
    public AudioSource windSound;

    //public GameObject GamePausedText;
    //public GameObject ResumeButton;
    //public GameObject SaveButton;
    //public GameObject MenuButton;

    private void Start()
    {
        gun.currentAmmo = PlayerPrefs.GetInt("CurrentAmmo");
        gun.maxAmmo = PlayerPrefs.GetInt("MaxAmmo");
        stats._currentScore = PlayerPrefs.GetInt("CurrentMoney");
        stats.health = PlayerPrefs.GetFloat("CurrentHealth");
        hud.UpdateAmmo(gun.currentAmmo,gun.maxAmmo);
        hud.UpdateHealth(stats.health,stats.maxHealth);



        PausePanel.SetActive(false);
        DiedPanel.SetActive(false);
        SaveText.SetActive(false);
        Crosshair.SetActive(true);
        LockCursor();
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
            gun.canShoot = false;
            //windSound.Stop();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && escPressed == true)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            escPressed = false;
            LockCursor();
            Crosshair.SetActive(true);
            gun.canShoot = true;
            //windSound.Play();
        }
    }

    public IEnumerator SavedTextDelay()
    {
        SaveText.SetActive(true);
        yield return new WaitForSeconds(1f);
        SaveText.SetActive(false);
    }

    public void GameToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Crosshair.SetActive(true);
        Time.timeScale = 1;
        LockCursor();
        escPressed = false;
        gun.canShoot = true;
    }

    public void SaveButtonPressed()
    {
        StartCoroutine(SaveSystem());
    }

    public IEnumerator SaveSystem()
    {
        SaveText.SetActive(true);
        PlayerPrefs.SetInt("CurrentAmmo",gun.currentAmmo);
        PlayerPrefs.SetInt("MaxAmmo",gun.maxAmmo);
        PlayerPrefs.SetInt("CurrentMoney",stats._currentScore);
        PlayerPrefs.SetFloat("CurrentHealth",stats.health);

        PlayerPrefs.Save();

        yield return new WaitForSeconds(1f);
        SaveText.SetActive(false);
        PausePanel.SetActive(false);
    }

    public void DiedPanelRestart()
    {
        SceneManager.LoadScene("Deneme");
        Time.timeScale = 1f;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnLockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
