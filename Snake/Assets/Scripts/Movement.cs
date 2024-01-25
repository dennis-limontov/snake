using UnityEngine;

namespace Snake
{
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private InputHandler _inputHandler;

        [SerializeField]
        private float _movementSpeed = 5f;

        [SerializeField]
        private float _rotationSpeed = 90f;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _movementSpeed * transform.forward;
            //_snakeHeadRb.AddForce(_snakeSpeed * transform.forward, ForceMode.Force);
        }

        private void Update()
        {
            float h = _inputHandler.Direction.x; 
            float v = _inputHandler.Direction.y;
            if ((h != 0f) || (v != 0f))
            {
                float destinationAngle = Mathf.Atan2(h, v) * 180f / Mathf.PI;
                Quaternion destinationQuaternion = Quaternion.Euler(0f, destinationAngle, 0f);
                //if (Mathf.Abs(transform.localEulerAngles.y - _snakeDestinationAngle) > 0.1f)
                //{
                    //float sign = (_snakeDestinationAngle > transform.localEulerAngles.y) ? 1f : -1f;
                    //transform.Rotate(0f, _snakeRotationSpeed * sign * Time.deltaTime, 0f);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation,
                        destinationQuaternion, _rotationSpeed * Time.deltaTime);
                    //transform.localEulerAngles = new Vector3(0f, _snakeDestinationAngle, 0f);
                //}
            }
        }
    }
}