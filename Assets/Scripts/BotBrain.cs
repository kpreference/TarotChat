using System;
using System.Collections.Generic;
using System.Linq;

public enum FlowState { Idle, CollectingInfo, ReadyToPick, Picking, Interpreting, Done }
public enum BotAction { None, OpenCardPicker, ShowResult, ReturnToChat }

public enum FortuneCategory { Unknown, Love, Money, Career, Health, General }

[Serializable]
public class PickedCard
{
    public string cardId;     // "THE_SUN" 같은 식별자(또는 ScriptableObject 이름)
    public bool reversed;
    public int position;      // 0/1/2 : 상황/조언/결과 등
}

[Serializable]
public class BotSession
{
    public FlowState state = FlowState.Idle;

    // 공통
    public string userRawFirstQuestion = "";
    public FortuneCategory category = FortuneCategory.Unknown;

    // 카테고리별 추가정보
    public string relationship = "";     // Love 전용: solo/couple/situationship
    public string jobType = "";          // Career 전용: 직군/취준/이직 여부 등
    public string moneyFocus = "";       // Money 전용: 저축/투자/지출 등
    public string healthFocus = "";      // Health 전용: 컨디션/수면/습관 등

    // 카드 선택 결과
    public List<PickedCard> picks = new List<PickedCard>();
}

public struct BotReply
{
    public List<string> messages;   // AI 버블
    public BotAction action;        // UI 요청(카드픽 열기 등)
}

public class BotBrain
{
    public readonly BotSession session = new();

    // --- 진입점: 유저가 메시지 입력할 때 ---
    public BotReply OnUserMessage(string raw)
    {
        var textNorm = Normalize(raw);
        // 전역 핫키: 어떤 상태에서든 "다시" 입력 시 즉시 초기화
        if (textNorm == "다시")
        {
            Reset();
            // Reset했으니 state = Idle, category = Unknown
            // Idle 상태 진입 멘트 강제로 반환
            return Reply(new[] {
                "보고 싶은 운세를 알려줘! (연애/금전/직업/건강/종합)",
                "예) \"연애운 어때?\", \"이번달 금전운 알려줘\""
            });
        }

        // 0) 처음 질문 → 카테고리 파악
        if (session.state == FlowState.Idle)
        {
            session.userRawFirstQuestion = raw.Trim();
            session.category = DetectCategory(textNorm);

            if (session.category == FortuneCategory.Unknown)
                return Reply(new[]{
                    "보고 싶은 운세를 알려줘! (연애/금전/건강/직업/종합 중에서 선택해줘)",/**/
                    "예) \"연애운 어때?\", \"이번달 금전운 알려줘\""
                });

            session.state = FlowState.CollectingInfo;
            return AskCategorySpecificInfo(session.category);
        }

        // 1) 추가정보 수집 단계
        if (session.state == FlowState.CollectingInfo)
        {
            ConsumeCategorySpecificInfo(session.category, textNorm, raw);
            if (IsInfoEnough(session.category))
            {
                session.state = FlowState.ReadyToPick;
                return Reply(new[]{
                    "좋아! 이제 카드를 3장 뽑아보자. \"카드\"라고 입력해줘."
                });
            }
            else
            {
                return AskCategorySpecificInfo(session.category); // 아직 부족하면 반복 질문
            }
        }

        // 2) 카드 픽 트리거
        if (session.state == FlowState.ReadyToPick)
        {
            if (textNorm.Contains("카드") || textNorm.Contains("뽑"))
            {
                session.state = FlowState.Picking;
                return new BotReply
                {
                    messages = new List<string> { "카드 선택 화면을 열게! 바로 밑에 있는 버튼을 눌러줘." },
                    action = BotAction.OpenCardPicker
                };
            }

            return Reply(new[] { "카드를 뽑으려면 \"카드\"라고 입력해줘!" });
        }

        // 3) 카드 선택 중엔 채팅으로 재촉
        if (session.state == FlowState.Picking)
        {
            return Reply(new[] { "먼저 카드 3장을 골라줘! (선택 화면에서)" });
        }

        // 4) 결과 후
        if (session.state == FlowState.Done)
        {

            return Reply(new[] { "더 궁금한 게 있으면 물어봐! 또는 \"다시\"라고 입력해도 돼." });
        }

        return Reply(new[] { "흐름을 파악 중이야… \"카드\"라고 입력해볼래?" });
    }

    // --- UI(카드 씬) → 채팅으로 복귀 시 호출 ---
    public BotReply OnCardsPicked(List<PickedCard> picks)
    {
        session.picks = picks ?? new List<PickedCard>();
        session.state = FlowState.Interpreting;

        var lines = Interpret(session);   // 해석 생성
        session.state = FlowState.Done;

        return new BotReply
        {
            messages = lines,
            action = BotAction.ShowResult
        };
    }

    // ========= 내부 로직 =========

    static string Normalize(string s) => (s ?? "").Trim().ToLowerInvariant();

    FortuneCategory DetectCategory(string norm)
    {
        // 날짜/월 같은 단어가 있어도 카테고리 우선
        if (MatchesAny(norm, new[] { "연애", "사랑", "애정", "연인" })) return FortuneCategory.Love;
        if (MatchesAny(norm, new[] { "금전", "재물", "돈", "자금", "투자", "수익" })) return FortuneCategory.Money;
        if (MatchesAny(norm, new[] { "직업", "커리어", "이직", "취업", "진로", "승진", "업무" })) return FortuneCategory.Career;
        if (MatchesAny(norm, new[] { "건강", "컨디션", "체력", "수면", "운동" })) return FortuneCategory.Health;
        if (MatchesAny(norm, new[] { "종합", "전체", "전반", "총운", "운세" })) return FortuneCategory.General;
        return FortuneCategory.Unknown;
    }

    static bool MatchesAny(string norm, IEnumerable<string> keys) =>
        keys.Any(k => norm.Contains(k));

    BotReply AskCategorySpecificInfo(FortuneCategory cat)
    {
        switch (cat)
        {
            case FortuneCategory.Love:
                if (string.IsNullOrEmpty(session.relationship))
                    return Reply(new[] { "연애운을 볼게! 현재 상태는? (솔로/커플/썸)" });
                return Reply(new[] { "상대와의 최근 분위기를 한 줄로 알려줄래? (예: 대화 줄었음/자주 만남 등)" });

            case FortuneCategory.Money:
                if (string.IsNullOrEmpty(session.moneyFocus))
                    return Reply(new[] { "금전운은 어떤 포인트가 궁금해? (저축/지출/투자 중 택1 또는 자유 입력)" });
                return Reply(new[] { "이번 달 목표가 있으면 말해줘! (예: 지출 30% 절약, 투자소액 등)" });

            case FortuneCategory.Career:
                if (string.IsNullOrEmpty(session.jobType))
                    return Reply(new[] { "직업운: 지금 상황은? (재직/이직준비/취준/프리랜서 등)" });
                return Reply(new[] { "가장 고민되는 포인트를 한 줄로 말해줘. (예: 협업/상사/성과/면접 등)" });

            case FortuneCategory.Health:
                if (string.IsNullOrEmpty(session.healthFocus))
                    return Reply(new[] { "건강운: 요즘 가장 신경 쓰는 건? (수면/식습관/운동/스트레스 등)" });
                return Reply(new[] { "생활 패턴을 한 줄로 말해줘. (예: 야근 잦음/주3회 운동 등)" });

            case FortuneCategory.General:
                return Reply(new[] { "종합운을 볼게! 특별히 신경 쓰는 영역이 있으면 한 줄로 알려줘. (없으면 바로 카드 뽑자고 입력해도 돼!)" });
        }
        return Reply(new[] { "보고 싶은 운세를 알려줘! (연애/금전/직업/건강/종합)" });
    }

    void ConsumeCategorySpecificInfo(FortuneCategory cat, string norm, string raw)
    {
        switch (cat)
        {
            case FortuneCategory.Love:
                if (string.IsNullOrEmpty(session.relationship))
                    session.relationship = ExtractRelationship(norm);
                // 그 외 자유 서술은 무시/보관 선택 가능
                break;

            case FortuneCategory.Money:
                if (string.IsNullOrEmpty(session.moneyFocus))
                    session.moneyFocus = ExtractMoneyFocus(norm, raw);
                break;

            case FortuneCategory.Career:
                if (string.IsNullOrEmpty(session.jobType))
                    session.jobType = ExtractJobType(norm, raw);
                break;

            case FortuneCategory.Health:
                if (string.IsNullOrEmpty(session.healthFocus))
                    session.healthFocus = ExtractHealthFocus(norm, raw);
                break;

            case FortuneCategory.General:
                // 자유 메모에 활용 가능 (여기서는 넘어감)
                break;
        }
    }

    bool IsInfoEnough(FortuneCategory cat)
    {
        return cat switch
        {
            FortuneCategory.Love => !string.IsNullOrEmpty(session.relationship),
            FortuneCategory.Money => !string.IsNullOrEmpty(session.moneyFocus),
            FortuneCategory.Career => !string.IsNullOrEmpty(session.jobType),
            FortuneCategory.Health => !string.IsNullOrEmpty(session.healthFocus),
            FortuneCategory.General => true,
            _ => false
        };
    }

    string ExtractRelationship(string norm)
    {
        if (MatchesAny(norm, new[] { "솔로", "싱글", "혼자", "없음" })) return "solo";
        if (MatchesAny(norm, new[] { "커플", "연인", "사귀", "여친", "남친" })) return "couple";
        if (MatchesAny(norm, new[] { "썸", "애매", "관심", "호감" })) return "situationship";
        return "";
    }
    string ExtractMoneyFocus(string norm, string raw)
    {
        if (MatchesAny(norm, new[] { "저축", "절약", "세이브" })) return "saving";
        if (MatchesAny(norm, new[] { "투자", "주식", "코인", "펀드" })) return "invest";
        if (MatchesAny(norm, new[] { "지출", "소비", "결제" })) return "spend";
        return raw; // 자유 입력 보관
    }
    string ExtractJobType(string norm, string raw)
    {
        if (MatchesAny(norm, new[] { "재직", "회사", "근무" })) return "employed";
        if (MatchesAny(norm, new[] { "이직", "퇴사" })) return "changing";
        if (MatchesAny(norm, new[] { "취준", "면접", "신입" })) return "seeking";
        if (MatchesAny(norm, new[] { "프리랜서", "자영업" })) return "freelance";
        return raw;
    }
    string ExtractHealthFocus(string norm, string raw)
    {
        if (MatchesAny(norm, new[] { "수면", "잠" })) return "sleep";
        if (MatchesAny(norm, new[] { "운동", "체력" })) return "workout";
        if (MatchesAny(norm, new[] { "식단", "식습관", "영양" })) return "diet";
        if (MatchesAny(norm, new[] { "스트레스", "번아웃" })) return "stress";
        return raw;
    }

    // --- 해석(간단 더미: 카드 내용 없이도 카테고리/추가정보 반영) ---
    List<string> Interpret(BotSession s)
    {
        var lines = new List<string> { "카드를 확인했어!" };

        switch (s.category)
        {
            case FortuneCategory.Love:
                lines.Add(LoveSummary(s.relationship, s.picks));
                break;
            case FortuneCategory.Money:
                lines.Add(MoneySummary(s.moneyFocus, s.picks));
                break;
            case FortuneCategory.Career:
                lines.Add(CareerSummary(s.jobType, s.picks));
                break;
            case FortuneCategory.Health:
                lines.Add(HealthSummary(s.healthFocus, s.picks));
                break;
            case FortuneCategory.General:
                lines.Add(GeneralSummary(s.picks));
                break;
        }

        // (선택) 포지션별 요약
        if (s.picks != null && s.picks.Count > 0)
        {
            lines.Add("• 펼친 카드: " + string.Join(", ",
                s.picks.OrderBy(p => p.position).Select(p => $"{p.cardId}{(p.reversed ? "(역)" : "")}")));
        }
        lines.Add("👉 다른 운세도 보고 싶다면 '다시'라고 입력해줘!");
        return lines;
    }

    string LoveSummary(string rel, List<PickedCard> picks) =>
        rel switch
        {
            "solo" => "새로운 인연의 기류가 형성돼. 가벼운 만남/소개에 발걸음이 가벼울수록 기회가 열려!",
            "couple" => "약속/일정 맞추기가 관계 안정의 키. 솔직한 대화 타이밍이 좋아.",
            "situationship" => "확신을 주는 작은 행동이 필요해. 가벼운 제안 → 명확한 약속 순으로.",
            _ => "자기 매력을 가다듬을수록 인연운이 상승해."
        };

    string MoneySummary(string focus, List<PickedCard> picks)
    {
        if (focus == "saving") return "지출 루틴을 한 번만 정리해도 체감이 커. 고정비 점검이 즉효야.";
        if (focus == "invest") return "정보 과다보다 핵심 1~2개에 집중. 분할 접근이 리스크를 낮춰줘.";
        if (focus == "spend") return "충동 소비 트리거를 차단해봐. 장바구니 ‘하루 보류’가 유용해.";
        return "수입·지출 흐름을 한 눈에 보이게 만들면 금전운이 안정돼.";
    }

    string CareerSummary(string job, List<PickedCard> picks)
    {
        return job switch
        {
            "employed" => "협업 룰과 일정 조율을 선점하면 체감 성과가 커져. 작은 승리를 빠르게.",
            "changing" => "핵심 역량 키워드 3개를 전면에. 이직 스토리를 간결하게 정리해봐.",
            "seeking" => "면접 답변을 경험 기반으로 리프레이밍. 포트폴리오는 ‘결과→과정’ 순으로.",
            "freelance" => "레퍼런스 3종을 전면에. 재의뢰를 부르는 A/S 룰을 명문화.",
            _ => "지금 중요한 건 ‘보이는 성과’ 1개. 작은 완성을 빠르게."
        };
    }

    string HealthSummary(string focus, List<PickedCard> picks)
    {
        if (focus == "sleep") return "취침·기상 고정만으로도 컨디션이 올라가. 카페인은 오후 2시 이전으로.";
        if (focus == "workout") return "가볍게라도 주 3회 루틴이면 충분. 무리하지 말고 지속성을 우선.";
        if (focus == "diet") return "하루 1끼만 ‘깨끗하게’로 바꿔도 체감돼. 수분 섭취를 늘려봐.";
        if (focus == "stress") return "미세 루틴(산책 10분, 스트레칭 3분)이 스트레스 해소에 도움돼.";
        return "작은 루틴 한 개만 고정해도 건강운이 상승해.";
    }

    string GeneralSummary(List<PickedCard> picks) =>
        "이번 달은 ‘정리’가 키워드. 작은 정리 → 행동력이 붙으면서 흐름이 좋아져.";

    static BotReply Reply(IEnumerable<string> lines) => new BotReply
    {
        messages = lines.ToList(),
        action = BotAction.None
    };

    public void Reset()
    {
        session.state = FlowState.Idle;
        session.category = FortuneCategory.Unknown;
        session.relationship = session.jobType = session.moneyFocus = session.healthFocus = "";
        session.userRawFirstQuestion = "";
        session.picks.Clear();
    }
}
