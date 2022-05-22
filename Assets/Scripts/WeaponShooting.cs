using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    private Camera cam;
    private float weaponRange;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Shoot
        }
    }

    private void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, weaponRange))
        {

        }
    }

    private void GetReferences()
    {
        cam = GetComponentInChildren<Camera>();
    }
}
