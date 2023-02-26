using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Следование камеры за машиной
    /// </summary>
    public class CarCameraFollow : CarCameraComponent
    {
        [Header("Offset")]
        /// <summary>
        /// Высота обзора
        /// </summary>
        [SerializeField] private float viewHeight;
        /// <summary>
        /// Высота
        /// </summary>
        [SerializeField] private float height;
        /// <summary>
        /// Дистанция
        /// </summary>
        [SerializeField] private float distance;

        [Header("Damping")]
        /// <summary>
        /// Затухание вращения
        /// </summary>
        [SerializeField] private float rotationDamping;
        /// <summary>
        /// Затухание высоты
        /// </summary>
        [SerializeField] private float heightDamping;
        /// <summary>
        /// Порог скорости
        /// </summary>
        [SerializeField] private float speedThreshold;

        /// <summary>
        /// Цель следования
        /// </summary>
        private Transform target;
        /// <summary>
        /// Тело, за которым следуем
        /// </summary>
        private new Rigidbody rigidbody;

        private void FixedUpdate()
        {
            // Скорость
            Vector3 velocity = rigidbody.velocity;
            // Угол поворота цели
            Vector3 targetRotation = target.eulerAngles;

            if (velocity.magnitude > speedThreshold)
            {
                targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
            }

            //Текущий угол
            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, rotationDamping * Time.fixedDeltaTime);
            float currentHeight = Mathf.Lerp(transform.position.y, target.position.y + height, heightDamping * Time.fixedDeltaTime);

            // Смещение позиции
            Vector3 positionOffset = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            transform.position = target.position - positionOffset;
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            transform.LookAt(target.position + new Vector3(0, viewHeight, 0));
        }

        public override void SetProperties(Car car, Camera camera)
        {
            base.SetProperties(car, camera);

            target = car.transform;
            rigidbody = car.Rigidbody;
        }
    }
}