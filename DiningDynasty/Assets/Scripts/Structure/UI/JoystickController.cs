using Structure.Singleton;
using UnityEngine;

namespace Structure.UI
{
    public class JoystickController : SingletonMonoBehaviour<JoystickController>
    {
        [SerializeField] private Joystick joystick;

        public (float horizontal, float vertical) GetMovementInput() => (joystick.Horizontal, joystick.Vertical);
    }
}