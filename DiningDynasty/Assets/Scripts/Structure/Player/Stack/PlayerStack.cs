using DG.Tweening;
using UnityEngine;

namespace Structure.Player.Stack
{
    public enum PlayerStackType
    {
        None,
        TomatoStack,
        StackType2,
        StackType3,
    }
    
    public class PlayerStack: MonoBehaviour
    {
        [SerializeField] private PlayerStackType stackType;
        [SerializeField] private Transform stackUpTf;
        [Header("Jump")]
        [SerializeField] private float jumpDuration = 0.3f;
        [SerializeField] private float jumpPower = 1f;
        [SerializeField] private float slideTime = 0.2f;

        public PlayerStackType StackType => stackType;
        public float GetHeight() => Mathf.Abs(transform.position.y - stackUpTf.position.y);

        private Sequence _stackSequence;
        private Transform _tf;
        private Rigidbody _rb;
        private Collider _coll;
        private PlayerInteractable _playerInteractable;

        protected virtual void Awake()
        {
            _tf = transform;
            _rb = GetComponent<Rigidbody>();
            _coll = GetComponent<Collider>();
            
            _playerInteractable = GetComponentInChildren<PlayerInteractable>();
            _playerInteractable.OnPlayerInteract += OnPlayerInteract;
        }

        private void OnPlayerInteract(PlayerBase player, bool interacted)
        {
            if(!interacted)
                return;
            
            _playerInteractable.OnPlayerInteract -= OnPlayerInteract;
            _coll.enabled = false;
            _rb.isKinematic = true;
            player.OnInteractWithStack(this);
        }

        public void JumpToCell(StackCell cell)
        {
            _stackSequence.Kill();
            _tf.SetParent(cell.transform);
            
            _stackSequence = DOTween.Sequence();
            _stackSequence.Append(_tf.DOLocalRotateQuaternion(Quaternion.identity, jumpDuration));
            _stackSequence.Join(_tf.DOLocalJump(Vector3.zero, jumpPower, 1, jumpDuration));
        }

        public void MoveToCell(StackCell cell)
        {
            _stackSequence.Kill();
            _tf.SetParent(cell.transform);
            
            _stackSequence = DOTween.Sequence();
            _stackSequence.Append(_tf.DOLocalRotateQuaternion(Quaternion.identity, slideTime));
            _stackSequence.Join(_tf.DOLocalMove(Vector3.zero, slideTime));
        }
    }
}