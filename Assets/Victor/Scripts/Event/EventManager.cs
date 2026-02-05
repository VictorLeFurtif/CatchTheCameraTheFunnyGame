using System;
using UnityEngine;

public static class EventManager 
{
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerSpawn;
    public static event Action OnToggleControls;

    public static void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public static void ToggleControls()
    {
        OnToggleControls?.Invoke();
    }
}
