using System;
using Structure.DataStores;
using UnityEngine;

namespace Structure.Utilities.TemplateHelpers
{
    [Serializable]
    public class EditorConfigData
    {
        public string testSceneName;
        public string testScenePath;
    }

    public class EditorConfigManager
    {
        private static EditorConfigData GetConfig()
        {
            return JsonDataStore.GetData<EditorConfigData>(GlobalConsts.EditorConfigFilePath);
        }

        private static void SetConfig(EditorConfigData data)
        {
            JsonDataStore.StoreData(GlobalConsts.EditorConfigFilePath, data);
        }

        public static void SetTestScene(string sceneName, string scenePath)
        {
            Debug.Log("Setting test scene to " + sceneName);

            var config = GetConfig() ?? new EditorConfigData();

            config.testSceneName = sceneName;
            config.testScenePath = scenePath;
            SetConfig(config);
        }

        public static (string sceneName, string scenePath) GetTestScene()
        {
            var config = GetConfig();
            if (config == null)
            {
                return (null, null);
            }
            
            return (config.testSceneName, config.testScenePath);
        }

        public static void ClearTestScene()
        {
            var config = GetConfig() ?? new EditorConfigData();

            config.testSceneName = null;
            config.testScenePath = null;
            SetConfig(config);
        }
    }
}