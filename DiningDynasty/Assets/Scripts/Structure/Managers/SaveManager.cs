using Project.MeshOpener;
using Structure.Singleton;
using UnityEngine;

namespace Structure.Managers
{
    public enum SaveType
    {
        PlayerPrefs,
        Json
    }
    
    public class SaveManager : SingletonMonoBehaviour<SaveManager>
    {
        [SerializeField] private SaveType level = SaveType.PlayerPrefs;
        [SerializeField] private SaveType currency = SaveType.PlayerPrefs;
        [SerializeField] private SaveType mesh = SaveType.PlayerPrefs;
        
        public int GetInt(string key, int defaultValue = 0, SaveType saveType = SaveType.PlayerPrefs)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public void SetInt(string key, int value, SaveType saveType = SaveType.PlayerPrefs)
        {
            switch (saveType)
            {
                case SaveType.PlayerPrefs:
                    PlayerPrefs.SetInt(key, value);
                    break;
            }
        }

        #region Level

        public int GetCurrentLevelIndex()
        {
            return GetInt(GlobalConsts.LevelKey, 1, level);
        }

        public void IncreaseLevelIndex(int value = 1)
        {
            var currentLevel = GetCurrentLevelIndex() + value;
            SetInt(GlobalConsts.LevelKey, currentLevel, level);
        }

        #endregion

        #region Currency

        private string GetCurrencyKey(CurrencyType type) => $"Currency_{type.ToString()}";

        public int GetCurrency(CurrencyType type)
        {
            return GetInt(GetCurrencyKey(type), 0, currency);
        }

        public void SetCurrency(CurrencyType type, int amount)
        {
            SetInt(GetCurrencyKey(type), amount, currency);
        }

        #endregion

        #region Mesh Open

        private string MeshRequiredAmountKey(MeshOpenerType type, int id, string specialType) => $"Mesh_{type}_{id}_{specialType}";

        public int GetMeshRequiredAmount(MeshOpenerType type, int id, string specialType)
        {
            return GetInt(MeshRequiredAmountKey(type, id, specialType), -1, mesh);
        }

        public void SetMeshRequiredAmount(MeshOpenerType type, int id, string specialType, int value)
        {
            SetInt(MeshRequiredAmountKey(type, id, specialType), value, mesh);
        }

        #endregion

    }
}