using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float m_moveSpeed = 5f;
    
    [SerializeField] private CameraDirectionState m_cameraDirectionState = CameraDirectionState.None;

    private bool m_move = true;
    
    #endregion

    #region Unity LifeCycle

    private void Update()
    {
        CameraMovement();
    }

    private void OnEnable()
    {
        EventManager.OnPlayerWin += CancelMovement;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerWin -= CancelMovement;
    }

    #endregion

    #region Camera Movement

    private void CameraMovement()
    {
        if (!m_move) return;
            
        switch (m_cameraDirectionState)
        {
            case CameraDirectionState.X:
                transform.position += new Vector3(m_moveSpeed * Time.deltaTime,0,0);
                break;
            case CameraDirectionState.Y:
                transform.position += new Vector3(0,m_moveSpeed * Time.deltaTime,0);
                break;
            case CameraDirectionState.Z:
                transform.position += new Vector3(0,0,m_moveSpeed * Time.deltaTime);
                break;
            case CameraDirectionState.None:
                throw new ArgumentException("Error you didnt select an axe");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CancelMovement()
    {
        m_move = false;
    }
    #endregion
}
