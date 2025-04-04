using UnityEngine;

public class ReadInput : MonoBehaviour
{
    public string prompt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string s)
    {
        prompt = s;
        Debug.Log(prompt);
    }
}
