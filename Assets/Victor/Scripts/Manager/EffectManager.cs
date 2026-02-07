using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class EffectManager : MonoBehaviour
{

    #region Variable

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
    
    [Header("Settings Vortex Effects")]
    [SerializeField] private float m_intensityVortex = 0.5f; 
    [SerializeField] private float m_durationVortex = 5;
    [SerializeField] private float m_fadeVortex = 1;
    [SerializeField] private string m_nameVortexParameters = "_Intensity";
    
    [Header("Settings Alcool Effects")]
    [SerializeField] private float m_intensityAlcool = 1f; 
    [SerializeField] private float m_durationAlcool = 5;
    [SerializeField] private float m_fadeAlcool = 1;
    [SerializeField] private string m_nameAlcoolParameters = "_Intensity";
    
    [Header("Settings Inverse Controls ")]
    [SerializeField] private float m_minTimeInverseControls = 0f;
    [SerializeField] private float m_maxTimeInverseControls = 0f;
    private float timerInverseControls = 0f;
    private float timerInverseControlsMax = 0f;
    
    [Header("Set UI ")]
    [SerializeField] private GameObject alcool;
    [SerializeField] private GameObject canabis;
    [SerializeField] private GameObject champignon;
    
    [Header("Set Sounds")]
    [SerializeField] private string SFX_alcool;
    [SerializeField] private string SFX_canabis;
    [SerializeField] private string SFX_champignon;
    

    #endregion
    
    #region Unity Lifecycle

    private void Update()
    {
        TimerInverseControls();
    }

    private void Start()
    {
        if (cameraMain == null)
            cameraMain = Camera.main;
            
        m_defaultFov = cameraMain.fieldOfView;
        
        if (m_materialVortex != null)
            m_vortexInitialIntensity = m_materialVortex.GetFloat("_Intensity");
            
        if (m_materialAlcool != null)
            m_alcoolInitialIntensity = m_materialAlcool.GetFloat("_Intensity");
        
        timerInverseControlsMax = Random.Range(m_minTimeInverseControls, m_maxTimeInverseControls);
    }
    
    private void OnDestroy()
    {
        if (m_materialVortex != null)
            m_materialVortex.SetFloat("_Intensity", m_vortexInitialIntensity);
            
        if (m_materialAlcool != null)
            m_materialAlcool.SetFloat("_Intensity", m_alcoolInitialIntensity);
    }

    private void OnEnable()
    {
        EventManager.OnAlcoolEffect += AlcoolEffect;
        EventManager.OnFovEffect += FovEffect;
        EventManager.OnVortexEffect += VortexEffect;
    }

    private void OnDisable()
    {
        EventManager.OnAlcoolEffect -= AlcoolEffect;
        EventManager.OnFovEffect -= FovEffect;
        EventManager.OnVortexEffect -= VortexEffect;
    }

    #endregion

    #region FOV Effects

    public void FovEffect()
    {
        AudioManager.instance.Play(SFX_alcool);
        if (m_currentEffect != null)
            StopCoroutine(m_currentEffect);
            
        m_currentEffect = StartCoroutine(IEFovEffect(effectDuration, alcool));
    }

    private IEnumerator IEFovEffect(float targetTime, GameObject ui)
    {
        ui.SetActive(true);
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
        ui.SetActive(false);
    }

    #endregion

    #region Inverse controls Effects

    private void ToggleControls() => EventManager.ToggleControls();

    #endregion

    #region Material Intensity Effects

    private IEnumerator ToggleIntensityEffect(float intensity, float duration, float fadeTime, Material material, string nameParameters, GameObject ui)
    {
        float elapsedTime = 0;
        ui.SetActive(true);
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
        ui.SetActive(false);
        
        material.SetFloat(nameParameters, 0);
    }

    public void VortexEffect()
    {
        AudioManager.instance.Play(SFX_champignon);
        StartCoroutine(ToggleIntensityEffect(m_intensityVortex,
            m_durationVortex, m_fadeVortex,m_materialVortex,m_nameVortexParameters, champignon));
    }
    
    public void AlcoolEffect()
    {
        AudioManager.instance.Play(SFX_canabis);
        StartCoroutine(ToggleIntensityEffect(m_intensityAlcool,
            m_durationAlcool, m_fadeAlcool, m_materialAlcool,m_nameAlcoolParameters, canabis));
    }

    #endregion

    #region Timer Inverse Controls

    private void TimerInverseControls()
    {
        if (timerInverseControls > timerInverseControlsMax)
        {
            timerInverseControls = 0;
            EventManager.ToggleControls();
        }
        else
        {
            timerInverseControls +=  Time.deltaTime;
        }
    }

    #endregion
}