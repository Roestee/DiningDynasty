using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Structure.UI.PanelControllers
{
    public abstract class PanelControllerBase : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup body;
        [SerializeField] protected bool disableOnClose;
        [SerializeField] private bool isActiveOnStart;

        protected Tween SetActiveTween;

        protected abstract void Open(bool instant);

        protected abstract void Close(bool instant);

        protected virtual void Start()
        {
            SetActive(isActiveOnStart, true);
        }

        [Button("Set Active Test")]
        public void SetActive(bool on, bool instant = false)
        {
            if (on)
            {
                Open(instant);
                OnOpen();
                return;
            }
            
            Close(instant);
            OnClose();
        }

        protected virtual void OnOpen()
        {
            
        }

        protected virtual void OnClose()
        {
            
        }
    }
}