using UnityEngine;

namespace Snake
{
    public class Camera : MonoBehaviour
    {
        [SerializeField]
        private SnakeHead _snakeHead;

        [SerializeField]
        private float _distance;

        private void Update()
        {
            Vector3 dir = _snakeHead.transform.position.normalized;
            Vector3 cameraUp = Quaternion.Euler(dir) * new Vector3(0f, 1f, 0f);
            transform.position = dir * _distance;
            //transform.LookAt(_snakeHead.transform.position, cameraUp);
            transform.rotation = Quaternion.LookRotation(-dir, cameraUp);
            Debug.DrawRay(transform.position, cameraUp * 10f, Color.magenta);
            Debug.DrawRay(transform.position, dir * 10f, Color.cyan);
        }
    }
}