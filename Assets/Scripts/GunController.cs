using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController: MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera maincamera;
    private RaycastHit hitGun;
    [SerializeField] private ParticleSystem muzzleFlash;
    private bool canShoot;

    void Start()
    {
        
    }

    void Update()
    { 
        shooting();
    }

    void shooting()
    {

        if (Input.GetMouseButtonDown(0))
        {

            ParticleSystem p = Instantiate(muzzleFlash, transform.position + transform.forward, transform.rotation);
            p.Play();

            if (Physics.Raycast(maincamera.transform.position, maincamera.transform.TransformDirection(Vector3.forward), out hitGun, 200))
            {

            }

            if (Physics.Raycast(maincamera.transform.position, maincamera.transform.TransformDirection(Vector3.forward), out hitGun, 200) && (hitGun.rigidbody))
            {
                hitGun.rigidbody.AddForce(player.transform.forward * 500);
            }
            else if (Physics.Raycast(maincamera.transform.position, maincamera.transform.TransformDirection(Vector3.forward), out hitGun, 200) && (hitGun.rigidbody))
            {
                hitGun.rigidbody.AddForce(player.transform.forward * 100);
            }

        }
    }
}