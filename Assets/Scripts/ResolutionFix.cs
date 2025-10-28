using UnityEngine;

public class ResolutionFix : MonoBehaviour
{
    void Awake()
    {
        // 해상도 고정 (1080x1920, 풀스크린X)
        Screen.SetResolution(1080, 1920, false);

        // 화면비 유지(옵션)
        Application.runInBackground = true;
    }
}
