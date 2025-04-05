using UnityEngine;
using Unity.Netcode;
public class NPC_Interactable : NetworkBehaviour {
    public string text = "Hello";
    public void Interact() {
        Debug.Log("interacting");
        ChatBubble1.Create(new Vector3(1f, 1f, 0f), text, transform.transform);
    }
}
