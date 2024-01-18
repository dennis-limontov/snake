using System;
using UnityEngine;

namespace Snake
{
    public class Surface : MonoBehaviour
    {
        [SerializeField]
        private float _gravity = -9.81f;

        [SerializeField]
        private Mesh _surfaceMesh;

        [SerializeField]
        private Food _food;

        [SerializeField]
        private Transform _foodParent;

        private int _foodAmount = 50;

        private float _foodHeight = 0.25f;

        private void OnDestroy()
        {
            Food.OnFoodEaten -= GenerateFood;
        }

        public void OnTriggerStay(Collider other)
        {
            Attract(other.GetComponent<Rigidbody>());
        }

        private void Start()
        {
            Food.OnFoodEaten += GenerateFood;

            for (int i = 0; i < _foodAmount; i++)
            {
                GenerateFood();
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

        private Vector3 GetRandomPointOnSurface(Mesh m)
        {
            int triangleOrigin = Mathf.FloorToInt(UnityEngine.Random.Range(0f, m.triangles.Length) / 3f) * 3;
            Vector3 vertexA = m.vertices[m.triangles[triangleOrigin]];
            Vector3 vertexB = m.vertices[m.triangles[triangleOrigin + 1]];
            Vector3 vertexC = m.vertices[m.triangles[triangleOrigin + 2]];
            Vector3 dAB = vertexB - vertexA;
            Vector3 dBC = vertexC - vertexB;
            float rAB = UnityEngine.Random.Range(0f, 1f);
            float rBC = UnityEngine.Random.Range(0f, 1f);
            Vector3 randPoint = vertexA + rAB * dAB + rBC * dBC;

            Vector3 dirPC = (vertexC - randPoint).normalized;
            Vector3 dirAB = (vertexB - vertexA).normalized;
            Vector3 dirAC = (vertexC - vertexA).normalized;

            Vector3 triangleNormal = Vector3.Cross(dirAC, dirAB).normalized;
            Vector3 dirH_AC = Vector3.Cross(triangleNormal, dirAC).normalized;

            float dot = Vector3.Dot(dirPC, dirH_AC);

            if (dot >= 0)
            {
                Vector3 centralPoint = (vertexA + vertexC) / 2;
                Vector3 symmetricRandPoint = 2 * centralPoint - randPoint;

                randPoint = symmetricRandPoint;
            }

            return randPoint;
        }

        private void GenerateFood()
        {
            Food newFood = Instantiate(_food, _foodParent);
            newFood.transform.position = GetRandomPointOnSurface(_surfaceMesh);
            newFood.transform.position += (newFood.transform.position - transform.position).normalized
                * (newFood.GetComponent<SphereCollider>().radius + _foodHeight);
        }
    }
}