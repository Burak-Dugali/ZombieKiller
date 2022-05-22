using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Text currenthealthText;
    [SerializeField] private Text maxhealthText;

    [SerializeField] private Text currentAmmoText;
    [SerializeField] private Text maxAmmoText;

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        currenthealthText.text = currentHealth.ToString();
        maxhealthText.text = maxHealth.ToString();
    }

    public void UpdateAmmo(float currentAmmo, float maxAmmo)
    {
        currentAmmoText.text = currentAmmo.ToString();
        maxAmmoText.text = maxAmmo.ToString();
    }
}
