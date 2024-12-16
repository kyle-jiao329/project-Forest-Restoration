using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float detectionRange = 1f;
    public GameObject biochar;
    public GameObject detector;
    public GameObject Netbag;
    public GameObject Cake;

    void pickoff()
    {
        biochar.SetActive(false);
        detector.SetActive(false);
        Netbag.SetActive(false);
        Cake.SetActive(false);
    }


    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("BiocharSpace"))
            {
                pickoff();
                biochar.SetActive(true);
            }
            if (collider.CompareTag("DetectorSpace"))
            {
                pickoff();
                detector.SetActive(true);
            }
            if (collider.CompareTag("NetBagSpace"))
            {
                pickoff();
                Netbag.SetActive(true);
            }
            if (collider.CompareTag("CakeSpace"))
            {
                pickoff();
                Cake.SetActive(true);
            }
            
        }
    }
}