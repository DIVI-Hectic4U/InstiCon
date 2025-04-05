using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MascotInteraction : NetworkBehaviour

{

    public void Interact(string s)
    {
        Debug.Log(s);
    }
}
