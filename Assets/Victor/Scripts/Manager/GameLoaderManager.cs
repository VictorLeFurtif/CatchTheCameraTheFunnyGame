using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Victor.Scripts.Enums;

namespace Victor.Scripts.Manager
{
    public class GameLoaderManager : MonoBehaviour
    {
        [Header("Game State")]
        [SerializeField] private GameState currentState = GameState.Menu;
        
        [Header("Scene Names")]
        [SerializeField] private string m_menuSceneName = "Menu";
        [SerializeField] private string m_gameSceneName = "Game";
        
        public static GameLoaderManager Instance;
        
        public static event Action<GameState> OnGameStateChanged;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            InitializeGameState();
        }

        private void OnEnable()
        {
            EventManager.OnPlayerDeath += GameOver;
        }

        private void OnDisable()
        {
            EventManager.OnPlayerDeath -= GameOver;
        }

        private void InitializeGameState()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            
            if (currentSceneName == m_menuSceneName)
                ChangeGameState(GameState.Menu);
            else if (currentSceneName == m_gameSceneName)
                ChangeGameState(GameState.Game);
        }
        
        public void ChangeGameState(GameState newState)
        {
            if (currentState == newState) return;
            
            GameState previousState = currentState;
            currentState = newState;
            
            
            OnGameStateChanged?.Invoke(newState);
        }
        
        public void StartGame()
        {
            LoadGameScene();
        }

        public void GameOver()
        {
            ChangeGameState(GameState.GameOver);
            Time.timeScale = 0f;
        }

        public void RestartGame()
        {
            ChangeGameState(GameState.Game);
            LoadGameScene();
        }
        
        public void ReturnToMenu()
        {
            LoadMenuScene();
            Time.timeScale = 1;
        }
        
        public void LoadMenuScene()
        {
            StartCoroutine(LoadSceneAndChangeState(m_menuSceneName, GameState.Menu));
        }

        public void LoadGameScene()
        {
            Time.timeScale = 1;
            StartCoroutine(LoadSceneAndChangeState(m_gameSceneName, GameState.Game));
        }

        private IEnumerator LoadSceneAndChangeState(string sceneName, GameState newState)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            
            while (asyncLoad is { isDone: false })
            {
                yield return null;
            }
            
            ChangeGameState(newState);
        }
    }
}
