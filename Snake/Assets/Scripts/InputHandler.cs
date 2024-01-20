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
        private float _maxDistance;

        private float _ringMovementSpeed = 30f;
        private Vector2 _direction;

        private void Awake()
        {
            _knob.enabled = false;
            _ring.enabled = false;
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

            float ringLength = _ring.GetComponent<RectTransform>().sizeDelta.x;
            _direction = (eventData.position - (Vector2)_ring.transform.position)
                / ((ringLength - _knob.rectTransform.sizeDelta.x) / 2);

            if (Vector2.Distance(eventData.position, _ring.transform.position)
                >= (ringLength * 0.8f / 2))
            {
                _ring.transform.position = Vector2.MoveTowards(_ring.transform.position,
                    eventData.position, _ringMovementSpeed);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _knob.enabled = false;
            _ring.enabled = false;
            _direction = Vector2.zero;
        }
    }
}