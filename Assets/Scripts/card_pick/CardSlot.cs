// CardSlot.cs
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
    [SerializeField] Image cardImage;        // 카드 보여줄 Image(뒤면/앞면 교체)
    [SerializeField] GameObject selectMark;  // 선택 표시(테두리/오버레이 등, 없으면 null)
    [HideInInspector] public int slotIndex;  // 컨트롤러에서 부여

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
