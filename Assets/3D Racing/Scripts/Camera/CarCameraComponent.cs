using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(CarCameraController))]

    /// <summary>
    /// Компоненты камеры
    /// </summary>
    public abstract class CarCameraComponent : MonoBehaviour
    {
        /// <summary>
        /// Автомобиль
        /// </summary>
        protected Car car;
        /// <summary>
        /// Камера
        /// </summary>
        protected new Camera camera;

        /// <summary>
        /// Установка свойств
        /// </summary>
        /// <param name="car">Автомобиль</param>
        /// <param name="camera">Камера</param>
        public virtual void SetProperties(Car car, Camera camera)
        {
            this.car = car;
            this.camera = camera;
        }
    }
}