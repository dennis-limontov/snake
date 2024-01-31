using UnityEngine;

namespace Snake
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField]
        private InputHandler _inputHandler;

        [SerializeField]
        private float _rotationSpeed = 300f;

        private void Update()
        {
            float h = _inputHandler.Direction.x;
            float v = _inputHandler.Direction.y;
            if ((h != 0f) || (v != 0f))
            {
                float destinationAngle = Mathf.Atan2(h, v) * 180f / Mathf.PI;
                Quaternion destinationQuaternion = Quaternion.Euler(0f, destinationAngle, 0f);
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation,
                    destinationQuaternion, _rotationSpeed * Time.deltaTime);
            }
        }
    }
}
