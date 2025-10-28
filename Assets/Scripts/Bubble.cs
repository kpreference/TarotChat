// Bubble.cs
using UnityEngine;
using TMPro;

public class Bubble : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;  // 프리팹 안의 TMP 지정

    public void SetText(string message)
    {
        if (text != null) text.text = message;
    }
}
