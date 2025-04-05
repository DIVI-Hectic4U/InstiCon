using UnityEngine;
using Unity.Netcode;

public class PlayerInteract : NetworkBehaviour
{
    private GameObject askSimba;
    private void Start()
    {
        if(askSimba != null) askSimba.SetActive(false);
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // Reference the same AskSimba for all players
        askSimba = GameManager.Instance.askSimba;

        // Let host disable it on startup
        if (IsHost && askSimba != null)
        {
            askSimba.SetActive(false);
        }
    }

    private void Update()
    {
        if (!IsOwner) return; // Only local player checks input

        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 4f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out MascotInteraction mascotinteraction))
                {
                    if (askSimba != null)
                    {
                        askSimba.SetActive(true);
                    }
                }
            }
        }
    }
}
