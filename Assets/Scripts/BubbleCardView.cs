using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class BubbleCardView : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private float maxHeight = 520f;   // 카드 버블 최대 세로
    [SerializeField] private float sidePadding = 16f;  // content 좌우 패딩과 맞추기

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

        // 1) content 가로폭 기준으로 최대폭 계산
        var parentRT = transform.parent as RectTransform;
        float parentWidth = parentRT ? parentRT.rect.width : 0f;
        // 레이아웃 패딩 감안 (좌우 여백)
        float maxWidth = Mathf.Max(0f, parentWidth - sidePadding * 2f);

        // 2) 스프라이트 원본 비율
        float w = sprite.rect.width;
        float h = sprite.rect.height;
        float aspect = w / Mathf.Max(1f, h);

        // 3) 세로 우선 제한 → 그 다음 가로 제한
        float targetH = Mathf.Min(maxHeight, h);
        float targetW = targetH * aspect;

        if (maxWidth > 0f && targetW > maxWidth)
        {
            targetW = maxWidth;
            targetH = targetW / Mathf.Max(0.0001f, aspect);
        }

        // 4) Image Rect 적용
        var rt = cardImage.rectTransform;
        rt.sizeDelta = new Vector2(targetW, targetH);
        rt.anchoredPosition = new Vector2(0, 0); // 레이아웃이 위치를 책임지도록 0으로

        // 5) LayoutElement에 높이 전달 (세로 레이아웃 안정)
        var le = cardImage.GetComponent<LayoutElement>();
        if (!le) le = cardImage.gameObject.AddComponent<LayoutElement>();
        le.preferredHeight = targetH;
        le.preferredWidth = targetW;

        // 6) 버블 루트에 여백 맞추고 중앙정렬 느낌
        var rootRT = transform as RectTransform;
        rootRT.pivot = new Vector2(0.5f, 1f);
        rootRT.anchorMin = new Vector2(0f, 1f);
        rootRT.anchorMax = new Vector2(1f, 1f);
        // 좌우 패딩만큼 인셋
        rootRT.offsetMin = new Vector2(sidePadding, rootRT.offsetMin.y);
        rootRT.offsetMax = new Vector2(-sidePadding, rootRT.offsetMax.y);
    }
}
