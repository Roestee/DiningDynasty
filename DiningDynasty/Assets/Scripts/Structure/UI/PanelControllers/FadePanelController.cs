using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Structure.UI.PanelControllers
{
    public class FadePanelController : PanelControllerBase
    {
        [SerializeField] private bool isScale = true;
        [Space]
        [SerializeField] private float panelOpenDuration = 0.4f;
        [SerializeField] private float panelCloseDuration = 0.3f;
        [ShowIf("@isScale")]
        [SerializeField] private float panelCloseScale = 0.6f;
        
        protected override void Open(bool instant)
        {
            if(disableOnClose)
                body.gameObject.SetActive(true);
            
            body.interactable = true;
            if (instant)
            {
                body.alpha = 1;
                body.blocksRaycasts = true;
                
                if(isScale)
                    body.transform.localScale = Vector3.zero;
            }
            else
            {
                SetActiveTween.Kill();
                var sequence = DOTween.Sequence();
                sequence.Append(body.DOFade(1, panelOpenDuration));
                sequence.OnComplete(() => body.blocksRaycasts = true);
                
                if(isScale)
                    sequence.Join(body.transform.DOScale(1, panelOpenDuration));
                
                SetActiveTween = sequence;
            }

            OnOpen();
        }

        protected override void Close(bool instant)
        {
            body.blocksRaycasts = false;
            
            if (instant)
            {
                body.alpha = 0;
                
                if(disableOnClose)
                    body.gameObject.SetActive(false);
                
                if(isScale)
                    body.transform.localScale = panelCloseScale * Vector3.one;
            }
            else
            {
                SetActiveTween.Kill();
                var sequence = DOTween.Sequence();
                sequence.Append(body.DOFade(0, panelCloseDuration));
                
                if(disableOnClose)
                    sequence.OnComplete(() => body.gameObject.SetActive(false));
                
                if(isScale)
                    sequence.Join(body.transform.DOScale(Vector3.one * panelCloseScale, panelCloseDuration));
                
                SetActiveTween = sequence;
            }
            
            OnClose();
        }
    }
}