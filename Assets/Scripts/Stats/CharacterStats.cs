using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] protected bool isDead;

    public int _currentScore;
    public int _maxScore;
    public Text _ScoreText;

    public PanelSystem1 panel;

    private void Start()
    {
        InitVariables();
    }

    public virtual void CheckHealth()
    {
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void Die()
    {
        isDead = true;
        StartCoroutine(DeadScreen());
    }

    public IEnumerator DeadScreen()
    {
        //Debug.LogWarning("Dead");

        panel.DiedPanel.SetActive(true);
        panel.Crosshair.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield return new WaitForSeconds(0.1f);
    }

    public void SetHealthTo(float healthToSetTo)
    {
        health = healthToSetTo;
        CheckHealth();
    }

    public void TakeDamage(float damage)
    {
        float healthAfterDamage = health - damage;
        SetHealthTo(healthAfterDamage);
    }

    public void Heal(float heal)
    {
        float healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal);
    }

    public virtual void InitVariables()
    {
        health = 100;
        maxHealth = 100;

        _maxScore = 9999;
        _currentScore = 0;
        _ScoreText.text = " " + _currentScore;

        SetHealthTo(maxHealth);
        isDead = false;
    }
}
