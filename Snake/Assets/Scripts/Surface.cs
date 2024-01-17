using UnityEngine;

namespace Snake
{
    public class Surface : MonoBehaviour
    {
        [SerializeField]
        private float _gravity = -9.81f;

        [SerializeField]
        private SphereCollider _surfaceCollider;

        [SerializeField]
        private Food _food;

        private int _foodAmount = 10;

        private float _foodHeight = 0.5f;

        public void OnTriggerStay(Collider other)
        {
            Attract(other.GetComponent<Rigidbody>());
        }

        private void Start()
        {
            GenerateFood(_foodAmount);
        }

        public void Attract(Rigidbody rigidbody)
        {
            Vector3 up = (rigidbody.position - transform.position).normalized;
            Vector3 localUp = rigidbody.transform.up;

            rigidbody.AddForce(up * _gravity);
            rigidbody.rotation = Quaternion.FromToRotation(localUp, up)
                * rigidbody.rotation;
        }

        private void GenerateFood(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(_food, _surfaceCollider.center + Random.onUnitSphere.normalized
                    * (_surfaceCollider.radius + _foodHeight), Quaternion.identity);
            }
        }
    }
}