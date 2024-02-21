using System;
using Sirenix.OdinInspector;
using Structure.Singleton;

namespace Structure.Managers
{
    public enum CurrencyType
    {
        None,
        Money,
    }
    
    public class CurrencyController : SingletonMonoBehaviour<CurrencyController>
    {
        public static event Action<CurrencyType, int> OnCurrencyChange;
        
        [Button("Add Currency")]
        public void AddCurrency(CurrencyType type = CurrencyType.Money, int value = 1000)
        {
            var currentCurrency = value + SaveManager.Instance.GetCurrency(type);
            SaveManager.Instance.SetCurrency(type, currentCurrency);
            OnCurrencyChange?.Invoke(type, currentCurrency);
        }

        public bool TryCharge(CurrencyType type, int amount)
        {
            var currency = SaveManager.Instance.GetCurrency(type);
            if (amount > currency) 
                return false;
            
            AddCurrency(type, -amount);
            return true;
        }

        public bool CanCharge(CurrencyType type, int amount) => SaveManager.Instance.GetCurrency(type) >= amount;
    }
}