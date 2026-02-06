using System;
using UnityEngine;

public static class EventManager 
{
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerWin;
    public static event Action OnToggleControls;

    public static event Action OnFovEffect;
    public static event Action OnAlcoolEffect;
    public static event Action OnVortexEffect;

    public static void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public static void PlayerWin()
    {
        OnPlayerWin?.Invoke();
    }

    public static void ToggleControls()
    {
        OnToggleControls?.Invoke();
    }
    
    public static void FovEffect()
    {
        OnFovEffect?.Invoke();
    }

    public static void AlcoolEffect()
    {
        OnAlcoolEffect?.Invoke();
    }

    public static void VortexEffect()
    {
        OnVortexEffect?.Invoke();
    }
}
