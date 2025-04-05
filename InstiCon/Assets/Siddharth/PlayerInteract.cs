using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject askSimba;
    private void Start()
    {
        askSimba = GameObject.Find("AskSimba");
        askSimba.SetActive(false);
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 3f;
            //Physics.OverlapSphere(transform.position, interactRange);
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out MascotInteraction mascotinteraction))
                {
                    askSimba.SetActive(true);
                }
            }
        }
    }
}