using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubbleView : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] TMP_Text body;
    [SerializeField] GameObject header;

    [Header("Optional")]
    [SerializeField] Button extraButton;
    [SerializeField] Image picture;

    [Header("Picture Layout")]
    [SerializeField] float picturePreferredHeight = 320f; // ← 한 번만 선언!

    void Awake()
    {
        if (picture)
        {
            picture.preserveAspect = true;
            var le = picture.GetComponent<LayoutElement>() ?? picture.gameObject.AddComponent<LayoutElement>();
            le.preferredHeight = picturePreferredHeight;
            le.flexibleHeight = 0f;
            le.flexibleWidth = 0f;
        }
    }

    public void SetText(string t)
    {
        if (header) header.SetActive(false);
        if (extraButton) extraButton.gameObject.SetActive(false);

        if (body)
        {
            body.enableWordWrapping = true;
            body.richText = true;
            body.text = t ?? string.Empty;
        }
        if (picture) picture.enabled = false;
        Rebuild();
    }

    public void SetPicture(Sprite sprite)
    {
        if (!picture) return;

        // 표시/숨김 먼저
        bool has = sprite != null;
        picture.enabled = has;

        // 스프라이트 교체 + 튜닝
        picture.sprite = sprite;
        picture.color = Color.white;           // 혹시 색이 투명/회색일 때 대비
        picture.preserveAspect = true;         // 비율 유지
        picture.raycastTarget = false;         // 클릭 먹지 않게(선택)

        // 레이아웃 보정(있으면)
        var le = picture.GetComponent<LayoutElement>();
        if (le != null)
        {
            // picturePreferredHeight 같은 값이 있으면 넣어도 됨
            // le.preferredHeight = has ? picturePreferredHeight : 0f;
            le.flexibleHeight = 0f;
            le.flexibleWidth = 0f;
        }

        Rebuild(); // 레이아웃 갱신
    }



    public void SetImageWithText(string label, Sprite sprite)
    {
        if (header) header.SetActive(false);
        if (extraButton) extraButton.gameObject.SetActive(false);

        if (body)
        {
            body.enableWordWrapping = true;
            body.richText = true;
            body.text = label ?? string.Empty;
        }

        if (picture)
        {
            picture.sprite = sprite;
            picture.preserveAspect = true;
            picture.enabled = sprite != null;

            var le = picture.GetComponent<LayoutElement>() ?? picture.gameObject.AddComponent<LayoutElement>();
            le.preferredHeight = picturePreferredHeight; // ← 중복 선언 없이 사용
            le.flexibleHeight = 0;
            le.minHeight = 0;
        }

        Rebuild();
    }

    void Rebuild()
    {
        var rt = transform as RectTransform;
        if (body) LayoutRebuilder.ForceRebuildLayoutImmediate(body.rectTransform);
        if (rt) LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
        if (rt && rt.parent) LayoutRebuilder.ForceRebuildLayoutImmediate(rt.parent as RectTransform);
    }
    

    public void HideButton()
    {
        if (extraButton) extraButton.gameObject.SetActive(false);
    }

    public void SetButton(string label, System.Action onClick)
    {
        if (!extraButton) return;
        extraButton.gameObject.SetActive(true);

        var tmp = extraButton.GetComponentInChildren<TMP_Text>();
        if (tmp)
        {
            tmp.enableAutoSizing = false;
            tmp.overflowMode = TextOverflowModes.Ellipsis;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.text = label ?? "OK";
        }

        var le = extraButton.GetComponent<LayoutElement>() ?? extraButton.gameObject.AddComponent<LayoutElement>();
        le.preferredWidth = 160f;
        le.preferredHeight = 48f;
        le.flexibleWidth = le.flexibleHeight = 0f;

        extraButton.onClick.RemoveAllListeners();
        extraButton.onClick.AddListener(() => onClick?.Invoke());
    }

}
