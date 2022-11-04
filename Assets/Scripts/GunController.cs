using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController: MonoBehaviour
{
    [SerializeField] private GameObject bullet;

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

            GameObject p = Instantiate(bullet, transform.position, Quaternion.identity);
            p.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 300, ForceMode.Impulse);

        }
    }
}