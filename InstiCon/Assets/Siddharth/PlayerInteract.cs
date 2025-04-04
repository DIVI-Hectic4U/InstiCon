using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
   private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 2f;
            Physics.OverlapSphere(transform.position, interactRange);
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
               if (collider.TryGetComponent(out MascotInteraction mascotInteraction)){
                    mascotInteraction.Interact();
                    
                }
            }
        }
    }
}
