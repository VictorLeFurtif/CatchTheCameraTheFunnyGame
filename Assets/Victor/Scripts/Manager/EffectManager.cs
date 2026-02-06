using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EffectManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera cameraMain;
    
    [Header("Settings Fov Effects")]
    [SerializeField] private float fovMax = 80f;
    [SerializeField] private float effectDuration = 1f;
    
    private float m_defaultFov;
    private Coroutine m_currentEffect;
    
    [Header("Post-Process Materials")]
    [SerializeField] private Material m_materialVortex;
    [SerializeField] private Material m_materialAlcool;
    
    private float m_vortexInitialIntensity;
    private float m_alcoolInitialIntensity;
    
    private void Start()
    {
        if (cameraMain == null)
            cameraMain = Camera.main;
            
        m_defaultFov = cameraMain.fieldOfView;
        
        if (m_materialVortex != null)
            m_vortexInitialIntensity = m_materialVortex.GetFloat("_Intensity");
            
        if (m_materialAlcool != null)
            m_alcoolInitialIntensity = m_materialAlcool.GetFloat("_Intensity");
    }
    
    
    private void OnDestroy()
    {
        if (m_materialVortex != null)
            m_materialVortex.SetFloat("_Intensity", m_vortexInitialIntensity);
            
        if (m_materialAlcool != null)
            m_materialAlcool.SetFloat("_Intensity", m_alcoolInitialIntensity);
    }

    #region FOV Effects

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

    #endregion

    #region Inverse controls Effects

    private void ToggleControls() => EventManager.ToggleControls();

    #endregion

    #region Material Intensity Effects

    private IEnumerator ToggleIntensityEffect(float intensity, float duration, float fadeTime, Material material, string nameParameters)
    {
        float elapsedTime = 0;
        
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeTime;
            material.SetFloat(nameParameters, Mathf.Lerp(0, intensity, t));
            yield return null;
        }
        
        material.SetFloat(nameParameters, intensity);
        
        yield return new WaitForSeconds(duration);
        
        elapsedTime = 0;
        
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeTime;
            material.SetFloat(nameParameters, Mathf.Lerp(intensity, 0, t));
            yield return null;
        }
        
        material.SetFloat(nameParameters, 0);
    }

    public void VortexEffect(float intensity = 0.5f, float duration = 5f, float fadeTime = 1f)
    {
        StartCoroutine(ToggleIntensityEffect(intensity, duration, fadeTime, m_materialVortex, "_Intensity"));
    }
    
    public void AlcoolEffect(float intensity = 1f, float duration = 5f, float fadeTime = 1f)
    {
        StartCoroutine(ToggleIntensityEffect(intensity, duration, fadeTime, m_materialAlcool, "_Intensity"));
    }

    #endregion
}