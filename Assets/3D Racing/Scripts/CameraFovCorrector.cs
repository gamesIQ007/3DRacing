using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Коррекция угла обзора
    /// </summary>
    public class CameraFovCorrector : MonoBehaviour
    {
        /// <summary>
        /// Автомобиль
        /// </summary>
        [SerializeField] private Car car;
        /// <summary>
        /// Камера
        /// </summary>
        [SerializeField] private new Camera camera;

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