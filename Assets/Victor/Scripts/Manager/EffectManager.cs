using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera cameraMain;
    
    [Header("Settings Fov Effects")]
    [SerializeField] private float fovMax = 80f;
    [SerializeField] private float effectDuration = 1f;
    
    private float m_defaultFov;
    private Coroutine m_currentEffect;
    
    private void Start()
    {
        if (cameraMain == null)
            cameraMain = Camera.main;
            
        m_defaultFov = cameraMain.fieldOfView;
    }
    
    public void FovEffect()
    {
        if (m_currentEffect != null)
            StopCoroutine(m_currentEffect);
            
        m_currentEffect = StartCoroutine(IEFovEffect(effectDuration));
    }

    private IEnumerator IEFovEffect(float targetTime)
    {
        float elapsedTime = 0;
        float halfTime = targetTime / 2f;
        
        while (elapsedTime < halfTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfTime;
            cameraMain.fieldOfView = Mathf.Lerp(m_defaultFov, fovMax, t);
            yield return null;
        }
        
        elapsedTime = 0;
        
        while (elapsedTime < halfTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfTime;
            cameraMain.fieldOfView = Mathf.Lerp(fovMax, m_defaultFov, t);
            yield return null;
        }
        
        cameraMain.fieldOfView = m_defaultFov;
        m_currentEffect = null;
    }
}