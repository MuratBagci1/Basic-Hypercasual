using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionManager
{
    public static void Initialize()
    {
        Debug.LogFormat("ActionManager Initialized");
    }

    public static Func<int, Color> GetColor;
    public static Action OnTapToPlayPressed;
    public static Action<StackController> OnStackAwaken;
    public static Action<List<Muzzle>> OnMerge;
    public static Action OnMergeWobble;
    public static Action<Muzzle, int> OnBlockValueChanged;
    public static Action<Muzzle> OnBlockRemoved;
    public static Action<Muzzle> OnBlockCollided;
    public static Action OnFinishLine;
    public static Action OnStoppedWalking;
    public static Action OnMultiplierDied;
    public static Func<int> GetFirePower;
    public static Action<int> OnDamageChanged;

    public static Action ReloadGame;
    public static Action OnSuccess;
    public static Action OnFail;
    public static Action<RectTransform> OnTryAgainButtonPressed;
    public static Action<RectTransform> OnPlayAgainButtonPressed;
}