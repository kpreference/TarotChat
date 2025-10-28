using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TarotMeaning
{
    public string title;        // 표시용 이름
    public string upright;      // 기본 의미(짧게)
    public string reversed;     // 역위 의미(없으면 null/빈문자)
    public string love;         // 선택: 연애문맥(짧게)
    public string career;       // 선택: 커리어문맥(짧게)
    public string advice;       // 한줄 조언
    public string money;   // 금전 문맥(짧게)
    public string health;  // 건강 문맥(짧게)
}

public static class TarotDB
{
    // ★ 카드명 key는 NormalizeName() 이후 값과 맞추면 됨
    static readonly Dictionary<string, TarotMeaning> _dict = new()
    {
        // Major------------------------------------------------------------------------------
        ["The Fool"] = new TarotMeaning
        {
            title = "The Fool",
            upright = "새 출발, 순수한 도전, 자유, 믿음의 도약",
            reversed = "경솔, 준비 부족, 방향 상실, 현실 회피",
            love = "가벼운 설렘, 관계의 시작",
            career = "새 프로젝트/이직에 운; 계획은 간단히라도",
            advice = "두려움보다 호기심을 따르되 기본 안전장치는 챙겨라.",
            money = "작게 시작하되 리스크 한도 명확히.",
            health = "새 루틴을 가볍게 시작; 과욕은 금물."
        },

        // 1. The Magician
        ["The Magician"] = new TarotMeaning
        {
            title = "The Magician",
            upright = "의지와 기술, 실행력, 자원 활용, 매니페스트",
            reversed = "속임수, 의지 부족, 기술 미비, 말뿐",
            love = "주도적 표현이 관계를 앞당김",
            career = "제안·프레젠테이션에 길; 능력 어필",
            advice = "지금 가진 카드로 바로 실행해라.",
            money = "수입원 다각화/부가수입 시도에 길.",
            health = "기초체력보다 ‘방법’ 최적화; 폼·호흡 점검."
        },

        // 2. The High Priestess
        ["The High Priestess"] = new TarotMeaning
        {
            title = "The High Priestess",
            upright = "직관, 비밀, 잠재의식, 고요한 통찰",
            reversed = "혼란, 직감 무시, 비밀의 드러남",
            love = "속마음 읽기, 서두르지 말기",
            career = "자료·데이터 더 파기, 조용한 준비",
            advice = "시끄러운 정보보다 내면의 신호를 우선하라.",
            money = "충동 매수 금지; 정보/데이터 검증.",
            health = "휴식·수면 우선. 몸의 미세 신호 체크."
        },

        // 3. The Empress
        ["The Empress"] = new TarotMeaning
        {
            title = "The Empress",
            upright = "풍요, 창조성, 보살핌, 성장",
            reversed = "과보호, 정체, 의존성, 낭비",
            love = "따뜻한 돌봄과 애정의 풍성함",
            career = "브랜딩/창작/고객경험에 유리",
            advice = "키우고 보살피면 결실은 따른다.",
            money = "현금흐름 호전·생활의 질에 지출 증가.",
            health = "영양·컨디션 관리로 회복력 상승."
        },

        // 4. The Emperor
        ["The Emperor"] = new TarotMeaning
        {
            title = "The Emperor",
            upright = "질서, 구조, 리더십, 책임",
            reversed = "권위주의, 고집, 구조의 경직",
            love = "경계·약속 명확화가 관계 안정",
            career = "조직/규정/리더십으로 성과",
            advice = "원칙을 세우고 그 원칙으로 결정하라.",
            money = "예산·규칙 수립, 장기적 안정 중시.",
            health = "루틴 고정·규칙적인 생활로 체력 회복."
        },

        // 5. The Hierophant
        ["The Hierophant"] = new TarotMeaning
        {
            title = "The Hierophant",
            upright = "전통, 교육, 멘토, 규범",
            reversed = "파격, 비전통, 체제와의 갈등",
            love = "공식화·약속·가치 일치 점검",
            career = "자격·교육·프로세스 준수에 이점",
            advice = "검증된 길을 먼저 익히고 변주하라.",
            money = "보수적 운용, 장기 적립·보험 유리.",
            health = "의사의 지침/가이드라인 준수."
        },

        // 6. The Lovers
        ["The Lovers"] = new TarotMeaning
        {
            title = "The Lovers",
            upright = "사랑, 가치 일치, 중요한 선택",
            reversed = "불일치, 유혹, 우유부단, 삼각",
            love = "상호 선택과 신뢰가 핵심",
            career = "파트너십/팀업의 시너지",
            advice = "가치가 맞는 쪽을 과감히 택하라.",
            money = "공동재정/동업 의사결정의 타이밍.",
            health = "생활습관을 ‘함께’ 바꾸면 지속성↑."
        },

        // 7. The Chariot
        ["The Chariot"] = new TarotMeaning
        {
            title = "The Chariot",
            upright = "의지로 전진, 통제, 승리, 이동",
            reversed = "폭주, 통제 상실, 동력이 분산",
            love = "관계의 주도·속도 조절",
            career = "데드라인 돌파·런칭·출시 운",
            advice = "흔들리는 변수들을 한 손에 쥐고 밀어붙여라.",
            money = "목표액 향해 드라이브; 과속 소비 주의.",
            health = "체력 상승기; 부상 방지에 유의."
        },

        // 8. Strength
        ["Strength"] = new TarotMeaning
        {
            title = "Strength",
            upright = "용기, 인내, 내적 힘, 부드러운 리더십",
            reversed = "자신감 저하, 조급함, 감정 기복",
            love = "진정성과 배려로 관계를 단단히",
            career = "끈기 있는 추진, 설득의 힘",
            advice = "서두르지 말고 꾸준히 밀고 가자.",
            money = "지속적 저축/상환으로 체감 성과.",
            health = "꾸준한 운동·재활, 페이스 유지."
        },

        // 9. The Hermit
        ["The Hermit"] = new TarotMeaning
        {
            title = "The Hermit",
            upright = "고독한 탐구, 성찰, 지혜, 멘토링",
            reversed = "고립, 답답함, 타인 단절",
            love = "잠시 각자의 성찰 시간",
            career = "리서치·기획·정리의 시기",
            advice = "소음을 끄고 스스로의 해답을 찾자.",
            money = "지출 끊고 점검; 불필요 구독 정리.",
            health = "휴식/명상/수면으로 회복 모드."
        },

        // 10. Wheel of Fortune
        ["Wheel of Fortune"] = new TarotMeaning
        {
            title = "Wheel of Fortune",
            upright = "전환점, 운의 흐름, 사이클, 기회",
            reversed = "타이밍 미스, 반복되는 패턴",
            love = "관계 국면 전환, 타이밍",
            career = "시장/운의 바람 활용",
            advice = "오는 흐름을 읽고 탄력적으로 올라타라.",
            money = "변동성 기간; 분산·리밸런싱.",
            health = "컨디션 등락; 리듬을 잡아 안정화."
        },

        // 11. Justice
        ["Justice"] = new TarotMeaning
        {
            title = "Justice",
            upright = "공정, 진실, 균형, 법/계약",
            reversed = "불공정, 왜곡, 편향, 책임 회피",
            love = "솔직한 대화와 균형",
            career = "계약·협상·정책 준수",
            advice = "사실과 원칙에 근거해 판단하라.",
            money = "세무/계약 준수, 비용-편익 균형.",
            health = "과불급 피하고 균형 잡힌 생활."
        },

        // 12. The Hanged Man
        ["The Hanged Man"] = new TarotMeaning
        {
            title = "The Hanged Man",
            upright = "관점 전환, 일시 정지, 내려놓음, 헌신",
            reversed = "정체의 좌절, 변화 회피",
            love = "잠시 멈추고 시야 바꾸기",
            career = "우선순위 재정렬, 손실 최소화",
            advice = "멈춤은 후퇴가 아니라 재배치다.",
            money = "지출 보류·재검토; 손절/홀딩 판단.",
            health = "무리 중단, 스트레칭/회복에 집중."
        },

        // 13. Death
        ["Death"] = new TarotMeaning
        {
            title = "Death",
            upright = "종결과 재탄생, 큰 변화, 정리",
            reversed = "미련, 변화 지연, 끊지 못함",
            love = "이전 국면을 마감하고 새 질서로",
            career = "낡은 것 정리·피벗",
            advice = "끝내야 시작된다. 과감히 정리하라.",
            money = "불필요 지출/부채 정리로 재시작.",
            health = "습관 교체의 강한 타이밍."
        },

        // 14. Temperance
        ["Temperance"] = new TarotMeaning
        {
            title = "Temperance",
            upright = "조화, 절제, 중용, 융합",
            reversed = "불균형, 과유불급, 조율 실패",
            love = "속도와 온도 조절",
            career = "팀 간 조율·통합 프로젝트에 길",
            advice = "적당함과 타이밍을 지켜 균형을 맞춰라.",
            money = "수입/지출 균형; 과소비 조절.",
            health = "식이·수면·운동의 밸런스 맞추기."
        },

        // 15. The Devil
        ["The Devil"] = new TarotMeaning
        {
            title = "The Devil",
            upright = "집착, 유혹, 중독, 계약의 올가미",
            reversed = "해방의 시도, 집착에서 벗어남",
            love = "관계의 집착/의존 점검",
            career = "이익 뒤의 리스크·조건 확인",
            advice = "매력적인 제안일수록 족쇄를 점검하라.",
            money = "빚/과소비/고금리 함정 주의.",
            health = "중독성 습관 끊기; 해로운 유혹 거리두기."
        },

        // 16. The Tower
        ["The Tower"] = new TarotMeaning
        {
            title = "The Tower",
            upright = "갑작스런 붕괴, 진실 폭로, 리셋",
            reversed = "피해 최소화, 경고, 작은 균열",
            love = "허물어져야 할 틀의 붕괴",
            career = "구조조정·긴급 이슈·리스크",
            advice = "무너지는 곳을 붙잡지 말고 재건을 준비하라.",
            money = "예상치 못한 지출/손실 대비(비상자금).",
            health = "급성 컨디션 하락 경고; 즉시 케어."
        },

        // 17. The Star
        ["The Star"] = new TarotMeaning
        {
            title = "The Star",
            upright = "희망, 치유, 영감, 진정성",
            reversed = "의심, 회복 지연, 자신감 저하",
            love = "솔직함과 배려로 회복",
            career = "브랜딩/크리에이티브/공유에 길",
            advice = "희망을 잃지 말고 한 걸음씩 회복하라.",
            money = "장기목표 재설정·저축의 재개.",
            health = "회복·재활의 호조; 꾸준함이 핵심."
        },

        // 18. The Moon
        ["The Moon"] = new TarotMeaning
        {
            title = "The Moon",
            upright = "불안, 착각, 숨은 것, 감정의 파도",
            reversed = "혼미 해소, 진실 접근",
            love = "오해를 사실 검증으로 풀기",
            career = "정보 불투명, 결정 보류·검증",
            advice = "감정과 사실을 분리해 확인하라.",
            money = "루머·충동 거래 주의; 확인 후 행동.",
            health = "수면/정서 기복 관리; 기록으로 점검."
        },

        // 19. The Sun
        ["The Sun"] = new TarotMeaning
        {
            title = "The Sun",
            upright = "성공, 명료함, 기쁨, 활력",
            reversed = "과신, 피로, 주목 과다",
            love = "밝고 건강한 관계의 성장",
            career = "성과 공개, 홍보/런칭 호재",
            advice = "단순·명확·당당하게 빛을 비춰라.",
            money = "수입 호전·성과 보너스 기대.",
            health = "활력 상승; 과로/탈수만 주의."
        },

        // 20. Judgement
        ["Judgement"] = new TarotMeaning
        {
            title = "Judgement",
            upright = "각성, 부름, 평가, 재도약",
            reversed = "자책, 미결, 결단 회피",
            love = "과거 정리·용서·새 기준",
            career = "리뷰/채점/재평가의 시기",
            advice = "깨달음을 행동으로 전환하라.",
            money = "재무 재평가/리밸런싱; 불필요 정리.",
            health = "정기검진/수치 점검으로 방향 수정."
        },

        // 21. The World
        ["The World"] = new TarotMeaning
        {
            title = "The World",
            upright = "완성, 성취, 통합, 다음 단계로",
            reversed = "미완, 마무리 지연, 루프",
            love = "관계의 성숙/다음 챕터",
            career = "프로젝트 완주, 글로벌/확장",
            advice = "마침표를 찍고 새 여정을 설계하라.",
            money = "장기 목표 달성·자산 구조 완성 단계.",
            health = "목표 달성기; 유지/관리 플랜 수립."
        },



        // Minor Arcana - Cups---------------------------------------------------------------------------------------

        ["Ace of Cups"] = new TarotMeaning
        {
            title = "Ace of Cups",
            upright = "새로운 사랑, 감정의 시작, 영감, 풍요로운 감정",
            reversed = "억눌린 감정, 기회 상실, 메마름",
            love = "신규 연애·고백·따뜻한 시작",
            career = "창의적 아이디어, 팀워크 향상",
            advice = "마음을 열고 감정을 표현해라.",
            money = "작은 기회·보너스 유입, 시작 자금 운.",
            health = "컨디션 회복 시작, 활력이 차오름."
        },

        ["Two of Cups"] = new TarotMeaning
        {
            title = "Two of Cups",
            upright = "조화로운 관계, 파트너십, 교감",
            reversed = "불균형한 관계, 의존, 불화",
            love = "상호 교감·약속·짝의 카드",
            career = "파트너십·합작 프로젝트 성사",
            advice = "상대와 대등하게 맞추고 조율하라.",
            money = "공동 투자·재정 협력에 길.",
            health = "협력 운동, 커플/팀으로 하는 건강 관리."
        },

        ["Three of Cups"] = new TarotMeaning
        {
            title = "Three of Cups",
            upright = "축하, 우정, 모임, 협력",
            reversed = "과도한 유흥, 삼각관계, 불화",
            love = "친구 이상의 관계·즐거운 교제",
            career = "협업·네트워킹 성공",
            advice = "함께 기뻐하고 성과를 나눠라.",
            money = "경조사·모임 지출, 즐거운 소비.",
            health = "사회적 활동으로 활력이 오름."
        },

        ["Four of Cups"] = new TarotMeaning
        {
            title = "Four of Cups",
            upright = "권태, 무관심, 내적 탐색",
            reversed = "기회 재인식, 무기력 극복",
            love = "지루함·흥미 상실, 하지만 재점화 기회 있음",
            career = "동기 부족, 하지만 새로운 제안 주의",
            advice = "닫힌 마음을 열고 주변 기회를 보라.",
            money = "기회 보류·투자 제안 무심, 다시 점검 필요.",
            health = "무기력·권태감, 휴식과 자극 필요."
        },

        ["Five of Cups"] = new TarotMeaning
        {
            title = "Five of Cups",
            upright = "상실, 실망, 후회, 부정적 감정",
            reversed = "회복, 수용, 다시 일어섬",
            love = "이별/후회, 그러나 새 희망도 가까이 있음",
            career = "프로젝트 손실, 실패 후 학습",
            advice = "잃은 것보다 남은 것을 보라.",
            money = "투자 손실·지출 후 아쉬움.",
            health = "건강 저하·실망감, 회복 의지 필요."
        },

        ["Six of Cups"] = new TarotMeaning
        {
            title = "Six of Cups",
            upright = "추억, 향수, 과거의 재회, 순수함",
            reversed = "과거 집착, 미성숙, 성장 지연",
            love = "옛 인연 재등장, 따뜻한 교감",
            career = "과거 경험·인맥이 도움",
            advice = "순수한 마음을 기억하되 현재에 적용하라.",
            money = "과거 투자·유산·지원금 운.",
            health = "옛 습관으로 회귀, 생활패턴 점검."
        },

        ["Seven of Cups"] = new TarotMeaning
        {
            title = "Seven of Cups",
            upright = "환상, 선택의 혼란, 다양한 기회",
            reversed = "현실 인식, 명확한 선택",
            love = "이상과 현실의 간극, 환상 속 연애 주의",
            career = "프로젝트 다중 선택·우선순위 필요",
            advice = "환상을 걷어내고 현실적 선택을 해라.",
            money = "다양한 투자 기회, 그러나 선택 혼란.",
            health = "과도한 정보로 혼란, 한 가지 루틴 집중."
        },

        ["Eight of Cups"] = new TarotMeaning
        {
            title = "Eight of Cups",
            upright = "무언가를 뒤로하고 떠남, 더 나은 길 탐색",
            reversed = "미련, 떠나지 못함, 회피",
            love = "관계 정리·감정적 거리두기",
            career = "직장/프로젝트에서 물러남, 전환점",
            advice = "현 상황에 안주 말고 필요한 결단을 내려라.",
            money = "비효율 지출 정리, 새로운 수입원 탐색.",
            health = "해로운 습관을 끊고 건강한 길로 이동."
        },

        ["Nine of Cups"] = new TarotMeaning
        {
            title = "Nine of Cups",
            upright = "만족, 성취, 소원 성취, 즐거움",
            reversed = "과도한 욕심, 만족 부족",
            love = "바람이 이루어지는 관계, 감정적 충족",
            career = "성과·보상·프로젝트 성공",
            advice = "현재 성취를 즐기되 자만하지 말라.",
            money = "재정적 성취, 원하는 것 충족.",
            health = "건강 호전, 만족스러운 컨디션."
        },

        ["Ten of Cups"] = new TarotMeaning
        {
            title = "Ten of Cups",
            upright = "행복한 가정, 조화, 완전한 사랑",
            reversed = "불화, 깨진 조화, 이상과 현실 차이",
            love = "가정의 안정·장기적 행복",
            career = "팀워크 완성, 공동 목표 달성",
            advice = "진정한 만족은 함께할 때 완성된다.",
            money = "가족·주택 관련 지출, 재정 안정.",
            health = "심리적 안정, 가족과의 건강한 생활."
        },

        ["Page of Cups"] = new TarotMeaning
        {
            title = "Page of Cups",
            upright = "감정적 메시지, 직감, 창의적 시작",
            reversed = "미성숙, 현실 회피, 감정 기복",
            love = "풋풋한 고백, 설렘, 감정 어린 관계",
            career = "신선한 아이디어, 직감적 영감",
            advice = "마음을 열고 작은 감정의 신호를 따라라.",
            money = "작은 재정 기회, 창의적 수입.",
            health = "컨디션 변동 크나 회복 가능성 높음."
        },

        ["Knight of Cups"] = new TarotMeaning
        {
            title = "Knight of Cups",
            upright = "로맨틱, 제안, 매력, 직관적 행동",
            reversed = "현실성 부족, 과민, 바람둥이",
            love = "고백, 로맨틱한 제안",
            career = "비전 제안, 크리에이티브 프로젝트",
            advice = "감정에 솔직하되 현실적 기반을 확인하라.",
            money = "창의적 제안·프리랜스 수입.",
            health = "감정 기복 관리 필요, 수분·휴식 중요."
        },

        ["Queen of Cups"] = new TarotMeaning
        {
            title = "Queen of Cups",
            upright = "공감, 직관, 감정적 성숙, 돌봄",
            reversed = "감정 기복, 의존, 자기희생 과다",
            love = "따뜻한 교감, 돌보는 사랑",
            career = "팀원/고객의 감정 관리 능력",
            advice = "공감과 직관을 발휘하라.",
            money = "재정 관리 능력, 감정적 소비 주의.",
            health = "정서 안정, 심리치료·마음 관리에 길."
        },

        ["King of Cups"] = new TarotMeaning
        {
            title = "King of Cups",
            upright = "감정의 균형, 현명한 조언자, 성숙한 리더십",
            reversed = "조작적, 냉정, 감정 억제",
            love = "성숙하고 안정된 사랑, 배려 깊은 파트너",
            career = "감정적 리더십, 현명한 의사결정",
            advice = "감정을 통제하며 현명하게 이끌어라.",
            money = "안정적 자산 관리, 현명한 투자.",
            health = "심리적 안정, 스트레스 관리 우선."
        },

        // Minor Arcana - Wands --------------------------------------------------------------------------

        ["Ace of Wands"] = new TarotMeaning
        {
            title = "Ace of Wands",
            upright = "새로운 기회, 창조, 열정, 행동의 시작",
            reversed = "지연, 동기 부족, 기회 상실",
            love = "새로운 연애의 불꽃, 열정의 시작",
            career = "신규 프로젝트, 창의적 돌파구",
            advice = "열정을 행동으로 옮겨라.",
            money = "새로운 수입원·프로젝트에서 재정 기회.",
            health = "활력의 시작, 운동 루틴 착수에 길."
        },

        ["Two of Wands"] = new TarotMeaning
        {
            title = "Two of Wands",
            upright = "계획, 선택, 미래 전망",
            reversed = "불확실성, 선택 회피, 두려움",
            love = "관계의 방향을 결정할 기점",
            career = "진로/사업의 전략적 결정 필요",
            advice = "큰 그림을 그리고 과감히 선택하라.",
            money = "재정 방향성 결정, 투자/저축 전략 필요.",
            health = "건강 계획 세우기, 장기 루틴 준비."
        },

        ["Three of Wands"] = new TarotMeaning
        {
            title = "Three of Wands",
            upright = "확장, 기회, 원대한 비전",
            reversed = "좌절, 지연, 협력 부족",
            love = "장거리 연애·미래 계획 논의",
            career = "해외/새로운 시장 기회",
            advice = "시야를 넓히고 협력자와 함께 나아가라.",
            money = "해외/확장 투자 운, 장기적 기회.",
            health = "체력 확장기, 활동 반경이 넓어짐."
        },

        ["Four of Wands"] = new TarotMeaning
        {
            title = "Four of Wands",
            upright = "안정, 축하, 성취, 공동체",
            reversed = "불안정, 준비 부족, 불화",
            love = "결혼·약혼·가정적 안정",
            career = "프로젝트 성과·팀워크의 결실",
            advice = "성과를 나누고 안정적 기반을 다져라.",
            money = "안정적 재정 기반, 가정/주택 관련 길.",
            health = "안정기, 휴식과 회복의 시기."
        },

        ["Five of Wands"] = new TarotMeaning
        {
            title = "Five of Wands",
            upright = "경쟁, 갈등, 도전, 논쟁",
            reversed = "타협, 해결, 무력한 싸움",
            love = "사소한 다툼·삼각관계·경쟁",
            career = "직장 내 경쟁·意見 충돌",
            advice = "경쟁을 피하지 말고 공정하게 맞서라.",
            money = "재정적 경쟁/갈등, 지출 충돌.",
            health = "스트레스성 체력 소모, 과도 경쟁 주의."
        },

        ["Six of Wands"] = new TarotMeaning
        {
            title = "Six of Wands",
            upright = "승리, 인정을 받음, 성공",
            reversed = "실패, 오만, 좌절",
            love = "관계의 성취·주위의 인정",
            career = "성과 인정·리더십 성과",
            advice = "승리를 즐기되 겸손을 잊지 마라.",
            money = "성과 인정, 보너스·수입 상승.",
            health = "컨디션 호조, 목표 달성에 에너지 상승."
        },

        ["Seven of Wands"] = new TarotMeaning
        {
            title = "Seven of Wands",
            upright = "방어, 경쟁에서의 우위, 자신감",
            reversed = "압도당함, 방어 실패, 두려움",
            love = "관계를 지키려는 투쟁",
            career = "자리를 지키기 위한 노력",
            advice = "당당히 입장을 지켜라.",
            money = "재정 방어, 자산 지키기 위한 노력.",
            health = "체력은 유지되나 방어적, 면역 주의."
        },

        ["Eight of Wands"] = new TarotMeaning
        {
            title = "Eight of Wands",
            upright = "빠른 진행, 소통, 기회, 돌파구",
            reversed = "지연, 혼란, 급한 결정",
            love = "빠른 진전, 메시지·연락",
            career = "빠른 프로젝트 진행, 해외 교류",
            advice = "속도를 활용하되 성급한 결정은 피하라.",
            money = "빠른 수입 변동, 해외 거래 운.",
            health = "회복/변화가 빠르게 진행됨."
        },

        ["Nine of Wands"] = new TarotMeaning
        {
            title = "Nine of Wands",
            upright = "인내, 경계, 끝까지 버팀",
            reversed = "지침, 포기, 불신",
            love = "다툼 후에도 관계를 지키려는 노력",
            career = "프로젝트 마무리 전 방어적 태도",
            advice = "끝까지 버티되 유연함도 잃지 마라.",
            money = "재정적 압박, 끝까지 버텨내는 국면.",
            health = "피로 누적, 끝까지 버티는 상황."
        },

        ["Ten of Wands"] = new TarotMeaning
        {
            title = "Ten of Wands",
            upright = "과중한 부담, 책임, 무거운 짐",
            reversed = "해방, 책임 회피, 과로",
            love = "관계의 짐·책임 과다",
            career = "업무 과중·책임의 압박",
            advice = "혼자 짊어지지 말고 나눠라.",
            money = "과중한 재정 책임·빚 부담.",
            health = "과로·스트레스 누적, 부담 주의."
        },

        ["Page of Wands"] = new TarotMeaning
        {
            title = "Page of Wands",
            upright = "열정적 시작, 호기심, 새로운 아이디어",
            reversed = "방향 상실, 미성숙, 충동",
            love = "가벼운 만남, 새로움에 설레는 관계",
            career = "신규 프로젝트 제안, 탐험적 시도",
            advice = "호기심을 따라가되 방향성을 잡아라.",
            money = "작은 투자·새로운 재정 아이디어.",
            health = "새로운 운동/건강 습관 시도."
        },

        ["Knight of Wands"] = new TarotMeaning
        {
            title = "Knight of Wands",
            upright = "에너지, 모험, 추진력, 열정",
            reversed = "성급함, 좌절, 변덕",
            love = "빠른 진전·강렬한 연애",
            career = "진취적 행동·해외 기회",
            advice = "과감히 도전하되 준비를 소홀히 말라.",
            money = "모험적 투자, 단기 수익 기회.",
            health = "활발한 체력 활동, 충동적 무리 주의."
        },

        ["Queen of Wands"] = new TarotMeaning
        {
            title = "Queen of Wands",
            upright = "자신감, 매력, 독립성, 리더십",
            reversed = "불안정, 질투, 자기중심적",
            love = "매력적·주도적 파트너",
            career = "카리스마 리더, 조직을 이끄는 힘",
            advice = "자신감을 드러내고 주도권을 쥐어라.",
            money = "재정 운영 능력 상승, 자산 관리 자신감.",
            health = "심리적 활력·자신감이 건강에 도움."
        },

        ["King of Wands"] = new TarotMeaning
        {
            title = "King of Wands",
            upright = "비전, 리더십, 장기적 안목",
            reversed = "독단, 충동, 무책임",
            love = "성숙하고 책임감 있는 파트너",
            career = "사업가·리더의 성과",
            advice = "큰 그림을 그리고 리더십을 발휘하라.",
            money = "장기적 비전 기반의 재정 성공.",
            health = "리더십과 안정감, 꾸준한 체력 유지."
        },

        // Minor Arcana - Pentacles-------------------------------------------------------------------------

        ["Ace of Pentacles"] = new TarotMeaning
        {
            title = "Ace of Pentacles",
            upright = "새로운 재정적 기회, 안정적 시작, 번영의 씨앗",
            reversed = "지연, 기회 상실, 불안정한 시작",
            love = "안정적 관계의 시작, 현실적인 연애",
            career = "새로운 직업 기회, 재정적 성취",
            advice = "실질적인 기반을 마련하라.",
            money = "새로운 재정 기회, 안정적 수입 시작.",
            health = "건강한 습관·기초 체력 다지기."
        },

        ["Two of Pentacles"] = new TarotMeaning
        {
            title = "Two of Pentacles",
            upright = "균형, 조율, 다중 과제 관리",
            reversed = "불균형, 혼란, 과로",
            love = "연애와 생활의 균형 필요",
            career = "여러 프로젝트 동시 진행",
            advice = "우선순위를 정하고 균형을 맞춰라.",
            money = "수입·지출 균형 필요, 자금 juggling.",
            health = "컨디션 기복, 생활 리듬 조율 필요."
        },

        ["Three of Pentacles"] = new TarotMeaning
        {
            title = "Three of Pentacles",
            upright = "협력, 팀워크, 기술의 발전",
            reversed = "불화, 협력 부족, 서툰 실행",
            love = "협력적인 관계, 함께 성장",
            career = "프로젝트 협력, 전문성 발휘",
            advice = "함께 할 때 성과가 커진다.",
            money = "협업으로 재정 성과, 공동 프로젝트 길.",
            health = "협력적 운동·건강 관리가 효과적."
        },

        ["Four of Pentacles"] = new TarotMeaning
        {
            title = "Four of Pentacles",
            upright = "소유, 안정, 집착, 절약",
            reversed = "손실, 집착 해소, 불안정",
            love = "집착하는 사랑, 안정 추구",
            career = "재정 보수적, 자원 고수",
            advice = "놓아야 할 때는 과감히 놓아라.",
            money = "저축·재정 보수적, 자산 고수.",
            health = "긴장·집착으로 인한 체력 경직."
        },

        ["Five of Pentacles"] = new TarotMeaning
        {
            title = "Five of Pentacles",
            upright = "궁핍, 고립, 손실, 불안",
            reversed = "회복, 지원, 기회 발견",
            love = "외로운 관계, 경제적 어려움 속 연애",
            career = "재정 손실, 실직 위험",
            advice = "도움을 구하고 희망을 잃지 마라.",
            money = "재정 손실·곤란, 도움 요청 필요.",
            health = "체력 저하·질병, 외로움 주의."
        },

        ["Six of Pentacles"] = new TarotMeaning
        {
            title = "Six of Pentacles",
            upright = "나눔, 지원, 균형 잡힌 관계",
            reversed = "불균형한 관계, 조건부 도움",
            love = "한쪽이 베풀고 다른 한쪽이 받는 관계",
            career = "재정적 후원, 보상·인센티브",
            advice = "주는 것과 받는 것을 균형 있게 하라.",
            money = "재정적 지원·보상·균형 잡힌 교환.",
            health = "도움 주고받는 건강 회복."
        },

        ["Seven of Pentacles"] = new TarotMeaning
        {
            title = "Seven of Pentacles",
            upright = "인내, 장기적 시야, 투자",
            reversed = "조급함, 성급한 결론, 지연",
            love = "관계를 지켜보는 단계",
            career = "투자·노력의 결과 대기",
            advice = "조급해하지 말고 기다려라.",
            money = "장기 투자·인내 필요, 성과 대기.",
            health = "지속적 관리로 서서히 회복."
        },

        ["Eight of Pentacles"] = new TarotMeaning
        {
            title = "Eight of Pentacles",
            upright = "숙련, 반복 훈련, 자기계발",
            reversed = "미숙, 실수, 헛수고",
            love = "관계를 개선하기 위한 노력",
            career = "전문성 개발, 기술 숙련",
            advice = "꾸준히 배우고 갈고닦아라.",
            money = "장기 투자·인내 필요, 성과 대기.",
            health = "지속적 관리로 서서히 회복."
        },

        ["Nine of Pentacles"] = new TarotMeaning
        {
            title = "Nine of Pentacles",
            upright = "풍요, 독립, 성취, 만족",
            reversed = "허세, 의존, 물질적 불안",
            love = "자립적인 연애, 독립된 행복",
            career = "성공, 재정적 풍요",
            advice = "당당히 성취를 누려라.",
            money = "재정적 풍요·독립·성취.",
            health = "심신 만족·안정된 컨디션."
        },

        ["Ten of Pentacles"] = new TarotMeaning
        {
            title = "Ten of Pentacles",
            upright = "가족, 전통, 유산, 안정된 번영",
            reversed = "가정 불화, 재산 문제, 단절",
            love = "안정된 결혼·가족 중심",
            career = "장기적 사업 성공·안정",
            advice = "안정과 유산을 소중히 지켜라.",
            money = "가족·유산·장기적 재정 안정.",
            health = "가족 건강·유전적 관리 중요."
        },

        ["Page of Pentacles"] = new TarotMeaning
        {
            title = "Page of Pentacles",
            upright = "학습, 기회, 현실적 시작",
            reversed = "게으름, 무계획, 미성숙",
            love = "배움 같은 관계, 서투르지만 진지한 시작",
            career = "훈련·인턴십·학습의 기회",
            advice = "작은 기회라도 성실히 붙잡아라.",
            money = "새로운 수입 기회·학습 통한 재정 성장.",
            health = "건강 관련 공부·기초 습관 형성."
        },

        ["Knight of Pentacles"] = new TarotMeaning
        {
            title = "Knight of Pentacles",
            upright = "성실, 꾸준함, 책임감",
            reversed = "지루함, 고집, 경직",
            love = "느리지만 진중한 연애",
            career = "끈기 있는 업무 처리, 책임 완수",
            advice = "꾸준히 한 걸음씩 나아가라.",
            money = "성실한 재정 관리, 꾸준한 수입.",
            health = "지속적 루틴·규칙적인 건강 유지."
        },

        ["Queen of Pentacles"] = new TarotMeaning
        {
            title = "Queen of Pentacles",
            upright = "현실적, 따뜻한 배려, 안정",
            reversed = "의존, 과도한 걱정, 불안정",
            love = "가정을 돌보는 따뜻한 연애",
            career = "실용적·안정적 리더십",
            advice = "현실을 지키며 따뜻함을 잃지 마라.",
            money = "재정적 안정·가정 재정 관리 능력.",
            health = "따뜻한 돌봄, 안정된 건강 관리."
        },

        ["King of Pentacles"] = new TarotMeaning
        {
            title = "King of Pentacles",
            upright = "부, 안정, 권위, 책임",
            reversed = "탐욕, 독선, 실패",
            love = "안정적이고 책임감 있는 파트너",
            career = "성공한 사업가, 재정적 권위",
            advice = "안정과 책임을 중시하라.",
            money = "부·재정 권위·안정적 성취.",
            health = "튼튼한 체력·안정된 생활 습관."
        },
        // Minor Arcana - Swords-------------------------------------------------

        ["Ace of Swords"] = new TarotMeaning
        {
            title = "Ace of Swords",
            upright = "진실, 통찰, 명확한 판단, 결단의 시작",
            reversed = "혼란, 왜곡, 의사소통 문제",
            love = "솔직한 대화로 돌파",
            career = "분석·전략 수립, 명쾌한 결정",
            advice = "사실과 논리로 분명히 잘라라.",
            money = "명확한 판단으로 재정 기회 포착.",
            health = "건강 문제에 대한 통찰, 치료 방향 설정."
        },

        ["Two of Swords"] = new TarotMeaning
        {
            title = "Two of Swords",
            upright = "교착, 선택의 기로, 감정과 이성의 대치",
            reversed = "결정 회피 끝의 폭발, 편향",
            love = "감정 차단, 대화 필요",
            career = "결정 미루는 중, 데이터 더 수집",
            advice = "눈가리개를 벗고 선택하라.",
            money = "재정 결정 지연, 데이터 더 필요.",
            health = "건강 관련 선택 주저, 진단·치료 미룸."
        },

        ["Three of Swords"] = new TarotMeaning
        {
            title = "Three of Swords",
            upright = "상처, 실망, 삼각 갈등, 이별",
            reversed = "치유 시작, 통증의 완화",
            love = "상처 받은 관계, 이별·배신 이슈",
            career = "프로젝트 좌절, 냉혹한 피드백",
            advice = "아픔을 인정하고 회복 루틴을 만들라.",
            money = "재정 손실·투자 실패로 인한 상처.",
            health = "심리적 스트레스, 심장·혈압 주의."
        },

        ["Four of Swords"] = new TarotMeaning
        {
            title = "Four of Swords",
            upright = "휴식, 재충전, 정리, 명상",
            reversed = "회복 지연, 번아웃, 강제 휴식",
            love = "잠시 거리를 두고 정리",
            career = "휴가·리프레시·전략 재정비",
            advice = "멈추고 에너지를 회복하라.",
            money = "지출 보류, 재정 휴식기.",
            health = "휴식·재충전 필요, 번아웃 예방."
        },

        ["Five of Swords"] = new TarotMeaning
        {
            title = "Five of Swords",
            upright = "승리했지만 손상, 이기적 논쟁, 불신",
            reversed = "화해 시도, 손실 인정",
            love = "이기적 태도로 상처 남김",
            career = "정치 싸움, 승자 없는 승부",
            advice = "이기는 것보다 남기는 것을 보라.",
            money = "재정적 갈등, 승자 없는 손실.",
            health = "스트레스성 질환, 자기 관리 필요."
        },

        ["Six of Swords"] = new TarotMeaning
        {
            title = "Six of Swords",
            upright = "이동, 전환, 고통에서 벗어남",
            reversed = "미련, 이동 지연, 불완전한 전환",
            love = "관계의 새 국면으로 이동",
            career = "팀·업무 전환, 환경 변화",
            advice = "조용히 더 나은 쪽으로 옮겨가라.",
            money = "재정 전환·이동, 안정적 국면으로.",
            health = "치료·재활로 점진적 회복."
        },

        ["Seven of Swords"] = new TarotMeaning
        {
            title = "Seven of Swords",
            upright = "은밀함, 전략, 기지, 속임수",
            reversed = "들통, 정면 돌파 필요",
            love = "비밀·회피·양다리 경계",
            career = "전략적 행동 vs 비윤리적 술수",
            advice = "정면승부가 답인지, 기지가 답인지 분별하라.",
            money = "은밀한 투자·리스크, 사기 주의.",
            health = "은폐된 증상, 정밀 검진 필요."
        },

        ["Eight of Swords"] = new TarotMeaning
        {
            title = "Eight of Swords",
            upright = "자기 제한, 두려움, 갇힘",
            reversed = "해방, 관점 전환, 스스로 묶임을 해제",
            love = "두려움이 만든 거리, 솔직한 소통 필요",
            career = "규정/두려움에 묶임, 역할 재정의",
            advice = "스스로 만든 밧줄을 푸는 용기를 내라.",
            money = "재정적 제약, 빚이나 규제에 묶임.",
            health = "움직임 제한, 불안·긴장성 건강 문제."
        },

        ["Nine of Swords"] = new TarotMeaning
        {
            title = "Nine of Swords",
            upright = "불안, 악몽, 과도한 걱정",
            reversed = "불안에서 서서히 벗어남",
            love = "걱정이 관계를 잠식, 솔직한 공유 필요",
            career = "실수·리스크에 대한 과도한 불안",
            advice = "사실 확인과 수면·루틴으로 불안을 낮춰라.",
            money = "재정 불안·걱정, 잠 못 드는 시기.",
            health = "불면·불안·과도한 스트레스."
        },

        ["Ten of Swords"] = new TarotMeaning
        {
            title = "Ten of Swords",
            upright = "완전한 끝, 붕괴, 바닥",
            reversed = "바닥 찍고 반등, 재기",
            love = "관계의 종결, 혹은 패턴의 종식",
            career = "프로젝트 종결, 방향 전환",
            advice = "끝을 인정하고 새 시작을 준비하라.",
            money = "재정적 붕괴·완전한 손실.",
            health = "건강 최저점, 재기 필요."
        },

        ["Page of Swords"] = new TarotMeaning
        {
            title = "Page of Swords",
            upright = "호기심, 학습, 정보 수집, 신중한 관찰",
            reversed = "가십, 피상적 지식, 경솔한 말",
            love = "서로를 더 알아가는 단계",
            career = "리서치·분석·신기술 학습",
            advice = "빨리 말하기보다 정확히 조사하라.",
            money = "재정 관련 정보 수집·학습의 단계.",
            health = "새로운 건강 지식·예방 습득."
        },

        ["Knight of Swords"] = new TarotMeaning
        {
            title = "Knight of Swords",
            upright = "직진, 돌파, 결단, 빠른 사고",
            reversed = "성급, 무모, 독주",
            love = "직설·빠른 전개, 상처 주지 않게 주의",
            career = "과감한 실행·이슈 돌파",
            advice = "속도를 내되 주변 피해를 최소화하라.",
            money = "과감한 투자·단기적 돌파, 무모함 주의.",
            health = "과격한 활동·돌발 부상 주의."
        },

        ["Queen of Swords"] = new TarotMeaning
        {
            title = "Queen of Swords",
            upright = "이성적 통찰, 경계, 사실 중심 소통",
            reversed = "냉소, 고립, 과도한 비판",
            love = "감정과 이성의 균형 소통",
            career = "팩트·분석 기반 의사결정",
            advice = "따뜻함을 잃지 않되 사실을 말하라.",
            money = "재정 결정을 이성적으로 처리.",
            health = "심리적 거리두기, 냉정한 자기 관리."
        },

        ["King of Swords"] = new TarotMeaning
        {
            title = "King of Swords",
            upright = "권위 있는 지성, 공정, 전략, 법·규정",
            reversed = "권위 남용, 경직, 차가움",
            love = "성숙한 이성적 리더십",
            career = "정책·전략·법무·데이터 의사결정",
            advice = "원칙과 전략으로 냉정하게 이끌어라.",
            money = "재정 권위·법/정책 기반 안정.",
            health = "전문가 조언·진단 기반 건강 관리."
        },


        // 필요 카드들을 계속 추가…
    };

    /// 이름 문자열을 DB 키로 정규화
    public static string NormalizeName(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return "";
        var n = raw.Trim();

        // 역위 표기 예시 처리: " (R)", "(Reversed)" 등
        n = n.Replace("(Reversed)", "").Replace("(R)", "").Trim();

        // 대소문자 정규화
        return n;
    }

    public static bool TryGet(string rawName, out TarotMeaning m)
    {
        var key = NormalizeName(rawName);
        return _dict.TryGetValue(key, out m);
    }

    /// 간단: 역위 여부 판단
    public static bool IsReversed(string rawName)
    {
        if (string.IsNullOrEmpty(rawName)) return false;
        var s = rawName.ToLower();
        return s.Contains("(r)") || s.Contains("reversed");
    }

}
