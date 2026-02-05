using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject m_cameraObject;
    
    [SerializeField] private GameObject m_playerObject;
    
    [SerializeField] private float m_maxDistance;

    private float m_timerCheckDeath;

    private bool m_gameEnd = false;

    #endregion

    #region Unity Cycle

    private void Update()
    {
        CheckIfPlayerTooFar();
    }

    #endregion

    #region Game Management

    private void CheckIfPlayerTooFar()
    {
        if (!(Vector3.Distance(m_cameraObject.transform.position, m_playerObject.transform.position) > m_maxDistance)
            || m_gameEnd) return;
        
        
        EventManager.PlayerDeath();
        m_gameEnd = true;
        print("Game Over");


        /*if (m_timerCheckDeath > 1)
        {
            m_timerCheckDeath = 0;
            
            
        }
        else
        {
            m_timerCheckDeath +=  Time.deltaTime;
        }*/


    }

    #endregion
}
