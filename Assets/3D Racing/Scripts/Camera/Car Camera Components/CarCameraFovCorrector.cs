using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Коррекция угла обзора
    /// </summary>
    public class CarCameraFovCorrector : CarCameraComponent
    {
        /// <summary>
        /// Минимальный угол обзора
        /// </summary>
        [SerializeField] private float minFOV;
        /// <summary>
        /// Максимальный угол обзора
        /// </summary>
        [SerializeField] private float maxFOV;

        /// <summary>
        /// Угол обзора по умолчанию
        /// </summary>
        private float defaultFOV;

        private void Start()
        {
            camera.fieldOfView = defaultFOV;
        }

        private void Update()
        {
            camera.fieldOfView = Mathf.Lerp(minFOV, maxFOV, car.NormalizedLinearVelocity);
        }
    }
}