using System.Linq;
using UnityEngine;

public class ReadInput : MonoBehaviour
{
    private string prompt;
    public MascotInteraction mascotInteraction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject askSimba;
    private void Start()
    {
        askSimba = GameObject.Find("AskSimba");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string s)
    {
        prompt = s;
        Debug.Log(prompt);
        mascotInteraction.Interact(prompt);
        
    }
}
