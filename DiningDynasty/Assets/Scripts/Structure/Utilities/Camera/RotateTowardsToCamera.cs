using UnityEngine;
using Utilities.Helpers;

namespace Structure.Utilities.Camera
{
    public class RotateTowardsToCamera : MonoBehaviour
    {
        [SerializeField] private bool onlyRotateOnStart = true;

        private Transform _myTransform;

        private void Start()
        {
            _myTransform = transform;
            Rotate();
        }

        private void Update()
        {
            if(onlyRotateOnStart)
                return;
            
            ApplyRotation();
        }
        
        private void Rotate()
        {
            var eulerAngle = _myTransform.eulerAngles;
            eulerAngle.x = GeneralHelpers.Camera.transform.eulerAngles.x;
            _myTransform.eulerAngles = eulerAngle;
        }

        private void ApplyRotation()
        {
            _myTransform.LookAt(GeneralHelpers.Camera.transform.position); 
        }
    }
}