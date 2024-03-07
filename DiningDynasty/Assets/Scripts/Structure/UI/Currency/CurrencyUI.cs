using DG.Tweening;
using Structure.Managers;
using TMPro;
using UnityEngine;
using Utilities.Helpers;

namespace Structure.UI.Currency
{
    public class CurrencyUI : MonoBehaviour
    {
        [SerializeField] private CurrencyType type;
        [SerializeField] private RectTransform currencyIconTf;
        [SerializeField] private TextMeshProUGUI currencyText; 
        
        [Header("Animation")]
        [SerializeField] private float scaleAmount = 1.1f;
        [SerializeField] private float scaleDuration = 0.1f;
        
        private Tween _iconAnimateTween;
        private int _currentCurrency;

        public CurrencyType CurrencyType => type;
        public Vector2 GetIconPosition() => currencyIconTf.position;
        public void UpdateCurrencyText() => currencyText.text = CurrencyUnitHelper.IntToK(_currentCurrency);

        public void UpdateCurrencyUI()
        {
            _currentCurrency = SaveManager.Instance.GetCurrency(type);
            UpdateCurrencyText();
            
            _iconAnimateTween.Kill();
            
            var sequence = DOTween.Sequence();
            _iconAnimateTween = sequence;
            sequence.Append(currencyIconTf.DOScale(scaleAmount * Vector3.one, scaleDuration));
            sequence.Join(currencyText.transform.DOScale(scaleAmount * Vector3.one, scaleDuration));
            sequence.Append(currencyIconTf.DOScale(1, scaleDuration));
            sequence.Join(currencyText.transform.DOScale(1, scaleDuration));
            sequence.Play();
        }
    }
}