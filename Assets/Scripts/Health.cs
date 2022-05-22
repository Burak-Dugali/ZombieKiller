using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : CharacterStats
{
    //public GameObject health;
    private PlayerStats stats;
    private PlayerHUD hud;
    //public int heal = 20;

    public void Start()
    {
        InitVariables();
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        hud.UpdateHealth(health, maxHealth);
    }
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.LogError("Deneme");
    //        DontDestroyOnLoad(gameObject);
    //        Heal(20);
    //        Destroy(gameObject);
    //        hud.UpdateHealth(health, maxHealth);
    //    }
    //}
}