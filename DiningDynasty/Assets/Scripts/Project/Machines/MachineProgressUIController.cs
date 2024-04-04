using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Machines
{
    public class MachineProgressUIController : MonoBehaviour
    {
        [SerializeField] private Image progressFillImage;
        [SerializeField] private CanvasGroup body;

        private Sequence _progressSequence;

        private void Awake()
        {
            progressFillImage.fillAmount = 0;
            body.alpha = 0;
        }

        public void StartProgress(float duration)
        {
            progressFillImage.fillAmount = 0;
            
            _progressSequence.Kill();
            _progressSequence = DOTween.Sequence();
            _progressSequence.Append(body.DOFade(1, 0.1f));
            _progressSequence.Join(progressFillImage.DOFillAmount(1, duration));
            _progressSequence.OnComplete(() => body.alpha = 0);
        }
    }
}