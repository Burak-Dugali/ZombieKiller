using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected bool isDead;

    public float _currentMoney;
    public float _maxMoney;
    public Text _moneyText;
    //[SerializeField] private GameObject enemy;

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

    //public virtual void CheckAmmo()
    //{
    //    if(currentAmmo <= 0)
    //    {
    //        currentAmmo = 0;
    //        //Reload();
    //    }
    //    if(currentAmmo >= maxAmmo)
    //    {
    //        currentAmmo = maxAmmo;
    //    }
    //}

    public virtual void Die()
    {
        isDead = true;
        StartCoroutine(DeadScreen());
    }

    private IEnumerator DeadScreen()
    {
        panel.DiedPanel.SetActive(true);
        panel.Crosshair.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0f;
    }

    public void SetHealthTo(float healthToSetTo)
    {
        health = healthToSetTo;
        CheckHealth();
    }

    //public void SetAmmoTo(float ammoToSetTo)
    //{
    //    currentAmmo = ammoToSetTo;
    //    CheckAmmo();
    //}

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

        //maxAmmo = 90;
        //SetAmmoTo(maxAmmo);

        _maxMoney = 500;
        _currentMoney = _maxMoney;
        _moneyText.text = " " + _currentMoney;

        SetHealthTo(maxHealth);
        isDead = false;
    }
}
