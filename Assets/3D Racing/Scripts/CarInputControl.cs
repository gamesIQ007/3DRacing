using UnityEngine;
using UnityEngine.InputSystem;

namespace Racing
{
    /// <summary>
    /// Управление автомобилем
    /// </summary>
    public class CarInputControl : MonoBehaviour
    {
        /// <summary>
        /// Автомобиль
        /// </summary>
        [SerializeField] private Car car;

        /// <summary>
        /// Вектор управления движением
        /// </summary>
        private Vector2 moveInputVector;

        /// <summary>
        /// Торможение
        /// </summary>
        private float moveBrake;

        private void Update()
        {
            car.ThrottleControl = moveInputVector.y;
            car.SteerControl = moveInputVector.x;
            car.BrakeControl = moveBrake;
        }

        public void OnMove(InputValue input)
        {
            moveInputVector = input.Get<Vector2>();
        }

        public void OnBreak(InputValue input)
        {
            moveBrake = input.Get<float>();
        }
    }
}