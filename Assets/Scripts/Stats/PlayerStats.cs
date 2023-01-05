using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private PlayerHUD hud;
    //private Health manager;
    //public GameObject healthhh;

    private void Start()
    {
        GetReferences();
        InitVariables();
    }

    private void GetReferences()
    {
        hud = GetComponent<PlayerHUD>();
        //manager = GetComponent<Health>();
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        hud.UpdateHealth(health, maxHealth);
        //hud.UpdateAmmo(ammo, maxAmmo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }
    // public void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Health"))
    //     {
    //         DontDestroyOnLoad(healthhh);
    //         Debug.LogError("Deneme");
    //         Heal(20);
    //         Destroy(healthhh);
    //         hud.UpdateHealth(health, maxHealth);
    //     }
    // }
}
