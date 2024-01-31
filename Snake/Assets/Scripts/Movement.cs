using UnityEngine;

namespace Snake
{
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private SnakeHead _snakeHead;

        [SerializeField]
        private float _movementSpeed = 6f;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _movementSpeed * _snakeHead.transform.forward;
        }
    }
}