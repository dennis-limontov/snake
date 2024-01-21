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
        private Image _ring;

        [SerializeField]
        private float _ringMovementSpeed = 30f;

        private float _ringRadius;
        private float _maxDistance;
        private Vector2 _direction;

        private void Awake()
        {
            _knob.enabled = false;
            _ring.enabled = false;
            _ringRadius = _ring.rectTransform.sizeDelta.x / 2;
            float knobRadius = _knob.rectTransform.sizeDelta.x / 2;
            _maxDistance = _ringRadius - knobRadius;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _knob.transform.position = eventData.position;
            _ring.transform.position = eventData.position;
            _knob.enabled = true;
            _ring.enabled = true;
            _direction = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _knob.transform.position = eventData.position;

            if (Vector2.Distance(eventData.position, _ring.transform.position)
                >= (_ringRadius * 0.8f))
            {
                _ring.transform.position = Vector2.MoveTowards(_ring.transform.position,
                    eventData.position, _ringMovementSpeed);
            }

            _direction = (eventData.position - (Vector2)_ring.transform.position) / _maxDistance;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _knob.enabled = false;
            _ring.enabled = false;
            _direction = Vector2.zero;
        }
    }
}