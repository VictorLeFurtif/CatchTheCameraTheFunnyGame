using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject m_cameraObject;
    
    [SerializeField] private GameObject m_playerObject;
    
    [SerializeField] private float m_maxDistance;
    [SerializeField] private float m_minDistance;

    private float m_timerCheckDeath;

    private bool m_gameEnd = false;
    
    private float m_distanceBetweenPlayerCamera;
    
    [SerializeField] private Material m_materialDeathIndicator;
    private float m_deathShaderInitialIntensity;
    [SerializeField] private string m_nameDeathShaderParameters = "_Intensity";
    [SerializeField] private float m_endLevelZ;


    #endregion

    #region Unity Cycle

    private void Update()
    {
        CheckPlayerWinAndLoose();
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

    private void CheckPlayerWinAndLoose()
    {
        if (m_gameEnd) return;

        m_distanceBetweenPlayerCamera =
            Vector3.Distance(m_cameraObject.transform.position, m_playerObject.transform.position);

        float t = m_distanceBetweenPlayerCamera / m_maxDistance;
        m_materialDeathIndicator.SetFloat(m_nameDeathShaderParameters, t);

        if (m_distanceBetweenPlayerCamera <= m_minDistance)
        {
            EventManager.PlayerWin();
            m_gameEnd = true;
            return;
        }

        if (m_distanceBetweenPlayerCamera > m_maxDistance)
        {
            EventManager.PlayerDeath();
            m_gameEnd = true;
            return;
        }

        if (m_cameraObject.transform.position.z >= m_endLevelZ)
        {
            EventManager.PlayerDeath();
            m_gameEnd = true;
            return;
        }
    }


    #endregion
}
