using UnityEngine;

public class ResolutionFix : MonoBehaviour
{
    void Awake()
    {
        // �ػ� ���� (1080x1920, Ǯ��ũ��X)
        Screen.SetResolution(1080, 1920, false);

        // ȭ��� ����(�ɼ�)
        Application.runInBackground = true;
    }
}
