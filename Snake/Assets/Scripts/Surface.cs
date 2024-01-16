using UnityEngine;

namespace Snake
{
    public class Surface : MonoBehaviour
    {
        [SerializeField]
        private float _gravity = -9.81f;

        public void Attract(Rigidbody rigidbody)
        {
            Vector3 up = (rigidbody.position - transform.position).normalized;
            Vector3 localUp = rigidbody.transform.up;

            rigidbody.AddForce(up * _gravity);
            rigidbody.rotation = Quaternion.FromToRotation(localUp, up)
                * rigidbody.rotation;
        }

        public void OnTriggerStay(Collider other)
        {
            Attract(other.GetComponent<Rigidbody>());
        }
    }
}