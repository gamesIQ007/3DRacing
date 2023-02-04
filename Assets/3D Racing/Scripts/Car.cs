using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(CarChassis))]

    /// <summary>
    /// Информационная модель автомобиля.
    /// </summary>
    public class Car : MonoBehaviour
    {
        /// <summary>
        /// Максимальный крутящий момент
        /// </summary>
        [SerializeField] private float MaxMotorTorque;

        /// <summary>
        /// Максимальный угол поворота
        /// </summary>
        [SerializeField] private float MaxSteerAngle;

        /// <summary>
        /// Максимальное торможение
        /// </summary>
        [SerializeField] private float MaxBreakTorque;

        //DEBUG

        /// <summary>
        /// Контрол педали газа
        /// </summary>
        public float ThrottleControl;

        /// <summary>
        /// Контрол поворота
        /// </summary>
        public float SteerControl;

        /// <summary>
        /// Контрол торможения
        /// </summary>
        public float BrakeControl;

        /// <summary>
        /// Шасси
        /// </summary>
        private CarChassis chassis;

        private void Start()
        {
            chassis = GetComponent<CarChassis>();
        }

        private void Update()
        {
            chassis.MotorTorque = MaxMotorTorque * ThrottleControl;
            chassis.SteerAngle = MaxSteerAngle * SteerControl;
            chassis.BrakeTorque = MaxBreakTorque * BrakeControl;
        }
    }
}