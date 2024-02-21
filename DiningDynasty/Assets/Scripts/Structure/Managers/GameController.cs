using System;
using Sirenix.OdinInspector;
using Structure.Singleton;
using UnityEngine;

namespace Structure.Managers
{
    public enum GameState
    {
        Loading,
        Menu,
        Game,
        GameOver,
        LevelComplete
    }
    
    public class GameController : SingletonMonoBehaviour<GameController>
    {
        public static event Action<GameState> OnGameStateChange;

        [SerializeField] private GameState startState = GameState.Menu;

        private SceneLoader _sceneLoader;
        
        public GameState CurrentState { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            _sceneLoader = GetComponentInChildren<SceneLoader>(true);
            _sceneLoader.OnSceneLoadComplete += () => SetState(startState);
        }

        private void Start()
        {
            LoadLevel();
        }

        [Button("Set State")]
        public void SetState(GameState state)
        {
            CurrentState = state;
            OnGameStateChange?.Invoke(state);
        }

        public void LoadLevel()
        {
            SetState(GameState.Loading);
            _sceneLoader.LoadLevelAsync();
        }
    }
}

