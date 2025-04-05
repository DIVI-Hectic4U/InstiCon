using UnityEngine;
using Unity.Netcode;
using StarterAssets; // Add this if you're using the StarterAssetsInputs script

public class ReadInput : NetworkBehaviour {
    public Banner banner;
    private string s;
    public GameObject bannerCanvas;

    private void Start() {
        bannerCanvas = GameObject.Find("BannerCanvas");
    }

    public void ReadInputText(string input) {
        Debug.Log("Trying to read input");
        s = input;
        if (banner != null) {
            Debug.Log("Input received: " + s);
            banner.Interact(s);
            bannerCanvas.SetActive(false);

            // Re-lock the cursor
            var inputs = FindFirstObjectByType<StarterAssetsInputs>();
            if (inputs != null) {
                inputs.cursorLocked = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        } else {
            Debug.LogError("Banner is not assigned!");
        }
    }
}
