// CardPickerController.cs (����)
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardPickerController : MonoBehaviour
{
    public static CardPickerController Instance { get; private set; }
    const string SceneName = "pick_card";   // �� �� �̸� �� ������ ����

    [Header("Slots (�ڵ� ���� ����)")]
    [SerializeField] List<CardSelectable> slots = new();

    [Header("UI Buttons (����)")]
    [SerializeField] Button confirmButton;
    [SerializeField] Button cancelButton;

    [Header("Sprites")]
    [SerializeField] Sprite backSprite;
    [SerializeField] List<Sprite> deckSprites;   // 78
    [SerializeField] List<string> deckNames;     // 78

    readonly List<int> selectedIdx = new();
    readonly List<int> deckPickedIdx = new();

    void Awake()
    {
        Instance = this;

        if (slots == null || slots.Count == 0)
            slots = new List<CardSelectable>(FindObjectsOfType<CardSelectable>(true));

        for (int i = 0; i < slots.Count; i++)
        {
            var s = slots[i];
            if (!s) continue;
            s.index = i;
            s.SetBack(backSprite);          // ���⼭ ���� ũ�� ĳ��(�Ʒ� CardSelectable ����)
            s.SetSelected(false, instant: true);
        }

        if (confirmButton) confirmButton.onClick.AddListener(OnConfirm);
        if (cancelButton) cancelButton.onClick.AddListener(OnCancel);
        UpdateConfirm();
    }

    public void ToggleSlot(int idx)
    {
        if (idx < 0 || idx >= slots.Count) return;
        var s = slots[idx];
        if (!s) return;

        if (s.IsSelected)
        {
            s.SetSelected(false);
            selectedIdx.Remove(idx);
        }
        else
        {
            if (selectedIdx.Count >= 3) return;
            s.SetSelected(true);
            selectedIdx.Add(idx);
        }
        UpdateConfirm();
    }

    void UpdateConfirm()
    {
        if (confirmButton) confirmButton.interactable = (selectedIdx.Count == 3);
    }

    public void OnConfirm()
    {
        if (selectedIdx.Count != 3) return;
        if (deckSprites.Count != 78 || deckNames.Count != 78)
        {
            Debug.LogError("Deck must be exactly 78 sprites & 78 names.");
            return;
        }

        // ���ߺ� 3��
        deckPickedIdx.Clear();
        while (deckPickedIdx.Count < 3)
        {
            int r = Random.Range(0, deckSprites.Count);
            if (!deckPickedIdx.Contains(r)) deckPickedIdx.Add(r);
        }

        var pickedSprites = new List<Sprite>(3);
        var pickedNames = new List<string>(3);

        for (int i = 0; i < 3; i++)
        {
            int slotIdx = selectedIdx[i];
            int deckIdx = deckPickedIdx[i];
            var s = slots[slotIdx];

            // �� �ո� ��ü (ũ��� CardSelectable�� ������)
            s.SetFront(deckSprites[deckIdx]);

            pickedSprites.Add(deckSprites[deckIdx]);
            pickedNames.Add(deckNames[deckIdx]);
        }

        StartCoroutine(CloseAfterShow(pickedSprites, pickedNames, 0.8f)); // ��� �����ֱ�
    }

    System.Collections.IEnumerator CloseAfterShow(List<Sprite> sprites, List<string> names, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CardPickerBridge.OnPicked?.Invoke(sprites, names);
        SceneManager.UnloadSceneAsync(SceneName);   // �� "pick_card" �� ����
    }

    public void OnCancel()
    {
        CardPickerBridge.OnPicked?.Invoke(new List<Sprite>(), new List<string>());
        SceneManager.UnloadSceneAsync(SceneName);   // �� ����
    }
}
