using UnityEngine;

public class CloseButton : MonoBehaviour
{
    private GameObject askSimba;

    void Start()
    {
        askSimba = GameManager.Instance.askSimba;
    }

    // This method should be public to be called from a UI Button's OnClick()
    public void ClosePanel()
    {
        if (askSimba != null)
        {
            askSimba.SetActive(false);
        }
    }
}
