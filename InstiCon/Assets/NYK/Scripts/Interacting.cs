using Unity.Netcode;
using UnityEngine;

public class Interacting : NetworkBehaviour {
    public GameObject bannerCanvas;

    private void Start() {
        bannerCanvas = GameObject.Find("BannerCanvas");
        if (bannerCanvas != null) {
            bannerCanvas.SetActive(false);
        }

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            float interactRange = 2f;
            Collider[] collider = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider c in collider) {
                if (c.TryGetComponent(out NPC_Interactable interactable)) {
                    interactable.Interact();
                }
                if (c.TryGetComponent(out Banner banner)) {
                    if (IsHost) {
                        Debug.Log("Interacting with banner");
                        bannerCanvas.SetActive(true);

                        // Unlock the cursor
                        var inputs = FindFirstObjectByType<StarterAssets.StarterAssetsInputs>();
                        if (inputs != null) {
                            inputs.cursorLocked = false;
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                        }
                    }
                }
            }
        }
    }
}
