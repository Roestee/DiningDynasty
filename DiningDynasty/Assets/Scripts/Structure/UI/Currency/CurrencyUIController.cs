using System.Linq;
using Sirenix.Utilities;
using Structure.Managers;
using UnityEngine;

namespace Structure.UI.Currency
{
    public class CurrencyUIController : MonoBehaviour
    {
        [SerializeField] private CurrencyUI[] currencyUIs;
        
        public CurrencyUI GetCurrencyUI(CurrencyType type) => currencyUIs.FirstOrDefault(p => p.CurrencyType == type);

        private void Start()
        {
            currencyUIs.ForEach(p => p.UpdateCurrencyUI());
        }

        private void OnCurrencyChange(CurrencyType type, int currency)
        {
            GetCurrencyUI(type).UpdateCurrencyUI();
        }

        private void OnEnable()
        {
            CurrencyController.OnCurrencyChange += OnCurrencyChange;
        }

        private void OnDisable()
        {
            CurrencyController.OnCurrencyChange -= OnCurrencyChange;
        }
    }
}