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
    
    private float m_distanceBetweenPlayerCamera;
    
    [SerializeField] private Material m_materialDeathIndicator;
    private float m_deathShaderInitialIntensity;
    [SerializeField] private string m_nameDeathShaderParameters = "_Intensity";

    #endregion

    #region Unity Cycle

    private void Update()
    {
        CheckIfPlayerTooFar();
    }
    
    private void Start()
    {
        if (m_materialDeathIndicator != null)
            m_deathShaderInitialIntensity = m_materialDeathIndicator.GetFloat(m_nameDeathShaderParameters);
    }

    private void OnDestroy()
    {
        if (m_materialDeathIndicator != null)
            m_materialDeathIndicator.SetFloat(m_nameDeathShaderParameters, m_deathShaderInitialIntensity);
    }

    #endregion

    #region Game Management

    private void CheckIfPlayerTooFar()
    {
        m_distanceBetweenPlayerCamera =
            Vector3.Distance(m_cameraObject.transform.position, m_playerObject.transform.position);
        
        float t = m_distanceBetweenPlayerCamera / m_maxDistance;
        
        m_materialDeathIndicator.SetFloat(m_nameDeathShaderParameters, t);
        
        if (!( m_distanceBetweenPlayerCamera > m_maxDistance) || m_gameEnd) return;
        
        
        EventManager.PlayerDeath();
        m_gameEnd = true;
        print("Game Over");

        


    }

    #endregion
}
