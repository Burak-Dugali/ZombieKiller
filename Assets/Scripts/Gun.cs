using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 150f;
    
    public int currentAmmo;
    public int maxAmmo;
    public int magazineMaxAmmo;

    public float reloadtime = 3f;
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
    public AudioSource GunShoot;
    public AudioSource ReloadSound;
    public AudioSource EmptyGunSound;

    //public PanelSystem1 panel1;

    public void Start()
    {   
        currentAmmoText.text = currentAmmo.ToString();
        maxAmmoText.text = maxAmmo.ToString();
        canShoot = true;
        maxAmmo = 30;
        magazineMaxAmmo = 20; 
        currentAmmo = magazineMaxAmmo;
    }

    public void Awake()
    {
        
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

        //if (currentAmmo <= 0 && maxAmmo>0)
        //{
        //    Reload();
        //    return;
        //}

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
        //Debug.LogWarning("Reloading...");
        StartCoroutine(ReloadDelay());
    }

    public IEnumerator ReloadDelay()
    {
        ReloadSound.Play();
        anim.SetBool("ReloadTrigger",true);
        yield return new WaitForSeconds(reloadtime);
        anim.SetBool("ReloadTrigger", false);
        maxAmmo -= magazineMaxAmmo - currentAmmo;
        currentAmmo = magazineMaxAmmo;
        if (maxAmmo < 0)
        {
            currentAmmo += maxAmmo;
            maxAmmo = 0;
        }
        
        isReloading = false;
        canShoot = true;
    }

    public IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.1f);
        GunShoot.Play();
        muzzleFlash.Play();
        anim.SetBool("Recoil",true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Recoil", false);
        currentAmmo--;
        currentAmmoText.text = currentAmmo.ToString();
        maxAmmoText.text = maxAmmo.ToString();
    }

    private void Shoot()
    {

        if (currentAmmo<=0)
        {
            EmptyGunSound.Play();
        }
        else if(canShoot == true && currentAmmo>0)
        {
            StartCoroutine(ShootDelay());
            Instantiate(GunLight,this.transform);

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
