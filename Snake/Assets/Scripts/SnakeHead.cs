using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class SnakeHead : MonoBehaviour
    {
        [SerializeField]
        private GameObject _snakeBodyPart;

        [SerializeField]
        private float _snakeBodyDistance = 0.6f;

        [SerializeField]
        private uint _snakeStartLength = 1;

        private readonly List<GameObject> _snakeBodyParts = new List<GameObject>();

        private readonly List<(Vector3 pos, Quaternion rot)> _snakeWaypoints
            = new List<(Vector3 pos, Quaternion rot)>();

        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, _snakeWaypoints[0].pos) > _snakeBodyDistance)
            {
                _snakeWaypoints[^1] = _snakeWaypoints[^2];
                for (int i = _snakeBodyParts.Count - 1; i > 0; i--)
                {
                    _snakeWaypoints[i] = _snakeWaypoints[i - 1];
                    _snakeBodyParts[i].transform.SetPositionAndRotation(_snakeWaypoints[i].pos,
                        _snakeWaypoints[i].rot);
                }
                _snakeWaypoints[0] = (transform.position, transform.rotation);
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

        private void Start()
        {
            _snakeBodyParts.Add(gameObject);
            _snakeWaypoints.Add((transform.position, transform.rotation));
            _snakeWaypoints.Add((transform.position, transform.rotation));

            for (int i = 0; i < _snakeStartLength; i++)
            {
                AddSnakeBodyPart();
            }
        }

        private void AddSnakeBodyPart()
        {
            GameObject snakeNewPart = Instantiate(_snakeBodyPart, transform.parent);
            _snakeBodyParts.Add(snakeNewPart);
            snakeNewPart.transform.SetPositionAndRotation(_snakeWaypoints[^1].pos, _snakeWaypoints[^1].rot);
            _snakeWaypoints.Add((snakeNewPart.transform.position, snakeNewPart.transform.rotation));
        }
    }
}