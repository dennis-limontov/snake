using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class SnakeHead : MonoBehaviour
    {
        [SerializeField]
        private GameObject _snakeBodyPart;

        [SerializeField]
        private uint _snakeStartLength;

        private List<GameObject> _snakeBodyParts = new List<GameObject>();
        public int SnakeLength => _snakeBodyParts.Count;

        private void Start()
        {
            _snakeBodyParts.Add(gameObject);

            for (int i = 1; i < _snakeStartLength; i++)
            {
                AddSnakeBodyPart();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Food food))
            {
                Food.OnFoodEaten?.Invoke(food);
                AddSnakeBodyPart();
            }
        }

        private void AddSnakeBodyPart()
        {
            GameObject snakeNewPart = Instantiate(_snakeBodyPart, transform.parent);
            Transform lastSnakeBodyTransform = _snakeBodyParts[^1].transform;
            _snakeBodyParts.Add(snakeNewPart);
            snakeNewPart.transform.SetPositionAndRotation(lastSnakeBodyTransform.position,
                lastSnakeBodyTransform.rotation);
            snakeNewPart.GetComponent<Joint>().connectedBody = lastSnakeBodyTransform.GetComponent<Rigidbody>();
        }
    }
}