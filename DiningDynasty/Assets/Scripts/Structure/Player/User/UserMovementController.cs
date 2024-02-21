using UnityEngine;

namespace Structure.Player.User
{
    public class UserMovementController : PlayerMovementBase
    {
        [SerializeField] private PlayerMovementType movementType = PlayerMovementType.Idle;
        [SerializeField] private Transform rotateTf;
        [Header("Ground Check")]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float groundCheckRadius = 0.2f;
        
        private CharacterController _characterController;
        private float _gravity;

        private bool IsGrounded() => Physics.CheckSphere(transform.position, groundCheckRadius, groundMask);
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _gravity = Physics.gravity.y;
        }

        public override void Move(Vector3 input, float delta)
        {
            var movementInput = input;
            if(movementType == PlayerMovementType.Runner)
                movementInput.z = 1;
            else
                movementInput.Normalize();

            if (!IsGrounded())
                movementInput.y = _gravity;

            _characterController.Move(movementSpeed * Time.deltaTime * movementInput);
        }

        public void Rotate(Vector3 input)
        {
            if(input == Vector3.zero)
                return;
            
            if(movementType == PlayerMovementType.Runner)
                input.z = 1;
            
            rotateTf.rotation = Quaternion.LookRotation(input.normalized);
        }

#if UNITY_EDITOR
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
            Gizmos.color = Color.white;
        }
        
#endif
    }
}