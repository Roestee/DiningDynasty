using System;
using System.Collections;
using Structure.UI.MainPanels.UIControllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Structure.Managers
{
    public class SceneLoader : MonoBehaviour
    {
        public event Action OnSceneLoadComplete;

        [SerializeField] private LoadingUIController uiController;
        
        public void LoadLevelAsync()
        {
            StartCoroutine(LoadLevelCoroutine());
        }

        private IEnumerator LoadLevelCoroutine()
        {
            var operation = SceneManager.LoadSceneAsync(GetLevelIndex());
            while (!operation.isDone)
            {
                var progress = Mathf.Clamp01(operation.progress / 0.9f);
                uiController.UpdateProgress(progress);
                yield return null;
            }
            
            OnSceneLoadComplete?.Invoke();
        }

        private static int GetLevelIndex()
        {
            var index = SaveManager.Instance.GetCurrentLevelIndex();
            if (index > SceneManager.sceneCountInBuildSettings)
                index = Random.Range(1, SceneManager.sceneCountInBuildSettings);
            
            return index;
        }
    }
}