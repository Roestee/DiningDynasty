using Structure.UI;
using UnityEngine;

namespace Structure.Player.User
{
    public class UserPlayer : PlayerBase
    {
        private JoystickController _joystickController;
        
        public UserMovementController MovementController { get; private set; }
        public UserAnimationController AnimationController { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            MovementController = GetComponent<UserMovementController>();
            AnimationController = GetComponent<UserAnimationController>();
            
            _joystickController = JoystickController.Instance;
        }

        private void Update()
        {
            HandleMovement(Time.deltaTime);
        }

        private void HandleMovement(float delta)
        {
            if(!MovementController.CanMove)
                return;
            
            var input = _joystickController.GetMovementInput();
            var inputVector = new Vector3(input.horizontal, 0, input.vertical);
            AnimationController.SetMovementSpeed(inputVector.sqrMagnitude);
            MovementController.Move(inputVector, delta);
            MovementController.Rotate(inputVector);
        }
    }
}