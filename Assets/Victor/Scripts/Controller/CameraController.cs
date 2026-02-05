using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float m_moveSpeed = 5f;
    
    [SerializeField] private CameraDirectionState m_cameraDirectionState = CameraDirectionState.None;
    
    #endregion

    #region Unity LifeCycle

    private void FixedUpdate()
    {
        CameraMovement();
    }

    #endregion

    #region Camera Movement

    private void CameraMovement()
    {
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

    #endregion
}
