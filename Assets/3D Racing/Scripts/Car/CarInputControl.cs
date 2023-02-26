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
        /// Кривая торможения
        /// </summary>
        [SerializeField] private AnimationCurve brakeCurve;

        /// <summary>
        /// Сила автоматического торможения
        /// </summary>
        [SerializeField] [Range(0.0f, 1.0f)] private float autoBrakeStrenght = 0.5f;

        /// <summary>
        /// Кривая поворота
        /// </summary>
        [SerializeField] private AnimationCurve steerCurve;

        /// <summary>
        /// Скорость колёс
        /// </summary>
        private float wheelSpeed;
        /// <summary>
        /// Вертикальная ось. Вперёд/назад
        /// </summary>
        private float verticalAxis;
        /// <summary>
        /// Горизонтальная ось. Влево/вправо.
        /// </summary>
        private float horizontalAxis;
        /// <summary>
        /// Ось ручника
        /// </summary>
        private float handbrakeAxis;

        /// <summary>
        /// Вектор управления движением
        /// </summary>
        private Vector2 moveInputVector;

        private void Update()
        {
            wheelSpeed = car.WheelSpeed;

            UpdateThrottleAndBrake();
            UpdateSteer();

            UpdateAutoBrake();

            //DEBUG
            if (Input.GetKeyDown(KeyCode.E))
                car.UpGear();
            if (Input.GetKeyDown(KeyCode.Q))
                car.DownGear();
        }

        /// <summary>
        /// Обновление ускорения и торможения
        /// </summary>
        private void UpdateThrottleAndBrake()
        {
            if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
            {
                car.ThrottleControl = Mathf.Abs(verticalAxis);
                car.BrakeControl = 0;
            }
            else
            {
                car.ThrottleControl = 0;
                car.BrakeControl = brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed);
            }

            // Передачи
            if (verticalAxis < 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
            {
                car.ShiftToReverseGear();
            }
            if (verticalAxis > 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
            {
                car.ShiftToFirstGear();
            }

            //car.BrakeControl = handbrakeAxis;
        }

        /// <summary>
        /// Обновление поворота
        /// </summary>
        private void UpdateSteer()
        {
            car.SteerControl = steerCurve.Evaluate(wheelSpeed / car.MaxSpeed) * horizontalAxis;
        }

        /// <summary>
        /// Автоматическое торможение
        /// </summary>
        private void UpdateAutoBrake()
        {
            if (verticalAxis == 0)
            {
                car.BrakeControl = brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed) * autoBrakeStrenght;
            }
        }

        /// <summary>
        /// Остановка
        /// </summary>
        public void Stop()
        {
            moveInputVector = new Vector2(0, 0);
            verticalAxis = 0;
            horizontalAxis = 0;

            car.ThrottleControl = 0;
            car.SteerControl = 0;
            car.BrakeControl = 1;
        }

        public void OnMove(InputValue input)
        {
            moveInputVector = input.Get<Vector2>();
            verticalAxis = moveInputVector.y;
            horizontalAxis = moveInputVector.x;
        }

        public void OnBreak(InputValue input)
        {
            handbrakeAxis = input.Get<float>();
        }
    }
}