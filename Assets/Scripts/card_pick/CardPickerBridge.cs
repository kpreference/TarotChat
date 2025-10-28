// CardPickerBridge.cs
using System;
using System.Collections.Generic;
using UnityEngine;

public static class CardPickerBridge
{
    // 결과 콜백: (뽑힌 카드 이미지 3장, 이름 3개)
    public static Action<List<Sprite>, List<string>> OnPicked;
}
