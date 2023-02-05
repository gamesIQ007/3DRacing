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
        /// Максимальный угол поворота
        /// </summary>
        [SerializeField] private float MaxSteerAngle;

        /// <summary>
        /// Максимальное торможение
        /// </summary>
        [SerializeField] private float MaxBreakTorque;

        /// <summary>
        /// Кривая крутящего момента двигателя
        /// </summary>
        [SerializeField] private AnimationCurve engineTorqueCurve;
        /// <summary>
        /// Максимальный крутящий момент
        /// </summary>
        [SerializeField] private float MaxMotorTorque;
        /// <summary>
        /// Максимальная скорость
        /// </summary>
        [SerializeField] private int maxSpeed;

        /// <summary>
        /// Линейная скорость
        /// </summary>
        public float LinearVelocity => chassis.LinearVelocity;

        //DEBUG

        [SerializeField] private float linearVelocity;

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
            linearVelocity = LinearVelocity;

            // Крутящий момент двигателя
            float engineTorque = engineTorqueCurve.Evaluate(LinearVelocity / maxSpeed) * MaxMotorTorque;

            if (LinearVelocity >= maxSpeed)
            {
                engineTorque = 0;
            }

            chassis.MotorTorque = engineTorque * ThrottleControl;
            chassis.SteerAngle = MaxSteerAngle * SteerControl;
            chassis.BrakeTorque = MaxBreakTorque * BrakeControl;
        }
    }
}