using System;
using UnityEngine;
using UnityEngine.UI;
using Victor.Scripts.Enums;
using Victor.Scripts.Manager;

public class ButtonAction : MonoBehaviour
{
    [SerializeField] private Button m_button;
    [SerializeField] private ButtonType m_buttonType;

    private void Start()
    {
        m_button.onClick.AddListener(Action);
    }

    private void Action()
    {
        switch (m_buttonType)
        {
            case ButtonType.Play:
            case ButtonType.Retry:
                GameLoaderManager.Instance.LoadGameScene();
                break;
            case ButtonType.Quit:
                Application.Quit();
                break;
            case ButtonType.ReturnMenu:
                GameLoaderManager.Instance.LoadMenuScene();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
