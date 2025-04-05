using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ChatMessage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TextMeshProUGUI messageText;
    public void SetText(string text)
    {
        messageText.text = text;
    }
}