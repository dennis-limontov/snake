using UnityEngine;

namespace Snake
{
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private InputHandler _inputHandler;

        [SerializeField]
        private float _snakeSpeed;

        [SerializeField]
        private float _snakeRotationSpeed;

        private Rigidbody _snakeHeadRb;

        private float _snakeDestinationAngle;

        private Quaternion _snakeQuaternion;

        private void Start()
        {
            _snakeHeadRb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _snakeHeadRb.velocity = _snakeSpeed * transform.forward;
            //_snakeHeadRb.AddForce(_snakeSpeed * transform.forward, ForceMode.Force);
        }

        private void Update()
        {
            float h = _inputHandler.Direction.x; 
            float v = _inputHandler.Direction.y;
            if ((h == 0f) && (v == 0f))
            {
                /*Vector3 chapa, chipi = transform.localEulerAngles;
                if (chipi.y > 180f)
                {
                    chapa = new Vector3(0f, 359.999f, 0f);
                }
                else
                {
                    chapa = Vector3.zero;
                }
                transform.localEulerAngles = Vector3.Slerp(chipi, chapa, Time.deltaTime * 2f);*/
            }
            else
            {
                _snakeDestinationAngle = Mathf.Atan2(h, v) * 180f / Mathf.PI;

                //if (Mathf.Abs(transform.localEulerAngles.y - _snakeDestinationAngle) > 0.1f)
                //{
                    //float sign = (_snakeDestinationAngle > transform.localEulerAngles.y) ? 1f : -1f;
                    //transform.Rotate(0f, _snakeRotationSpeed * sign * Time.deltaTime, 0f);
                    _snakeQuaternion = Quaternion.Euler(0f, _snakeDestinationAngle, 0f);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation,
                        _snakeQuaternion, _snakeRotationSpeed * Time.deltaTime);
                    //transform.localEulerAngles = new Vector3(0f, _snakeDestinationAngle, 0f);
                //}
            }
        }
    }
}