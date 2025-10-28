using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class BubbleCardView : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private float maxHeight = 520f;   // ī�� ���� �ִ� ����
    [SerializeField] private float sidePadding = 16f;  // content �¿� �е��� ���߱�

    void Reset()
    {
        if (!cardImage) cardImage = GetComponentInChildren<Image>(true);
    }

    public void SetCard(Sprite sprite)
    {
        if (!cardImage)
        {
            Debug.LogError($"[BubbleCardView] Image reference missing on {name}");
            return;
        }

        cardImage.enabled = sprite != null;
        cardImage.sprite = sprite;
        cardImage.preserveAspect = true;

        if (!sprite) return;

        // 1) content ������ �������� �ִ��� ���
        var parentRT = transform.parent as RectTransform;
        float parentWidth = parentRT ? parentRT.rect.width : 0f;
        // ���̾ƿ� �е� ���� (�¿� ����)
        float maxWidth = Mathf.Max(0f, parentWidth - sidePadding * 2f);

        // 2) ��������Ʈ ���� ����
        float w = sprite.rect.width;
        float h = sprite.rect.height;
        float aspect = w / Mathf.Max(1f, h);

        // 3) ���� �켱 ���� �� �� ���� ���� ����
        float targetH = Mathf.Min(maxHeight, h);
        float targetW = targetH * aspect;

        if (maxWidth > 0f && targetW > maxWidth)
        {
            targetW = maxWidth;
            targetH = targetW / Mathf.Max(0.0001f, aspect);
        }

        // 4) Image Rect ����
        var rt = cardImage.rectTransform;
        rt.sizeDelta = new Vector2(targetW, targetH);
        rt.anchoredPosition = new Vector2(0, 0); // ���̾ƿ��� ��ġ�� å�������� 0����

        // 5) LayoutElement�� ���� ���� (���� ���̾ƿ� ����)
        var le = cardImage.GetComponent<LayoutElement>();
        if (!le) le = cardImage.gameObject.AddComponent<LayoutElement>();
        le.preferredHeight = targetH;
        le.preferredWidth = targetW;

        // 6) ���� ��Ʈ�� ���� ���߰� �߾����� ����
        var rootRT = transform as RectTransform;
        rootRT.pivot = new Vector2(0.5f, 1f);
        rootRT.anchorMin = new Vector2(0f, 1f);
        rootRT.anchorMax = new Vector2(1f, 1f);
        // �¿� �е���ŭ �μ�
        rootRT.offsetMin = new Vector2(sidePadding, rootRT.offsetMin.y);
        rootRT.offsetMax = new Vector2(-sidePadding, rootRT.offsetMax.y);
    }
}
