// CardSlot.cs
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
    [SerializeField] Image cardImage;        // ī�� ������ Image(�ڸ�/�ո� ��ü)
    [SerializeField] GameObject selectMark;  // ���� ǥ��(�׵θ�/�������� ��, ������ null)
    [HideInInspector] public int slotIndex;  // ��Ʈ�ѷ����� �ο�

    public bool IsSelected { get; private set; }

    public void Init(int index)
    {
        slotIndex = index;
        SetSelected(false);
    }

    public void SetBack(Sprite backSprite)
    {
        if (cardImage) cardImage.sprite = backSprite;
    }

    public void SetFront(Sprite frontSprite)
    {
        if (cardImage) cardImage.sprite = frontSprite;
    }

    public void ToggleSelect()
    {
        SetSelected(!IsSelected);
    }

    public void SetSelected(bool on)
    {
        IsSelected = on;
        if (selectMark) selectMark.SetActive(on);
    }
}
