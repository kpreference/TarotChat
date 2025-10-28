using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Text;

public enum ReadingDomain { General, Love, Career, Money, Health }

public class ChatUIController : MonoBehaviour
{
    [Header("Reading")]
    [SerializeField] ReadingDomain readingDomain = ReadingDomain.Love;

    [Header("Refs")]
    [SerializeField] ScrollRect chatScroll;
    [SerializeField] RectTransform content;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button sendButton;

    [Header("Prefabs")]
    [SerializeField] GameObject bubbleUserPrefab;
    [SerializeField] GameObject bubbleAIPrefab;

    [Header("Chat Visibility (optional)")]
    [Tooltip("채팅 루트에 CanvasGroup 연결하면 카드 씬 동안 페이드로 숨깁니다.")]
    [SerializeField] CanvasGroup chatCanvasGroup;

    [Header("Card Prefab")]
    [SerializeField] GameObject bubbleCardPrefab;   // ← Bubble_Card 프리팹 드래그

    const string CardSceneName = "pick_card";

    private BotBrain brain = new();
    private bool sending;

    void Awake()
    {
        // CanvasGroup 자동 연결/생성
        if (!chatCanvasGroup)
        {
            if (chatScroll) chatCanvasGroup = chatScroll.GetComponentInParent<CanvasGroup>();
            if (!chatCanvasGroup) chatCanvasGroup = GetComponentInParent<CanvasGroup>();
            if (!chatCanvasGroup)
            {
                var root = chatScroll ? chatScroll.transform.root.gameObject : gameObject;
                chatCanvasGroup = root.AddComponent<CanvasGroup>();
            }
        }
        chatCanvasGroup.alpha = 1f;
        chatCanvasGroup.interactable = true;
        chatCanvasGroup.blocksRaycasts = true;

        if (sendButton)
        {
            sendButton.onClick.AddListener(() => StartCoroutine(CoSendOnce(null)));
            var nav = sendButton.navigation;
            nav.mode = Navigation.Mode.None;
            sendButton.navigation = nav;
        }

        if (inputField)
        {
            inputField.lineType = TMP_InputField.LineType.MultiLineNewline;
            inputField.resetOnDeActivation = false;
        }
    }

    void OnDestroy()
    {
        if (sendButton) sendButton.onClick.RemoveAllListeners();
    }

    void Update()
    {
        if (inputField && inputField.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                if (!shift)
                {
                    var msg = inputField.text?.TrimEnd('\r', '\n');
                    StartCoroutine(CoSendOnce(msg));
                    inputField.ActivateInputField();
                    inputField.caretPosition = inputField.text.Length;
                }
            }
        }
    }

    IEnumerator CoSendOnce(string submitted)
    {
        if (sending) yield break;
        sending = true;
        yield return null;

        string msg = submitted;
        if (msg == null)
        {
            var raw = inputField.text;
            if (string.IsNullOrWhiteSpace(raw)) { sending = false; yield break; }
            msg = raw.TrimEnd('\r', '\n');
        }
        if (string.IsNullOrWhiteSpace(msg)) { sending = false; yield break; }

        AddBubble(bubbleUserPrefab, msg);

        if (inputField)
        {
            inputField.SetTextWithoutNotify(string.Empty);
            inputField.ActivateInputField();
            inputField.caretPosition = 0;
        }

        var reply = brain.OnUserMessage(msg);
        ShowAIReply(reply);

        if (EventSystem.current && inputField)
        {
            EventSystem.current.SetSelectedGameObject(inputField.gameObject);
            inputField.ActivateInputField();
            inputField.caretPosition = inputField.text.Length;
        }
        sending = false;
    }

    // ---------- 말풍선 유틸 ----------
    void AddBubble(GameObject prefab, string text)
    {
        var go = Instantiate(prefab, content);
        var view = go.GetComponent<BubbleView>();
        if (view) { view.SetText(text); view.HideButton(); }
        Rebuild(go);
    }

    void AddBubbleWithButton(GameObject prefab, string text, string btnLabel, System.Action onClick)
    {
        var go = Instantiate(prefab, content);
        var view = go.GetComponent<BubbleView>();
        if (!view) { Debug.LogError("BubbleView가 프리팹에 없음: " + prefab.name); return; }

        view.SetText(text);
        view.SetButton(btnLabel, onClick);
        Rebuild(go);
    }

    void AddBubbleCard(Sprite sprite)
    {
        if (!bubbleCardPrefab || !content) return;

        var go = Instantiate(bubbleCardPrefab, content);
        var view = go.GetComponent<BubbleCardView>();   // 프리팹에 붙어 있어야 함
        if (view) view.SetCard(sprite);

        Rebuild(go);
    }


    void Rebuild(GameObject go)
    {
        go.transform.SetAsLastSibling();
        LayoutRebuilder.ForceRebuildLayoutImmediate(go.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        StartCoroutine(ScrollBottom());
    }

    IEnumerator ScrollBottom()
    {
        yield return null;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        if (chatScroll) chatScroll.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    // ---------- AI 답장 ----------
    void ShowAIReply(BotReply reply)
    {
        var msgs = reply.messages?.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
        if (msgs == null || msgs.Count == 0) return;

        if (reply.action == BotAction.None)
        {
            AddBubble(bubbleAIPrefab, string.Join("\n", msgs));
            return;
        }

        if (reply.action == BotAction.OpenCardPicker)
        {
            for (int i = 0; i < msgs.Count - 1; i++)
                AddBubble(bubbleAIPrefab, msgs[i]);

            string tail = msgs[msgs.Count - 1]; // "카드 선택 화면을 열게!"
            AddBubbleWithButton(bubbleAIPrefab, tail, "카드 고르기", OnClickOpenCardPicker);
            return;
        }

        AddBubble(bubbleAIPrefab, string.Join("\n", msgs));
    }

    // ---------- 카드 픽커 ----------
    void OnClickOpenCardPicker()
    {
        if (!Application.CanStreamedLevelBeLoaded(CardSceneName))
        {
            Debug.LogError($"Scene '{CardSceneName}' not in Build Settings.");
            return;
        }
        CardPickerBridge.OnPicked = OnCardsPicked;
        StartCoroutine(FadeChat(false, 0.15f));
        SceneManager.LoadSceneAsync(CardSceneName, LoadSceneMode.Additive);
    }

    void OnCardsPicked(List<Sprite> sprites, List<string> names)
    {
        CardPickerBridge.OnPicked = null;

        if (sprites == null || sprites.Count == 0)
        {
            AddBubble(bubbleAIPrefab, "카드 선택을 취소했어.");
            StartCoroutine(FadeChat(true, 0.15f));
            return;
        }

        StartCoroutine(FadeChat(true, 0.15f));

        // 1/2/3: 이름 버블 → 이미지 버블 (한 쌍)
        for (int i = 0; i < sprites.Count; i++)
        {
            string pos = (i == 0) ? "1번 (보조)" : (i == 1) ? "2번 (메인)" : "3번 (보조)";
            string name = (names != null && i < names.Count && !string.IsNullOrEmpty(names[i])) ? names[i] : "Unknown";
            ShowCardPair($"{pos} — {name}", sprites[i]);
        }

        // 리딩(도메인은 인스펙터 선택값 사용)
        string domain = brain.session.category switch
        {
            FortuneCategory.Love => "love",
            FortuneCategory.Career => "career",
            FortuneCategory.Money => "money",
            FortuneCategory.Health => "health",
            _ => "general"
        };
        var reading = BuildCombinedReading(names, domain);
        AddBubble(bubbleAIPrefab, reading);


        AddBubble(bubbleAIPrefab, "다른 운세도 확인해보고 싶다면 '다시'라고 입력해줘!");
    }


    // ---------- 페이드 ----------
    IEnumerator FadeChat(bool show, float duration)
    {
        float from = chatCanvasGroup.alpha;
        float to = show ? 1f : 0f;

        chatCanvasGroup.interactable = show;
        chatCanvasGroup.blocksRaycasts = show;

        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            chatCanvasGroup.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        chatCanvasGroup.alpha = to;
    }

    // ---------- 리딩 생성 ----------
    static string BuildCombinedReading(IReadOnlyList<string> cardNames, string domain)
    {
        if (cardNames == null || cardNames.Count < 3) return "카드를 3장 모두 골라줘.";

        string leftName = cardNames[0];
        string mainName = cardNames[1];
        string rightName = cardNames[2];

        string left = GetCardMeaningShort(leftName, domain);
        string main = GetCardMeaningShort(mainName, domain);
        string right = GetCardMeaningShort(rightName, domain);

        string adviceLeft = TarotDB.TryGet(leftName, out var ml) && !string.IsNullOrWhiteSpace(ml.advice) ? ml.advice : null;
        string adviceMain = TarotDB.TryGet(mainName, out var mm) && !string.IsNullOrWhiteSpace(mm.advice) ? mm.advice : null;
        string adviceRight = TarotDB.TryGet(rightName, out var mr) && !string.IsNullOrWhiteSpace(mr.advice) ? mr.advice : null;

        var sb = new StringBuilder();
        sb.AppendLine($"[결과 리딩]");
        sb.AppendLine($"- 메인 카드 {mainName}: {main}");
        sb.AppendLine($"- 왼쪽 보조 {leftName}: {left}");
        sb.AppendLine($"- 오른쪽 보조 {rightName}: {right}");
        sb.AppendLine();

        sb.Append("■ 요약: ");
        sb.Append($"현재 흐름은 {mainName}의 기조가 중심이고, ");
        sb.Append($"왼쪽의 {leftName}(이전/배경·보완)이 이를 뒷받침하거나 방향을 잡아주며, ");
        sb.Append($"오른쪽의 {rightName}(전개/결론·보완)이 실제 행동/결정의 톤을 만들어요.");
        sb.AppendLine();

        // 4) BuildCombinedReading의 도메인별 안내문에 money/health 추가
        if (domain == "love")
            sb.AppendLine("→ 관계에서는 메인을 분위기/핵심으로, 보조 두 장을 감정의 강도·속도·보완점으로 이해하세요.");
        else if (domain == "career")
            sb.AppendLine("→ 일/커리어에서는 메인을 방향·전략으로, 보조 두 장을 리스크/자원/속도로 읽어보세요.");
        else if (domain == "money")
            sb.AppendLine("→ 금전에서는 메인을 자금 흐름·원칙으로, 보조 두 장을 수입/지출 혹은 투자·리스크로 해석하세요.");
        else if (domain == "health")
            sb.AppendLine("→ 건강에서는 메인을 핵심 루틴·주의점으로, 보조 두 장을 보완 습관·리커버리로 이해하세요.");
        else
            sb.AppendLine("→ 보조 카드는 메인의 의미를 강화·보완·조절합니다.");


        var adv = new List<string>();
        if (!string.IsNullOrWhiteSpace(adviceMain)) adv.Add(adviceMain);
        if (!string.IsNullOrWhiteSpace(adviceLeft)) adv.Add(adviceLeft);
        if (!string.IsNullOrWhiteSpace(adviceRight)) adv.Add(adviceRight);

        if (adv.Count > 0)
        {
            sb.AppendLine();
            sb.AppendLine("□ 조언:");
            foreach (var a in adv) sb.AppendLine($"- {a}");
        }
        return sb.ToString();
    }

    static string GetCardMeaningShort(string rawCardName, string domain)
    {
        if (!TarotDB.TryGet(rawCardName, out var m)) return rawCardName;

        bool reversed = TarotDB.IsReversed(rawCardName);
        string byDomain = domain switch
        {
            "love" => m.love,
            "career" => m.career,
            "money" => m.money,
            "health" => m.health,
            _ => null
        };

        string core = reversed
            ? (string.IsNullOrWhiteSpace(m.reversed) ? m.upright : m.reversed)
            : m.upright;

        string text = !string.IsNullOrWhiteSpace(byDomain) ? byDomain : core;
        if (reversed) text += " (역위 경향)";
        return text;
    }


    void ShowCardPair(string titleText, Sprite spriteOrNull)
    {
        AddBubble(bubbleAIPrefab, titleText);

        if (spriteOrNull != null)
            AddBubbleCard(spriteOrNull);
        else
            AddBubble(bubbleAIPrefab, "(이미지 없음)");
    }
}
