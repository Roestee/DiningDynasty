using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Structure.UI.MainPanels.UIControllers
{
    public class LoadingUIController : UIControllerBase
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI perText;

        private void SetPerText(float value) => perText.text = $"Loading... {value * 100:0}%";

        public void UpdateProgress(float per)
        {
            SetPerText(per);
            fillImage.fillAmount = per;
        }
    }
}