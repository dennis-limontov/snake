using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Snake
{
    public class InputHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public Vector2 Direction => _direction;

        [SerializeField]
        private Image _knob;

        [SerializeField]
        private float _maxDistance;

        private Vector2 _startPosition;
        private Vector2 _direction;

        private void Awake()
        {
            _knob.enabled = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _startPosition = eventData.position;
            _knob.transform.position = _startPosition;
            _knob.enabled = true;
            _direction = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var delta = eventData.position - _startPosition;

            _knob.transform.position = (delta.magnitude < _maxDistance)
                ? eventData.position
                : _startPosition + delta.normalized * _maxDistance;

            _direction = ((Vector2)_knob.transform.position - _startPosition) / _maxDistance;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _knob.enabled = false;
            _direction = Vector2.zero;
        }
    }
}