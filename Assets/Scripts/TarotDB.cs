using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TarotMeaning
{
    public string title;        // ǥ�ÿ� �̸�
    public string upright;      // �⺻ �ǹ�(ª��)
    public string reversed;     // ���� �ǹ�(������ null/����)
    public string love;         // ����: ���ֹ���(ª��)
    public string career;       // ����: Ŀ�����(ª��)
    public string advice;       // ���� ����
    public string money;   // ���� ����(ª��)
    public string health;  // �ǰ� ����(ª��)
}

public static class TarotDB
{
    // �� ī��� key�� NormalizeName() ���� ���� ���߸� ��
    static readonly Dictionary<string, TarotMeaning> _dict = new()
    {
        // Major------------------------------------------------------------------------------
        ["The Fool"] = new TarotMeaning
        {
            title = "The Fool",
            upright = "�� ���, ������ ����, ����, ������ ����",
            reversed = "���, �غ� ����, ���� ���, ���� ȸ��",
            love = "������ ����, ������ ����",
            career = "�� ������Ʈ/������ ��; ��ȹ�� ��������",
            advice = "�η��򺸴� ȣ����� ������ �⺻ ������ġ�� ì�ܶ�.",
            money = "�۰� �����ϵ� ����ũ �ѵ� ��Ȯ��.",
            health = "�� ��ƾ�� ������ ����; ������ �ݹ�."
        },

        // 1. The Magician
        ["The Magician"] = new TarotMeaning
        {
            title = "The Magician",
            upright = "������ ���, �����, �ڿ� Ȱ��, �Ŵ��佺Ʈ",
            reversed = "���Ӽ�, ���� ����, ��� �̺�, ����",
            love = "�ֵ��� ǥ���� ���踦 �մ��",
            career = "���ȡ����������̼ǿ� ��; �ɷ� ����",
            advice = "���� ���� ī��� �ٷ� �����ض�.",
            money = "���Կ� �ٰ�ȭ/�ΰ����� �õ��� ��.",
            health = "����ü�º��� ������� ����ȭ; ����ȣ�� ����."
        },

        // 2. The High Priestess
        ["The High Priestess"] = new TarotMeaning
        {
            title = "The High Priestess",
            upright = "����, ���, �����ǽ�, ����� ����",
            reversed = "ȥ��, ���� ����, ����� �巯��",
            love = "�Ӹ��� �б�, ���θ��� ����",
            career = "�ڷᡤ������ �� �ı�, ������ �غ�",
            advice = "�ò����� �������� ������ ��ȣ�� �켱�϶�.",
            money = "�浿 �ż� ����; ����/������ ����.",
            health = "�޽ġ����� �켱. ���� �̼� ��ȣ üũ."
        },

        // 3. The Empress
        ["The Empress"] = new TarotMeaning
        {
            title = "The Empress",
            upright = "ǳ��, â����, ������, ����",
            reversed = "����ȣ, ��ü, ������, ����",
            love = "������ ������ ������ ǳ����",
            career = "�귣��/â��/�����迡 ����",
            advice = "Ű��� �����Ǹ� ����� ������.",
            money = "�����帧 ȣ������Ȱ�� ���� ���� ����.",
            health = "���硤����� ������ ȸ���� ���."
        },

        // 4. The Emperor
        ["The Emperor"] = new TarotMeaning
        {
            title = "The Emperor",
            upright = "����, ����, ������, å��",
            reversed = "��������, ����, ������ ����",
            love = "��衤��� ��Ȯȭ�� ���� ����",
            career = "����/����/���������� ����",
            advice = "��Ģ�� ����� �� ��Ģ���� �����϶�.",
            money = "���ꡤ��Ģ ����, ����� ���� �߽�.",
            health = "��ƾ ��������Ģ���� ��Ȱ�� ü�� ȸ��."
        },

        // 5. The Hierophant
        ["The Hierophant"] = new TarotMeaning
        {
            title = "The Hierophant",
            upright = "����, ����, ����, �Թ�",
            reversed = "�İ�, ������, ü������ ����",
            love = "����ȭ����ӡ���ġ ��ġ ����",
            career = "�ڰݡ����������μ��� �ؼ��� ����",
            advice = "������ ���� ���� ������ �����϶�.",
            money = "������ ���, ��� ���������� ����.",
            health = "�ǻ��� ��ħ/���̵���� �ؼ�."
        },

        // 6. The Lovers
        ["The Lovers"] = new TarotMeaning
        {
            title = "The Lovers",
            upright = "���, ��ġ ��ġ, �߿��� ����",
            reversed = "����ġ, ��Ȥ, �����δ�, �ﰢ",
            love = "��ȣ ���ð� �ŷڰ� �ٽ�",
            career = "��Ʈ�ʽ�/������ �ó���",
            advice = "��ġ�� �´� ���� ������ ���϶�.",
            money = "��������/���� �ǻ������ Ÿ�̹�.",
            health = "��Ȱ������ ���Բ��� �ٲٸ� ���Ӽ���."
        },

        // 7. The Chariot
        ["The Chariot"] = new TarotMeaning
        {
            title = "The Chariot",
            upright = "������ ����, ����, �¸�, �̵�",
            reversed = "����, ���� ���, ������ �л�",
            love = "������ �ֵ����ӵ� ����",
            career = "������� ���ġ���Ī����� ��",
            advice = "��鸮�� �������� �� �տ� ��� �о�ٿ���.",
            money = "��ǥ�� ���� ����̺�; ���� �Һ� ����.",
            health = "ü�� ��±�; �λ� ������ ����."
        },

        // 8. Strength
        ["Strength"] = new TarotMeaning
        {
            title = "Strength",
            upright = "���, �γ�, ���� ��, �ε巯�� ������",
            reversed = "�ڽŰ� ����, ������, ���� �⺹",
            love = "�������� ����� ���踦 �ܴ���",
            career = "���� �ִ� ����, ������ ��",
            advice = "���θ��� ���� ������ �а� ����.",
            money = "������ ����/��ȯ���� ü�� ����.",
            health = "������ �����Ȱ, ���̽� ����."
        },

        // 9. The Hermit
        ["The Hermit"] = new TarotMeaning
        {
            title = "The Hermit",
            upright = "���� Ž��, ����, ����, ���丵",
            reversed = "��, �����, Ÿ�� ����",
            love = "��� ������ ���� �ð�",
            career = "����ġ����ȹ�������� �ñ�",
            advice = "������ ���� �������� �ش��� ã��.",
            money = "���� ���� ����; ���ʿ� ���� ����.",
            health = "�޽�/���/�������� ȸ�� ���."
        },

        // 10. Wheel of Fortune
        ["Wheel of Fortune"] = new TarotMeaning
        {
            title = "Wheel of Fortune",
            upright = "��ȯ��, ���� �帧, ����Ŭ, ��ȸ",
            reversed = "Ÿ�̹� �̽�, �ݺ��Ǵ� ����",
            love = "���� ���� ��ȯ, Ÿ�̹�",
            career = "����/���� �ٶ� Ȱ��",
            advice = "���� �帧�� �а� ź�������� �ö�Ÿ��.",
            money = "������ �Ⱓ; �лꡤ���뷱��.",
            health = "����� ���; ������ ��� ����ȭ."
        },

        // 11. Justice
        ["Justice"] = new TarotMeaning
        {
            title = "Justice",
            upright = "����, ����, ����, ��/���",
            reversed = "�Ұ���, �ְ�, ����, å�� ȸ��",
            love = "������ ��ȭ�� ����",
            career = "��ࡤ������å �ؼ�",
            advice = "��ǰ� ��Ģ�� �ٰ��� �Ǵ��϶�.",
            money = "����/��� �ؼ�, ���-���� ����.",
            health = "���ұ� ���ϰ� ���� ���� ��Ȱ."
        },

        // 12. The Hanged Man
        ["The Hanged Man"] = new TarotMeaning
        {
            title = "The Hanged Man",
            upright = "���� ��ȯ, �Ͻ� ����, ��������, ���",
            reversed = "��ü�� ����, ��ȭ ȸ��",
            love = "��� ���߰� �þ� �ٲٱ�",
            career = "�켱���� ������, �ս� �ּ�ȭ",
            advice = "������ ���� �ƴ϶� ���ġ��.",
            money = "���� �����������; ����/Ȧ�� �Ǵ�.",
            health = "���� �ߴ�, ��Ʈ��Ī/ȸ���� ����."
        },

        // 13. Death
        ["Death"] = new TarotMeaning
        {
            title = "Death",
            upright = "����� ��ź��, ū ��ȭ, ����",
            reversed = "�̷�, ��ȭ ����, ���� ����",
            love = "���� ������ �����ϰ� �� ������",
            career = "���� �� �������ǹ�",
            advice = "������ ���۵ȴ�. ������ �����϶�.",
            money = "���ʿ� ����/��ä ������ �����.",
            health = "���� ��ü�� ���� Ÿ�̹�."
        },

        // 14. Temperance
        ["Temperance"] = new TarotMeaning
        {
            title = "Temperance",
            upright = "��ȭ, ����, �߿�, ����",
            reversed = "�ұ���, �����ұ�, ���� ����",
            love = "�ӵ��� �µ� ����",
            career = "�� �� ���������� ������Ʈ�� ��",
            advice = "�����԰� Ÿ�̹��� ���� ������ �����.",
            money = "����/���� ����; ���Һ� ����.",
            health = "���̡����顤��� �뷱�� ���߱�."
        },

        // 15. The Devil
        ["The Devil"] = new TarotMeaning
        {
            title = "The Devil",
            upright = "����, ��Ȥ, �ߵ�, ����� �ð���",
            reversed = "�ع��� �õ�, �������� ���",
            love = "������ ����/���� ����",
            career = "���� ���� ����ũ������ Ȯ��",
            advice = "�ŷ����� �����ϼ��� ���⸦ �����϶�.",
            money = "��/���Һ�/��ݸ� ���� ����.",
            health = "�ߵ��� ���� ����; �طο� ��Ȥ �Ÿ��α�."
        },

        // 16. The Tower
        ["The Tower"] = new TarotMeaning
        {
            title = "The Tower",
            upright = "���۽��� �ر�, ���� ����, ����",
            reversed = "���� �ּ�ȭ, ���, ���� �տ�",
            love = "�㹰������ �� Ʋ�� �ر�",
            career = "������������� �̽�������ũ",
            advice = "�������� ���� ������ ���� ����� �غ��϶�.",
            money = "����ġ ���� ����/�ս� ���(����ڱ�).",
            health = "�޼� ����� �϶� ���; ��� �ɾ�."
        },

        // 17. The Star
        ["The Star"] = new TarotMeaning
        {
            title = "The Star",
            upright = "���, ġ��, ����, ������",
            reversed = "�ǽ�, ȸ�� ����, �ڽŰ� ����",
            love = "�����԰� ����� ȸ��",
            career = "�귣��/ũ������Ƽ��/������ ��",
            advice = "����� ���� ���� �� ������ ȸ���϶�.",
            money = "����ǥ �缳���������� �簳.",
            health = "ȸ������Ȱ�� ȣ��; �������� �ٽ�."
        },

        // 18. The Moon
        ["The Moon"] = new TarotMeaning
        {
            title = "The Moon",
            upright = "�Ҿ�, ����, ���� ��, ������ �ĵ�",
            reversed = "ȥ�� �ؼ�, ���� ����",
            love = "���ظ� ��� �������� Ǯ��",
            career = "���� ������, ���� ����������",
            advice = "������ ����� �и��� Ȯ���϶�.",
            money = "��ӡ��浿 �ŷ� ����; Ȯ�� �� �ൿ.",
            health = "����/���� �⺹ ����; ������� ����."
        },

        // 19. The Sun
        ["The Sun"] = new TarotMeaning
        {
            title = "The Sun",
            upright = "����, �����, ���, Ȱ��",
            reversed = "����, �Ƿ�, �ָ� ����",
            love = "��� �ǰ��� ������ ����",
            career = "���� ����, ȫ��/��Ī ȣ��",
            advice = "�ܼ�����Ȯ������ϰ� ���� �����.",
            money = "���� ȣ�������� ���ʽ� ���.",
            health = "Ȱ�� ���; ����/Ż���� ����."
        },

        // 20. Judgement
        ["Judgement"] = new TarotMeaning
        {
            title = "Judgement",
            upright = "����, �θ�, ��, �絵��",
            reversed = "��å, �̰�, ��� ȸ��",
            love = "���� �������뼭���� ����",
            career = "����/ä��/������ �ñ�",
            advice = "�������� �ൿ���� ��ȯ�϶�.",
            money = "�繫 ����/���뷱��; ���ʿ� ����.",
            health = "�������/��ġ �������� ���� ����."
        },

        // 21. The World
        ["The World"] = new TarotMeaning
        {
            title = "The World",
            upright = "�ϼ�, ����, ����, ���� �ܰ��",
            reversed = "�̿�, ������ ����, ����",
            love = "������ ����/���� é��",
            career = "������Ʈ ����, �۷ι�/Ȯ��",
            advice = "��ħǥ�� ��� �� ������ �����϶�.",
            money = "��� ��ǥ �޼����ڻ� ���� �ϼ� �ܰ�.",
            health = "��ǥ �޼���; ����/���� �÷� ����."
        },



        // Minor Arcana - Cups---------------------------------------------------------------------------------------

        ["Ace of Cups"] = new TarotMeaning
        {
            title = "Ace of Cups",
            upright = "���ο� ���, ������ ����, ����, ǳ��ο� ����",
            reversed = "�ﴭ�� ����, ��ȸ ���, �޸���",
            love = "�ű� ���֡���顤������ ����",
            career = "â���� ���̵��, ����ũ ���",
            advice = "������ ���� ������ ǥ���ض�.",
            money = "���� ��ȸ�����ʽ� ����, ���� �ڱ� ��.",
            health = "����� ȸ�� ����, Ȱ���� ������."
        },

        ["Two of Cups"] = new TarotMeaning
        {
            title = "Two of Cups",
            upright = "��ȭ�ο� ����, ��Ʈ�ʽ�, ����",
            reversed = "�ұ����� ����, ����, ��ȭ",
            love = "��ȣ ��������ӡ�¦�� ī��",
            career = "��Ʈ�ʽʡ����� ������Ʈ ����",
            advice = "���� ����ϰ� ���߰� �����϶�.",
            money = "���� ���ڡ����� ���¿� ��.",
            health = "���� �, Ŀ��/������ �ϴ� �ǰ� ����."
        },

        ["Three of Cups"] = new TarotMeaning
        {
            title = "Three of Cups",
            upright = "����, ����, ����, ����",
            reversed = "������ ����, �ﰢ����, ��ȭ",
            love = "ģ�� �̻��� ���衤��ſ� ����",
            career = "��������Ʈ��ŷ ����",
            advice = "�Բ� �⻵�ϰ� ������ ������.",
            money = "�����硤���� ����, ��ſ� �Һ�.",
            health = "��ȸ�� Ȱ������ Ȱ���� ����."
        },

        ["Four of Cups"] = new TarotMeaning
        {
            title = "Four of Cups",
            upright = "����, ������, ���� Ž��",
            reversed = "��ȸ ���ν�, ����� �غ�",
            love = "�����ԡ���� ���, ������ ����ȭ ��ȸ ����",
            career = "���� ����, ������ ���ο� ���� ����",
            advice = "���� ������ ���� �ֺ� ��ȸ�� ����.",
            money = "��ȸ ���������� ���� ����, �ٽ� ���� �ʿ�.",
            health = "����¡����°�, �޽İ� �ڱ� �ʿ�."
        },

        ["Five of Cups"] = new TarotMeaning
        {
            title = "Five of Cups",
            upright = "���, �Ǹ�, ��ȸ, ������ ����",
            reversed = "ȸ��, ����, �ٽ� �Ͼ",
            love = "�̺�/��ȸ, �׷��� �� ����� ������ ����",
            career = "������Ʈ �ս�, ���� �� �н�",
            advice = "���� �ͺ��� ���� ���� ����.",
            money = "���� �սǡ����� �� �ƽ���.",
            health = "�ǰ� ���ϡ��Ǹ���, ȸ�� ���� �ʿ�."
        },

        ["Six of Cups"] = new TarotMeaning
        {
            title = "Six of Cups",
            upright = "�߾�, ���, ������ ��ȸ, ������",
            reversed = "���� ����, �̼���, ���� ����",
            love = "�� �ο� �����, ������ ����",
            career = "���� ���衤�θ��� ����",
            advice = "������ ������ ����ϵ� ���翡 �����϶�.",
            money = "���� ���ڡ����ꡤ������ ��.",
            health = "�� �������� ȸ��, ��Ȱ���� ����."
        },

        ["Seven of Cups"] = new TarotMeaning
        {
            title = "Seven of Cups",
            upright = "ȯ��, ������ ȥ��, �پ��� ��ȸ",
            reversed = "���� �ν�, ��Ȯ�� ����",
            love = "�̻�� ������ ����, ȯ�� �� ���� ����",
            career = "������Ʈ ���� ���á��켱���� �ʿ�",
            advice = "ȯ���� �Ⱦ�� ������ ������ �ض�.",
            money = "�پ��� ���� ��ȸ, �׷��� ���� ȥ��.",
            health = "������ ������ ȥ��, �� ���� ��ƾ ����."
        },

        ["Eight of Cups"] = new TarotMeaning
        {
            title = "Eight of Cups",
            upright = "���𰡸� �ڷ��ϰ� ����, �� ���� �� Ž��",
            reversed = "�̷�, ������ ����, ȸ��",
            love = "���� ������������ �Ÿ��α�",
            career = "����/������Ʈ���� ������, ��ȯ��",
            advice = "�� ��Ȳ�� ���� ���� �ʿ��� ����� ������.",
            money = "��ȿ�� ���� ����, ���ο� ���Կ� Ž��.",
            health = "�طο� ������ ���� �ǰ��� ��� �̵�."
        },

        ["Nine of Cups"] = new TarotMeaning
        {
            title = "Nine of Cups",
            upright = "����, ����, �ҿ� ����, ��ſ�",
            reversed = "������ ���, ���� ����",
            love = "�ٶ��� �̷������ ����, ������ ����",
            career = "����������������Ʈ ����",
            advice = "���� ���븦 ���� �ڸ����� ����.",
            money = "������ ����, ���ϴ� �� ����.",
            health = "�ǰ� ȣ��, ���������� �����."
        },

        ["Ten of Cups"] = new TarotMeaning
        {
            title = "Ten of Cups",
            upright = "�ູ�� ����, ��ȭ, ������ ���",
            reversed = "��ȭ, ���� ��ȭ, �̻�� ���� ����",
            love = "������ ����������� �ູ",
            career = "����ũ �ϼ�, ���� ��ǥ �޼�",
            advice = "������ ������ �Բ��� �� �ϼ��ȴ�.",
            money = "���������� ���� ����, ���� ����.",
            health = "�ɸ��� ����, �������� �ǰ��� ��Ȱ."
        },

        ["Page of Cups"] = new TarotMeaning
        {
            title = "Page of Cups",
            upright = "������ �޽���, ����, â���� ����",
            reversed = "�̼���, ���� ȸ��, ���� �⺹",
            love = "ǲǲ�� ���, ����, ���� � ����",
            career = "�ż��� ���̵��, ������ ����",
            advice = "������ ���� ���� ������ ��ȣ�� �����.",
            money = "���� ���� ��ȸ, â���� ����.",
            health = "����� ���� ũ�� ȸ�� ���ɼ� ����."
        },

        ["Knight of Cups"] = new TarotMeaning
        {
            title = "Knight of Cups",
            upright = "�θ�ƽ, ����, �ŷ�, ������ �ൿ",
            reversed = "���Ǽ� ����, ����, �ٶ�����",
            love = "���, �θ�ƽ�� ����",
            career = "���� ����, ũ������Ƽ�� ������Ʈ",
            advice = "������ �����ϵ� ������ ����� Ȯ���϶�.",
            money = "â���� ���ȡ��������� ����.",
            health = "���� �⺹ ���� �ʿ�, ���С��޽� �߿�."
        },

        ["Queen of Cups"] = new TarotMeaning
        {
            title = "Queen of Cups",
            upright = "����, ����, ������ ����, ����",
            reversed = "���� �⺹, ����, �ڱ���� ����",
            love = "������ ����, ������ ���",
            career = "����/���� ���� ���� �ɷ�",
            advice = "������ ������ �����϶�.",
            money = "���� ���� �ɷ�, ������ �Һ� ����.",
            health = "���� ����, �ɸ�ġ�ᡤ���� ������ ��."
        },

        ["King of Cups"] = new TarotMeaning
        {
            title = "King of Cups",
            upright = "������ ����, ������ ������, ������ ������",
            reversed = "������, ����, ���� ����",
            love = "�����ϰ� ������ ���, ��� ���� ��Ʈ��",
            career = "������ ������, ������ �ǻ����",
            advice = "������ �����ϸ� �����ϰ� �̲����.",
            money = "������ �ڻ� ����, ������ ����.",
            health = "�ɸ��� ����, ��Ʈ���� ���� �켱."
        },

        // Minor Arcana - Wands --------------------------------------------------------------------------

        ["Ace of Wands"] = new TarotMeaning
        {
            title = "Ace of Wands",
            upright = "���ο� ��ȸ, â��, ����, �ൿ�� ����",
            reversed = "����, ���� ����, ��ȸ ���",
            love = "���ο� ������ �Ҳ�, ������ ����",
            career = "�ű� ������Ʈ, â���� ���ı�",
            advice = "������ �ൿ���� �Űܶ�.",
            money = "���ο� ���Կ���������Ʈ���� ���� ��ȸ.",
            health = "Ȱ���� ����, � ��ƾ ������ ��."
        },

        ["Two of Wands"] = new TarotMeaning
        {
            title = "Two of Wands",
            upright = "��ȹ, ����, �̷� ����",
            reversed = "��Ȯ�Ǽ�, ���� ȸ��, �η���",
            love = "������ ������ ������ ����",
            career = "����/����� ������ ���� �ʿ�",
            advice = "ū �׸��� �׸��� ������ �����϶�.",
            money = "���� ���⼺ ����, ����/���� ���� �ʿ�.",
            health = "�ǰ� ��ȹ �����, ��� ��ƾ �غ�."
        },

        ["Three of Wands"] = new TarotMeaning
        {
            title = "Three of Wands",
            upright = "Ȯ��, ��ȸ, ������ ����",
            reversed = "����, ����, ���� ����",
            love = "��Ÿ� ���֡��̷� ��ȹ ����",
            career = "�ؿ�/���ο� ���� ��ȸ",
            advice = "�þ߸� ������ �����ڿ� �Բ� ���ư���.",
            money = "�ؿ�/Ȯ�� ���� ��, ����� ��ȸ.",
            health = "ü�� Ȯ���, Ȱ�� �ݰ��� �о���."
        },

        ["Four of Wands"] = new TarotMeaning
        {
            title = "Four of Wands",
            upright = "����, ����, ����, ����ü",
            reversed = "�Ҿ���, �غ� ����, ��ȭ",
            love = "��ȥ����ȥ�������� ����",
            career = "������Ʈ ����������ũ�� ���",
            advice = "������ ������ ������ ����� ������.",
            money = "������ ���� ���, ����/���� ���� ��.",
            health = "������, �޽İ� ȸ���� �ñ�."
        },

        ["Five of Wands"] = new TarotMeaning
        {
            title = "Five of Wands",
            upright = "����, ����, ����, ����",
            reversed = "Ÿ��, �ذ�, ������ �ο�",
            love = "����� �������ﰢ���衤����",
            career = "���� �� �����̸ �浹",
            advice = "������ ������ ���� �����ϰ� �¼���.",
            money = "������ ����/����, ���� �浹.",
            health = "��Ʈ������ ü�� �Ҹ�, ���� ���� ����."
        },

        ["Six of Wands"] = new TarotMeaning
        {
            title = "Six of Wands",
            upright = "�¸�, ������ ����, ����",
            reversed = "����, ����, ����",
            love = "������ ���롤������ ����",
            career = "���� ������������ ����",
            advice = "�¸��� ���� ����� ���� ����.",
            money = "���� ����, ���ʽ������� ���.",
            health = "����� ȣ��, ��ǥ �޼��� ������ ���."
        },

        ["Seven of Wands"] = new TarotMeaning
        {
            title = "Seven of Wands",
            upright = "���, ���￡���� ����, �ڽŰ�",
            reversed = "�е�����, ��� ����, �η���",
            love = "���踦 ��Ű���� ����",
            career = "�ڸ��� ��Ű�� ���� ���",
            advice = "����� ������ ���Ѷ�.",
            money = "���� ���, �ڻ� ��Ű�� ���� ���.",
            health = "ü���� �����ǳ� �����, �鿪 ����."
        },

        ["Eight of Wands"] = new TarotMeaning
        {
            title = "Eight of Wands",
            upright = "���� ����, ����, ��ȸ, ���ı�",
            reversed = "����, ȥ��, ���� ����",
            love = "���� ����, �޽���������",
            career = "���� ������Ʈ ����, �ؿ� ����",
            advice = "�ӵ��� Ȱ���ϵ� ������ ������ ���϶�.",
            money = "���� ���� ����, �ؿ� �ŷ� ��.",
            health = "ȸ��/��ȭ�� ������ �����."
        },

        ["Nine of Wands"] = new TarotMeaning
        {
            title = "Nine of Wands",
            upright = "�γ�, ���, ������ ����",
            reversed = "��ħ, ����, �ҽ�",
            love = "���� �Ŀ��� ���踦 ��Ű���� ���",
            career = "������Ʈ ������ �� ����� �µ�",
            advice = "������ ��Ƽ�� �����Ե� ���� ����.",
            money = "������ �й�, ������ ���߳��� ����.",
            health = "�Ƿ� ����, ������ ��Ƽ�� ��Ȳ."
        },

        ["Ten of Wands"] = new TarotMeaning
        {
            title = "Ten of Wands",
            upright = "������ �δ�, å��, ���ſ� ��",
            reversed = "�ع�, å�� ȸ��, ����",
            love = "������ ����å�� ����",
            career = "���� ���ߡ�å���� �й�",
            advice = "ȥ�� �������� ���� ������.",
            money = "������ ���� å�ӡ��� �δ�.",
            health = "���Ρ���Ʈ���� ����, �δ� ����."
        },

        ["Page of Wands"] = new TarotMeaning
        {
            title = "Page of Wands",
            upright = "������ ����, ȣ���, ���ο� ���̵��",
            reversed = "���� ���, �̼���, �浿",
            love = "������ ����, ���ο� ������ ����",
            career = "�ű� ������Ʈ ����, Ž���� �õ�",
            advice = "ȣ����� ���󰡵� ���⼺�� ��ƶ�.",
            money = "���� ���ڡ����ο� ���� ���̵��.",
            health = "���ο� �/�ǰ� ���� �õ�."
        },

        ["Knight of Wands"] = new TarotMeaning
        {
            title = "Knight of Wands",
            upright = "������, ����, ������, ����",
            reversed = "������, ����, ����",
            love = "���� ������������ ����",
            career = "������ �ൿ���ؿ� ��ȸ",
            advice = "������ �����ϵ� �غ� ��Ȧ�� ����.",
            money = "������ ����, �ܱ� ���� ��ȸ.",
            health = "Ȱ���� ü�� Ȱ��, �浿�� ���� ����."
        },

        ["Queen of Wands"] = new TarotMeaning
        {
            title = "Queen of Wands",
            upright = "�ڽŰ�, �ŷ�, ������, ������",
            reversed = "�Ҿ���, ����, �ڱ��߽���",
            love = "�ŷ������ֵ��� ��Ʈ��",
            career = "ī������ ����, ������ �̲��� ��",
            advice = "�ڽŰ��� �巯���� �ֵ����� ����.",
            money = "���� � �ɷ� ���, �ڻ� ���� �ڽŰ�.",
            health = "�ɸ��� Ȱ�¡��ڽŰ��� �ǰ��� ����."
        },

        ["King of Wands"] = new TarotMeaning
        {
            title = "King of Wands",
            upright = "����, ������, ����� �ȸ�",
            reversed = "����, �浿, ��å��",
            love = "�����ϰ� å�Ӱ� �ִ� ��Ʈ��",
            career = "������������� ����",
            advice = "ū �׸��� �׸��� �������� �����϶�.",
            money = "����� ���� ����� ���� ����.",
            health = "�����ʰ� ������, ������ ü�� ����."
        },

        // Minor Arcana - Pentacles-------------------------------------------------------------------------

        ["Ace of Pentacles"] = new TarotMeaning
        {
            title = "Ace of Pentacles",
            upright = "���ο� ������ ��ȸ, ������ ����, ������ ����",
            reversed = "����, ��ȸ ���, �Ҿ����� ����",
            love = "������ ������ ����, �������� ����",
            career = "���ο� ���� ��ȸ, ������ ����",
            advice = "�������� ����� �����϶�.",
            money = "���ο� ���� ��ȸ, ������ ���� ����.",
            health = "�ǰ��� ���������� ü�� ������."
        },

        ["Two of Pentacles"] = new TarotMeaning
        {
            title = "Two of Pentacles",
            upright = "����, ����, ���� ���� ����",
            reversed = "�ұ���, ȥ��, ����",
            love = "���ֿ� ��Ȱ�� ���� �ʿ�",
            career = "���� ������Ʈ ���� ����",
            advice = "�켱������ ���ϰ� ������ �����.",
            money = "���ԡ����� ���� �ʿ�, �ڱ� juggling.",
            health = "����� �⺹, ��Ȱ ���� ���� �ʿ�."
        },

        ["Three of Pentacles"] = new TarotMeaning
        {
            title = "Three of Pentacles",
            upright = "����, ����ũ, ����� ����",
            reversed = "��ȭ, ���� ����, ���� ����",
            love = "�������� ����, �Բ� ����",
            career = "������Ʈ ����, ������ ����",
            advice = "�Բ� �� �� ������ Ŀ����.",
            money = "�������� ���� ����, ���� ������Ʈ ��.",
            health = "������ ����ǰ� ������ ȿ����."
        },

        ["Four of Pentacles"] = new TarotMeaning
        {
            title = "Four of Pentacles",
            upright = "����, ����, ����, ����",
            reversed = "�ս�, ���� �ؼ�, �Ҿ���",
            love = "�����ϴ� ���, ���� �߱�",
            career = "���� ������, �ڿ� ���",
            advice = "���ƾ� �� ���� ������ ���ƶ�.",
            money = "���ࡤ���� ������, �ڻ� ���.",
            health = "���塤�������� ���� ü�� ����."
        },

        ["Five of Pentacles"] = new TarotMeaning
        {
            title = "Five of Pentacles",
            upright = "����, ��, �ս�, �Ҿ�",
            reversed = "ȸ��, ����, ��ȸ �߰�",
            love = "�ܷο� ����, ������ ����� �� ����",
            career = "���� �ս�, ���� ����",
            advice = "������ ���ϰ� ����� ���� ����.",
            money = "���� �սǡ����, ���� ��û �ʿ�.",
            health = "ü�� ���ϡ�����, �ܷο� ����."
        },

        ["Six of Pentacles"] = new TarotMeaning
        {
            title = "Six of Pentacles",
            upright = "����, ����, ���� ���� ����",
            reversed = "�ұ����� ����, ���Ǻ� ����",
            love = "������ ��Ǯ�� �ٸ� ������ �޴� ����",
            career = "������ �Ŀ�, �����μ�Ƽ��",
            advice = "�ִ� �Ͱ� �޴� ���� ���� �ְ� �϶�.",
            money = "������ ���������󡤱��� ���� ��ȯ.",
            health = "���� �ְ�޴� �ǰ� ȸ��."
        },

        ["Seven of Pentacles"] = new TarotMeaning
        {
            title = "Seven of Pentacles",
            upright = "�γ�, ����� �þ�, ����",
            reversed = "������, ������ ���, ����",
            love = "���踦 ���Ѻ��� �ܰ�",
            career = "���ڡ������ ��� ���",
            advice = "���������� ���� ��ٷ���.",
            money = "��� ���ڡ��γ� �ʿ�, ���� ���.",
            health = "������ ������ ������ ȸ��."
        },

        ["Eight of Pentacles"] = new TarotMeaning
        {
            title = "Eight of Pentacles",
            upright = "����, �ݺ� �Ʒ�, �ڱ���",
            reversed = "�̼�, �Ǽ�, �����",
            love = "���踦 �����ϱ� ���� ���",
            career = "������ ����, ��� ����",
            advice = "������ ���� ����۾ƶ�.",
            money = "��� ���ڡ��γ� �ʿ�, ���� ���.",
            health = "������ ������ ������ ȸ��."
        },

        ["Nine of Pentacles"] = new TarotMeaning
        {
            title = "Nine of Pentacles",
            upright = "ǳ��, ����, ����, ����",
            reversed = "�㼼, ����, ������ �Ҿ�",
            love = "�ڸ����� ����, ������ �ູ",
            career = "����, ������ ǳ��",
            advice = "����� ���븦 ������.",
            money = "������ ǳ�䡤����������.",
            health = "�ɽ� ������������ �����."
        },

        ["Ten of Pentacles"] = new TarotMeaning
        {
            title = "Ten of Pentacles",
            upright = "����, ����, ����, ������ ����",
            reversed = "���� ��ȭ, ��� ����, ����",
            love = "������ ��ȥ������ �߽�",
            career = "����� ��� ����������",
            advice = "������ ������ ������ ���Ѷ�.",
            money = "���������ꡤ����� ���� ����.",
            health = "���� �ǰ��������� ���� �߿�."
        },

        ["Page of Pentacles"] = new TarotMeaning
        {
            title = "Page of Pentacles",
            upright = "�н�, ��ȸ, ������ ����",
            reversed = "������, ����ȹ, �̼���",
            love = "��� ���� ����, ���������� ������ ����",
            career = "�Ʒá����Ͻʡ��н��� ��ȸ",
            advice = "���� ��ȸ�� ������ ����ƶ�.",
            money = "���ο� ���� ��ȸ���н� ���� ���� ����.",
            health = "�ǰ� ���� ���Ρ����� ���� ����."
        },

        ["Knight of Pentacles"] = new TarotMeaning
        {
            title = "Knight of Pentacles",
            upright = "����, ������, å�Ӱ�",
            reversed = "������, ����, ����",
            love = "�������� ������ ����",
            career = "���� �ִ� ���� ó��, å�� �ϼ�",
            advice = "������ �� ������ ���ư���.",
            money = "������ ���� ����, ������ ����.",
            health = "������ ��ƾ����Ģ���� �ǰ� ����."
        },

        ["Queen of Pentacles"] = new TarotMeaning
        {
            title = "Queen of Pentacles",
            upright = "������, ������ ���, ����",
            reversed = "����, ������ ����, �Ҿ���",
            love = "������ ������ ������ ����",
            career = "�ǿ����������� ������",
            advice = "������ ��Ű�� �������� ���� ����.",
            money = "������ ���������� ���� ���� �ɷ�.",
            health = "������ ����, ������ �ǰ� ����."
        },

        ["King of Pentacles"] = new TarotMeaning
        {
            title = "King of Pentacles",
            upright = "��, ����, ����, å��",
            reversed = "Ž��, ����, ����",
            love = "�������̰� å�Ӱ� �ִ� ��Ʈ��",
            career = "������ �����, ������ ����",
            advice = "������ å���� �߽��϶�.",
            money = "�Ρ����� ������������ ����.",
            health = "ưư�� ü�¡������� ��Ȱ ����."
        },
        // Minor Arcana - Swords-------------------------------------------------

        ["Ace of Swords"] = new TarotMeaning
        {
            title = "Ace of Swords",
            upright = "����, ����, ��Ȯ�� �Ǵ�, ����� ����",
            reversed = "ȥ��, �ְ�, �ǻ���� ����",
            love = "������ ��ȭ�� ����",
            career = "�м������� ����, ������ ����",
            advice = "��ǰ� ���� �и��� �߶��.",
            money = "��Ȯ�� �Ǵ����� ���� ��ȸ ����.",
            health = "�ǰ� ������ ���� ����, ġ�� ���� ����."
        },

        ["Two of Swords"] = new TarotMeaning
        {
            title = "Two of Swords",
            upright = "����, ������ ���, ������ �̼��� ��ġ",
            reversed = "���� ȸ�� ���� ����, ����",
            love = "���� ����, ��ȭ �ʿ�",
            career = "���� �̷�� ��, ������ �� ����",
            advice = "���������� ���� �����϶�.",
            money = "���� ���� ����, ������ �� �ʿ�.",
            health = "�ǰ� ���� ���� ����, ���ܡ�ġ�� �̷�."
        },

        ["Three of Swords"] = new TarotMeaning
        {
            title = "Three of Swords",
            upright = "��ó, �Ǹ�, �ﰢ ����, �̺�",
            reversed = "ġ�� ����, ������ ��ȭ",
            love = "��ó ���� ����, �̺������ �̽�",
            career = "������Ʈ ����, ��Ȥ�� �ǵ��",
            advice = "������ �����ϰ� ȸ�� ��ƾ�� �����.",
            money = "���� �սǡ����� ���з� ���� ��ó.",
            health = "�ɸ��� ��Ʈ����, ���塤���� ����."
        },

        ["Four of Swords"] = new TarotMeaning
        {
            title = "Four of Swords",
            upright = "�޽�, ������, ����, ���",
            reversed = "ȸ�� ����, ���ƿ�, ���� �޽�",
            love = "��� �Ÿ��� �ΰ� ����",
            career = "�ް����������á����� ������",
            advice = "���߰� �������� ȸ���϶�.",
            money = "���� ����, ���� �޽ı�.",
            health = "�޽ġ������� �ʿ�, ���ƿ� ����."
        },

        ["Five of Swords"] = new TarotMeaning
        {
            title = "Five of Swords",
            upright = "�¸������� �ջ�, �̱��� ����, �ҽ�",
            reversed = "ȭ�� �õ�, �ս� ����",
            love = "�̱��� �µ��� ��ó ����",
            career = "��ġ �ο�, ���� ���� �º�",
            advice = "�̱�� �ͺ��� ����� ���� ����.",
            money = "������ ����, ���� ���� �ս�.",
            health = "��Ʈ������ ��ȯ, �ڱ� ���� �ʿ�."
        },

        ["Six of Swords"] = new TarotMeaning
        {
            title = "Six of Swords",
            upright = "�̵�, ��ȯ, ���뿡�� ���",
            reversed = "�̷�, �̵� ����, �ҿ����� ��ȯ",
            love = "������ �� �������� �̵�",
            career = "�������� ��ȯ, ȯ�� ��ȭ",
            advice = "������ �� ���� ������ �Űܰ���.",
            money = "���� ��ȯ���̵�, ������ ��������.",
            health = "ġ�ᡤ��Ȱ�� ������ ȸ��."
        },

        ["Seven of Swords"] = new TarotMeaning
        {
            title = "Seven of Swords",
            upright = "������, ����, ����, ���Ӽ�",
            reversed = "����, ���� ���� �ʿ�",
            love = "��С�ȸ�ǡ���ٸ� ���",
            career = "������ �ൿ vs �������� ����",
            advice = "����ºΰ� ������, ������ ������ �к��϶�.",
            money = "������ ���ڡ�����ũ, ��� ����.",
            health = "����� ����, ���� ���� �ʿ�."
        },

        ["Eight of Swords"] = new TarotMeaning
        {
            title = "Eight of Swords",
            upright = "�ڱ� ����, �η���, ����",
            reversed = "�ع�, ���� ��ȯ, ������ ������ ����",
            love = "�η����� ���� �Ÿ�, ������ ���� �ʿ�",
            career = "����/�η��� ����, ���� ������",
            advice = "������ ���� ������ Ǫ�� ��⸦ ����.",
            money = "������ ����, ���̳� ������ ����.",
            health = "������ ����, �Ҿȡ����强 �ǰ� ����."
        },

        ["Nine of Swords"] = new TarotMeaning
        {
            title = "Nine of Swords",
            upright = "�Ҿ�, �Ǹ�, ������ ����",
            reversed = "�Ҿȿ��� ������ ���",
            love = "������ ���踦 ���, ������ ���� �ʿ�",
            career = "�Ǽ�������ũ�� ���� ������ �Ҿ�",
            advice = "��� Ȯ�ΰ� ���顤��ƾ���� �Ҿ��� �����.",
            money = "���� �Ҿȡ�����, �� �� ��� �ñ�.",
            health = "�Ҹ顤�Ҿȡ������� ��Ʈ����."
        },

        ["Ten of Swords"] = new TarotMeaning
        {
            title = "Ten of Swords",
            upright = "������ ��, �ر�, �ٴ�",
            reversed = "�ٴ� ��� �ݵ�, ���",
            love = "������ ����, Ȥ�� ������ ����",
            career = "������Ʈ ����, ���� ��ȯ",
            advice = "���� �����ϰ� �� ������ �غ��϶�.",
            money = "������ �ر��������� �ս�.",
            health = "�ǰ� ������, ��� �ʿ�."
        },

        ["Page of Swords"] = new TarotMeaning
        {
            title = "Page of Swords",
            upright = "ȣ���, �н�, ���� ����, ������ ����",
            reversed = "����, �ǻ��� ����, ����� ��",
            love = "���θ� �� �˾ư��� �ܰ�",
            career = "����ġ���м����ű�� �н�",
            advice = "���� ���ϱ⺸�� ��Ȯ�� �����϶�.",
            money = "���� ���� ���� �������н��� �ܰ�.",
            health = "���ο� �ǰ� ���ġ����� ����."
        },

        ["Knight of Swords"] = new TarotMeaning
        {
            title = "Knight of Swords",
            upright = "����, ����, ���, ���� ���",
            reversed = "����, ����, ����",
            love = "���������� ����, ��ó ���� �ʰ� ����",
            career = "������ ���ࡤ�̽� ����",
            advice = "�ӵ��� ���� �ֺ� ���ظ� �ּ�ȭ�϶�.",
            money = "������ ���ڡ��ܱ��� ����, ������ ����.",
            health = "������ Ȱ�������� �λ� ����."
        },

        ["Queen of Swords"] = new TarotMeaning
        {
            title = "Queen of Swords",
            upright = "�̼��� ����, ���, ��� �߽� ����",
            reversed = "�ü�, ��, ������ ����",
            love = "������ �̼��� ���� ����",
            career = "��Ʈ���м� ��� �ǻ����",
            advice = "�������� ���� �ʵ� ����� ���϶�.",
            money = "���� ������ �̼������� ó��.",
            health = "�ɸ��� �Ÿ��α�, ������ �ڱ� ����."
        },

        ["King of Swords"] = new TarotMeaning
        {
            title = "King of Swords",
            upright = "���� �ִ� ����, ����, ����, ��������",
            reversed = "���� ����, ����, ������",
            love = "������ �̼��� ������",
            career = "��å�������������������� �ǻ����",
            advice = "��Ģ�� �������� �����ϰ� �̲����.",
            money = "���� ��������/��å ��� ����.",
            health = "������ �������� ��� �ǰ� ����."
        },


        // �ʿ� ī����� ��� �߰���
    };

    /// �̸� ���ڿ��� DB Ű�� ����ȭ
    public static string NormalizeName(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return "";
        var n = raw.Trim();

        // ���� ǥ�� ���� ó��: " (R)", "(Reversed)" ��
        n = n.Replace("(Reversed)", "").Replace("(R)", "").Trim();

        // ��ҹ��� ����ȭ
        return n;
    }

    public static bool TryGet(string rawName, out TarotMeaning m)
    {
        var key = NormalizeName(rawName);
        return _dict.TryGetValue(key, out m);
    }

    /// ����: ���� ���� �Ǵ�
    public static bool IsReversed(string rawName)
    {
        if (string.IsNullOrEmpty(rawName)) return false;
        var s = rawName.ToLower();
        return s.Contains("(r)") || s.Contains("reversed");
    }

}
