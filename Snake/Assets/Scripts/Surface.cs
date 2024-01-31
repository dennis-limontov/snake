using UnityEngine;

namespace Snake
{
    public class Surface : MonoBehaviour
    {
        [SerializeField]
        private float _gravity = -9.81f;

        public void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Rigidbody rigidbody))
            {
                Attract(rigidbody);
            }
        }

        public void Attract(Rigidbody rigidbody)
        {
            Vector3 up = (rigidbody.position - transform.position).normalized;
            Vector3 localUp = rigidbody.transform.up;

            rigidbody.AddForce(up * _gravity);
            rigidbody.transform.rotation = Quaternion.FromToRotation(localUp, up)
                * rigidbody.rotation;
        }
    }
}