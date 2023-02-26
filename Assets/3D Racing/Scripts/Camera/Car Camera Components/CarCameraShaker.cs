using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Тряска камеры
    /// </summary>
    public class CarCameraShaker : CarCameraComponent
    {
        /// <summary>
        /// Нормализованная скорость, выше которой будет тряска
        /// </summary>
        [SerializeField] [Range(0.0f, 1.0f)] private float normalizedSpeedShake;

        /// <summary>
        /// Сила тряски
        /// </summary>
        [SerializeField] private float shakeAmount;

        private void Update()
        {
            if (car.NormalizedLinearVelocity >= normalizedSpeedShake)
            {
                transform.localPosition += Random.insideUnitSphere * shakeAmount * Time.deltaTime;
            }
        }
    }
}