using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(Rigidbody))]

    /// <summary>
    /// Шасси автомобиля. Физика всего автомобиля.
    /// </summary>
    public class CarChassis : MonoBehaviour
    {
        /// <summary>
        /// Колёсные оси
        /// </summary>
        [SerializeField] private WheelAxle[] wheelAxles;

        /// <summary>
        /// Длина колёсной базы
        /// </summary>
        [SerializeField] private float wheelBaseLength;

        /// <summary>
        /// Центр масс
        /// </summary>
        [SerializeField] private Transform centerOfMass;

        /// <summary>
        /// Угловое сопротивление
        /// </summary>
        [Header("Angular Drag")]
        [SerializeField] private float angularDragMin;
        [SerializeField] private float angularDragMax;
        [SerializeField] private float angularDragFactor;

        /// <summary>
        /// Прижимная сила
        /// </summary>
        [Header("Down Force")]
        [SerializeField] private float downForceMin;
        [SerializeField] private float downForceMax;
        [SerializeField] private float downForceFactor;

        /// <summary>
        /// Линейная скорость в км/ч
        /// </summary>
        public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;

        private new Rigidbody rigidbody;
        public Rigidbody Rigidbody => rigidbody == null ? GetComponent<Rigidbody>() : rigidbody;

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

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            if (centerOfMass != null)
            {
                rigidbody.centerOfMass = centerOfMass.localPosition;
            }

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                wheelAxles[i].ConfigureVehicleSubsteps(50, 50, 50);
            }
        }

        private void FixedUpdate()
        {
            UpdateAngularDrag();

            UpdateDownForce();

            UpdateWheelAxles();
        }

        /// <summary>
        /// Получаем среднее количество оборотов в минуту со всех колём
        /// </summary>
        /// <returns>Среднее количество оборотов</returns>
        public float GetAverageRpm()
        {
            float sum = 0;

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                sum += wheelAxles[i].GetAverageRpm();
            }

            return sum / wheelAxles.Length;
        }

        /// <summary>
        /// Получаем скорость колеса
        /// </summary>
        /// <returns>Скорость колеса</returns>
        public float GetWheelSpeed()
        {
            return GetAverageRpm() * wheelAxles[0].GetRadius() * 2 * 0.1885f;
        }

        /// <summary>
        /// Обновление колёсных осей
        /// </summary>
        private void UpdateWheelAxles()
        {
            // Количество колёс с мотором
            int amountMotorWheel = 0;
            for (int i = 0; i < wheelAxles.Length; i++)
            {
                if (wheelAxles[i].IsMotor)
                {
                    amountMotorWheel += 2;
                }
            }

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                wheelAxles[i].Update();
                wheelAxles[i].ApplyMotorTorque(MotorTorque / amountMotorWheel);
                wheelAxles[i].ApplySteerAngle(SteerAngle, wheelBaseLength);
                wheelAxles[i].ApplyBreakTorque(BrakeTorque);
            }
        }

        /// <summary>
        /// Сопротивление вращению
        /// </summary>
        private void UpdateAngularDrag()
        {
            rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
        }

        /// <summary>
        /// Обновление прижимной силы
        /// </summary>
        private void UpdateDownForce()
        {
            float downForce = Mathf.Clamp(downForceFactor * LinearVelocity, downForceMin, downForceMax);
            rigidbody.AddForce(-transform.up * downForce);
        }

        /// <summary>
        /// Сброс шасси
        /// </summary>
        public void Reset()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}