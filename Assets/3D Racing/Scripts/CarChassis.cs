using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Шасси автомобиля. Физика всего автомобиля.
    /// </summary>
    public class CarChassis : MonoBehaviour
    {
        /// <summary>
        /// Колёсные оси
        /// </summary>
        [SerializeField] private WheelAxle[] wheelAxles;

        //DEBUG

        /// <summary>
        /// Крутящий момент
        /// </summary>
        public float MotorTorque;

        /// <summary>
        /// Торможение
        /// </summary>
        public float BrakeTorque;

        /// <summary>
        /// Угол поворота
        /// </summary>
        public float SteerAngle;

        private void FixedUpdate()
        {
            UpdateWheelAxles();
        }

        /// <summary>
        /// Обновление колёсных осей
        /// </summary>
        private void UpdateWheelAxles()
        {
            for (int i = 0; i < wheelAxles.Length; i++)
            {
                wheelAxles[i].Update();
                wheelAxles[i].ApplyMotorTorque(MotorTorque);
                wheelAxles[i].ApplySteerAngle(SteerAngle);
                wheelAxles[i].ApplyBreakTorque(BrakeTorque);
            }
        }
    }
}