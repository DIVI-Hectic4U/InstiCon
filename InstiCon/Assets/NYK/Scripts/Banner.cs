using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class Banner : NetworkBehaviour {
    public GameObject plane;
    private string currentTextureUrl = "";

    public override void OnNetworkSpawn() {
        if (IsClient && !string.IsNullOrEmpty(currentTextureUrl)) {
            ApplyTextureClientRpc(currentTextureUrl);
        }
    }

    public void Interact(string s) {
        //string newTextureUrl = "https://upload.wikimedia.org/wikipedia/commons/6/6a/JavaScript-logo.png";

        // Call the ServerRpc to update the texture
        SetTextureServerRpc(s);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetTextureServerRpc(string url) {
        currentTextureUrl = url;
        ApplyTextureClientRpc(url); // Send texture update to all clients
    }

    [ClientRpc]
    private void ApplyTextureClientRpc(string url) {
        StartCoroutine(LoadImage(url));
    }

    IEnumerator LoadImage(string url) {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url)) {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError("Error loading image: " + request.error);
            } else {
                Texture2D texture2D = DownloadHandlerTexture.GetContent(request);

                if (texture2D != null && plane != null) {
                    Renderer renderer = plane.GetComponent<Renderer>();
                    if (renderer != null) {
                        renderer.material.mainTexture = texture2D;
                    } else {
                        Debug.LogError("Renderer not found on plane.");
                    }
                } else {
                    Debug.LogError("Downloaded texture is null or plane is not assigned.");
                }
            }
        }
    }
}
