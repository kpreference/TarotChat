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

    // ���� ũ��/������
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

    // �޸� ���� + ���� ũ�� ����
    public void SetBack(Sprite back)
    {
        if (!sr) return;
        sr.sprite = back;

        // �޸� ũ�⸦ �������� ���
        refLocalScale = transform.localScale;
        refWorldSize = sr.bounds.size;
    }

    // �ո� ���� + ũ�� ����
    public void SetFront(Sprite face)
    {
        if (!sr || !face) return;
        sr.sprite = face;

        // ���� ũ��
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
