using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] Button hostButton;
    [SerializeField] Button joinButton;
    [SerializeField] GameObject menu;

    void Start()
    {
        hostButton.onClick.AddListener(Host);
        joinButton.onClick.AddListener(Join);
    }

    void Host()
    {
        NetworkManager.Singleton.StartHost();
        menu.SetActive(false);
        
    }

    void Join()
    {
        NetworkManager.Singleton.StartClient();
        menu.SetActive(false);
        
    }
}
