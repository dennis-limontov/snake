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
            if (other.GetComponent<Food>())
            {
                Destroy(other.gameObject);
                Food.OnFoodEaten?.Invoke();
                //AddSnakeBodyPart();
            }
        }

        private void AddSnakeBodyPart()
        {
            GameObject snakeNewPart = Instantiate(_snakeBodyPart, transform.parent);
            snakeNewPart.transform.SetPositionAndRotation(_snakeBodyParts[^1].transform.position,
                _snakeBodyParts[^1].transform.rotation);
            _snakeBodyParts.Add(snakeNewPart);
            snakeNewPart.GetComponent<HingeJoint>().connectedBody = _snakeBodyParts[^2].GetComponent<Rigidbody>();
        }
    }
}