// CardPickerBridge.cs
using System;
using System.Collections.Generic;
using UnityEngine;

public static class CardPickerBridge
{
    // ��� �ݹ�: (���� ī�� �̹��� 3��, �̸� 3��)
    public static Action<List<Sprite>, List<string>> OnPicked;
}
