using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject askSimba;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        if (askSimba != null)
        {
            askSimba.SetActive(false);
        }
    }
    private void Start()
    {
        if(askSimba != null)
        {
            askSimba.SetActive(false);
        }
    }
}
