using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CardSelectable : MonoBehaviour, IPointerClickHandler
{
    [Header("Visual")]
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color selectedColor = new Color(1f, 0.9f, 0.5f, 1f);

    [HideInInspector] public int index;
    public bool IsSelected { get; private set; }

    // 기준 크기/스케일
    private Vector2 refWorldSize;
    private Vector3 refLocalScale;

    void Reset()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Awake()
    {
        if (!sr) sr = GetComponent<SpriteRenderer>();
        SetSelected(false, instant: true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardPickerController.Instance?.ToggleSlot(index);
    }

    // 뒷면 적용 + 기준 크기 저장
    public void SetBack(Sprite back)
    {
        if (!sr) return;
        sr.sprite = back;

        // 뒷면 크기를 기준으로 기억
        refLocalScale = transform.localScale;
        refWorldSize = sr.bounds.size;
    }

    // 앞면 적용 + 크기 보정
    public void SetFront(Sprite face)
    {
        if (!sr || !face) return;
        sr.sprite = face;

        // 현재 크기
        var nowSize = sr.bounds.size;
        if (nowSize.x > 0.0001f && nowSize.y > 0.0001f)
        {
            var scale = transform.localScale;
            scale.x *= refWorldSize.x / nowSize.x;
            scale.y *= refWorldSize.y / nowSize.y;
            transform.localScale = scale;
        }
    }

    public void SetSelected(bool on, bool instant = false)
    {
        IsSelected = on;
        if (!sr) return;
        sr.color = on ? selectedColor : normalColor;
    }
}
