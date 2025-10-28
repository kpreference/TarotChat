// Bubble.cs
using UnityEngine;
using TMPro;

public class Bubble : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;  // ������ ���� TMP ����

    public void SetText(string message)
    {
        if (text != null) text.text = message;
    }
}
