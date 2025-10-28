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
    [SerializeField] float picturePreferredHeight = 320f; // �� �� ���� ����!

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

        // ǥ��/���� ����
        bool has = sprite != null;
        picture.enabled = has;

        // ��������Ʈ ��ü + Ʃ��
        picture.sprite = sprite;
        picture.color = Color.white;           // Ȥ�� ���� ����/ȸ���� �� ���
        picture.preserveAspect = true;         // ���� ����
        picture.raycastTarget = false;         // Ŭ�� ���� �ʰ�(����)

        // ���̾ƿ� ����(������)
        var le = picture.GetComponent<LayoutElement>();
        if (le != null)
        {
            // picturePreferredHeight ���� ���� ������ �־ ��
            // le.preferredHeight = has ? picturePreferredHeight : 0f;
            le.flexibleHeight = 0f;
            le.flexibleWidth = 0f;
        }

        Rebuild(); // ���̾ƿ� ����
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
            le.preferredHeight = picturePreferredHeight; // �� �ߺ� ���� ���� ���
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
