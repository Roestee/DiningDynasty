using DG.Tweening;
using UnityEngine;

namespace Structure.UI.PanelControllers
{
    public class SlidePanelController : PanelControllerBase
    {
        [Header("Animation")] 
        [SerializeField] private bool isMoveOnX = true;
        [SerializeField] private bool isMoveOnY;
        [SerializeField] private RectTransform animateTf;
        [Space]
        [SerializeField] private float panelOpenTime = 0.4f;
        [SerializeField] private float panelCloseTime = 0.3f;
        [SerializeField] private Vector2 panelOpenPos;
        [SerializeField] private Vector2 panelClosePos;

        protected override void Open(bool instant)
        {
            if(disableOnClose)
                body.gameObject.SetActive(true);
            
            body.interactable = true;
            body.alpha = 1;
            var anchoredPos = animateTf.anchoredPosition;
            if (instant)
            {
                body.blocksRaycasts = true;
                animateTf.anchoredPosition = new Vector2((isMoveOnX ? panelOpenPos.x: anchoredPos.x), (isMoveOnY ? panelOpenPos.y: animateTf.anchoredPosition.y));
                return;
            }
            
            SetActiveTween.Kill();
            SetActiveTween = animateTf.DOAnchorPos(
                    new Vector2((isMoveOnX ? panelOpenPos.x: anchoredPos.x), (isMoveOnY ? panelOpenPos.y: anchoredPos.y)), panelOpenTime)
                .SetEase(Ease.OutBack)
                .OnComplete(()=> body.blocksRaycasts = true);
        }

        protected override void Close(bool instant)
        {
            body.blocksRaycasts = false;
            var anchoredPos = animateTf.anchoredPosition;
            if (instant)
            {
                body.alpha = 0;
                animateTf.anchoredPosition = new Vector2((isMoveOnX ? panelClosePos.x: anchoredPos.x), (isMoveOnY ? panelClosePos.y: anchoredPos.y));
                
                if(disableOnClose)
                    body.gameObject.SetActive(false);
                return;
            }
            
            SetActiveTween.Kill();
            SetActiveTween = animateTf.DOAnchorPos(new Vector2((isMoveOnX ? panelClosePos.x: anchoredPos.x), 
                    (isMoveOnY ? panelClosePos.y: anchoredPos.y)), panelCloseTime).SetEase(Ease.InBack).OnComplete(()=>
            {
                body.alpha = 0;
                
                if(disableOnClose)
                    body.gameObject.SetActive(true);
            });
        }
    }
}