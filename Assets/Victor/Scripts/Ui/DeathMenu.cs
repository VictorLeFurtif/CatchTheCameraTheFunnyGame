using System;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_playerDeathMenu;
    [SerializeField] private GameObject m_playerWinMenu;
    
    private void OnEnable()
    {
        EventManager.OnPlayerDeath += ActivePanelDeathMenu;
        EventManager.OnPlayerWin += ActivePanelWonMenu;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= ActivePanelDeathMenu;
        EventManager.OnPlayerWin -= ActivePanelWonMenu;
    }

    private void Awake()
    {
        m_playerDeathMenu.SetActive(false);
        m_playerWinMenu.SetActive(false);
    }

    private void ActivePanelDeathMenu() => m_playerDeathMenu.SetActive(true);
    private void ActivePanelWonMenu() => m_playerWinMenu.SetActive(true);
}
