using System;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_playerDeathMenu;
    
    private void OnEnable()
    {
        EventManager.OnPlayerDeath += ActivePanelDeathMenu;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= ActivePanelDeathMenu;
    }

    private void Awake()
    {
        m_playerDeathMenu.SetActive(false);
    }

    private void ActivePanelDeathMenu() => m_playerDeathMenu.SetActive(true);
}
