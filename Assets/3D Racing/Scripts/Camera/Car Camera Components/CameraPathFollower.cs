using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Камера, следующая по точкам
    /// </summary>
    public class CameraPathFollower : CarCameraComponent
    {
        /// <summary>
        /// Путь
        /// </summary>
        [SerializeField] private Transform path;
        /// <summary>
        /// Цель слежения
        /// </summary>
        [SerializeField] private Transform lookTarget;
        /// <summary>
        /// Скорость перемещения
        /// </summary>
        [SerializeField] private float movementSpeed;

        /// <summary>
        /// Массив точек
        /// </summary>
        private Vector3[] points;
        /// <summary>
        /// Индекс точки
        /// </summary>
        private int pointIndex;

        private void Start()
        {
            points = new Vector3[path.childCount];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = path.GetChild(i).position;
            }
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, points[pointIndex], movementSpeed * Time.deltaTime);

            if (transform.position == points[pointIndex])
            {
                if (pointIndex == points.Length - 1)
                {
                    pointIndex = 0;
                }
                else
                {
                    pointIndex++;
                }
            }

            transform.LookAt(lookTarget);
        }

        /// <summary>
        /// Начать движение к ближайшей точке
        /// </summary>
        public void StartMoveToNearestPoint()
        {
            float minDistance = float.MaxValue;

            for (int i = 0; i < points.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, points[i]);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    pointIndex = i;
                }
            }
        }

        /// <summary>
        /// Назначить цель слежения
        /// </summary>
        /// <param name="target">Цель слежения</param>
        public void SetLookTarget(Transform target)
        {
            lookTarget = target;
        }
    }
}