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
        private float _rotationSpeed = 180f;

        private Rigidbody _rigidbody;
        private Quaternion _rotation = Quaternion.identity;

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
            //Debug.Log("h = " + h + ", v = " + v);
            if ((h != 0f) || (v != 0f))
            {
                float destinationAngle = Mathf.Atan2(h, v) * 180f / Mathf.PI;

                Quaternion destinationQuaternion = Quaternion.Euler(0f, destinationAngle, 0f);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, destinationQuaternion,
                    _rotationSpeed * Time.deltaTime);
            }
        }

        /*private void Update()
        {
            float h = _inputHandler.Direction.x;
            float v = _inputHandler.Direction.y;
            Debug.Log("h = " + h + ", v = " + v);
            transform.Rotate(new Vector3(0f, _rotationSpeed * h, 0f) * Time.deltaTime);
        }*/
    }
}