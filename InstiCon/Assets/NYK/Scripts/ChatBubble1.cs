using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ChatBubble1:NetworkBehaviour
{
    private TextMeshPro textMeshPro;
    private SpriteRenderer background;
    private void Awake() {
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
        background = transform.Find("Background").GetComponent<SpriteRenderer>();
    }
    private void Setup(string text) {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textsize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(3f, 3f);
       // background.size = textsize + padding;
    }
    public static void Create(Vector3 position, string text, Transform parent = null) {
        GameObject chatBubblePrefab = Resources.Load<GameObject>("ChatBubble");
        if (chatBubblePrefab == null) {
            Debug.LogError("ChatBubble not found in Resources folder.");
            return;
        }

        GameObject chatBubbleObject = Instantiate(chatBubblePrefab, parent);
        chatBubbleObject.transform.localPosition = position;
        chatBubbleObject.GetComponent<ChatBubble1>().Setup(text);
        Destroy(chatBubbleObject, 5f);
    }
}
