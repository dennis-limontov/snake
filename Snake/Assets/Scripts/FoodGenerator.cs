using UnityEngine;

namespace Snake
{
    public class FoodGenerator : MonoBehaviour
    {
        [SerializeField]
        private Food _food;

        [SerializeField]
        private Transform _foodParent;

        [SerializeField]
        private int _foodAmount = 50;

        [SerializeField]
        private float _foodHeight = 0.35f;

        [SerializeField]
        private MeshCollider _surfaceMeshCollider;

        private Mesh _surfaceMesh;

        private void OnDestroy()
        {
            Food.OnFoodEaten -= GenerateFood;
        }

        private void Start()
        {
            _surfaceMesh = _surfaceMeshCollider.sharedMesh;

            Food.OnFoodEaten += GenerateFood;

            for (int i = 0; i < _foodAmount; i++)
            {
                GenerateFood();
            }
        }

        private static Vector3 GetRandomPointOnSurface(Mesh m)
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

        private void GenerateFood(Food newFood = null)
        {
            if (newFood == null)
            {
                newFood = Instantiate(_food, _foodParent);
            }
            newFood.transform.position = _surfaceMeshCollider.ClosestPoint(GetRandomPointOnSurface(_surfaceMesh));
            newFood.transform.position += (newFood.transform.position - transform.position).normalized
                * (newFood.GetComponent<SphereCollider>().radius + _foodHeight);
        }
    }
}
