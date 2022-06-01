using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    
    public int currentAmmo;
    public int maxAmmo;
    public float reloadtime = 2f;
    private bool isReloading = false;
    public bool canShoot;

    public GameObject GunLight;
    //private LayerMask layerMask;
    private RaycastHit hit;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public Text currentAmmoText;
    public Text maxAmmoText;
    public Animator anim;

    //public PanelSystem1 panel1;

    public void Start()
    {
        maxAmmo = 20;
        currentAmmo = maxAmmo;
        currentAmmoText.text = currentAmmo.ToString();
        maxAmmoText.text = maxAmmo.ToString();
        canShoot = true;
    }

    public void Update()
    {
        //Debug.Log(canShoot);
        currentAmmoText.text = currentAmmo.ToString();
        maxAmmoText.text = maxAmmo.ToString();

        if(isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            Reload();
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < 20)
        {
            Reload();
        }
    }

    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void Reload()
    {
        isReloading = true;
        canShoot = false;
        Debug.LogError("Reloading...");
        StartCoroutine(ReloadDelay());
    }

    public IEnumerator ReloadDelay()
    {
        //maxAmmo = maxAmmo - currentAmmo;
        //maxAmmoText.text = maxAmmo.ToString();

        anim.SetBool("ReloadTrigger",true);
        yield return new WaitForSeconds(reloadtime);
        anim.SetBool("ReloadTrigger", false);
        currentAmmo = maxAmmo;
        isReloading = false;
        canShoot = true;
    }

    public IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.1f);
        currentAmmo--;
        currentAmmoText.text = currentAmmo.ToString();
        maxAmmoText.text = maxAmmo.ToString();
    }

    private void Shoot()
    {
        if(canShoot == true)
        {
            StartCoroutine(ShootDelay());
            Instantiate(GunLight,this.transform);
            muzzleFlash.Play();

            //RaycastHit hit;

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.LogWarning(hit.transform.name);
            }

            //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            //{
            //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //    Debug.Log("Hit");
            //}
            //else
            //{
            //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //    Debug.Log("No Hit");
            //}

            CharacterStats stats = hit.transform.GetComponent<CharacterStats>();
            if (stats != null)
            {
                stats.TakeDamage(damage);
            }
        }
    }
}
