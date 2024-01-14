using UnityEngine;

namespace Snake
{
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private InputHandler _inputHandler;

        [SerializeField]
        private float _snakePower;

        [SerializeField]
        private float _snakeRotationSpeed;

        private Rigidbody _snakeHeadRb;
        private SnakeHead _snake;

        private void Start()
        {
            _snakeHeadRb = GetComponent<Rigidbody>();
            _snake = _snakeHeadRb.GetComponent<SnakeHead>();
        }

        private void FixedUpdate()
        {
            Vector2 direction = _snakePower * _snake.SnakeLength * _inputHandler.Direction;
            _snakeHeadRb.AddForce(direction.x, 0f, direction.y, ForceMode.Force);
            //transform.Rotate(_inputHandler.Direction * _snakeRotationSpeed);
        }

        private void Update()
        {
            float h = _inputHandler.Direction.x; 
            float v = _inputHandler.Direction.y;
            if ((h == 0f) && (v == 0f))
            {
                Vector3 chapa, chipi = transform.localEulerAngles;
                if (chipi.y > 180f)
                {
                    chapa = new Vector3(0f, 359.999f, 0f);
                }
                else
                {
                    chapa = Vector3.zero;
                }
                transform.localEulerAngles = Vector3.Slerp(chipi, chapa, Time.deltaTime * 2f);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0f, Mathf.Atan2(h, v) * 180f / Mathf.PI, 0f);
            }
        }
    }
}