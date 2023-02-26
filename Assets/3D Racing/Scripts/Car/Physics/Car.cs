using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    [RequireComponent(typeof(CarChassis))]

    /// <summary>
    /// Информационная модель автомобиля.
    /// </summary>
    public class Car : MonoBehaviour
    {
        /// <summary>
        /// Событие изменения передачи
        /// </summary>
        public event UnityAction<string> GearChanged;

        /// <summary>
        /// Максимальный угол поворота
        /// </summary>
        [SerializeField] private float MaxSteerAngle;

        /// <summary>
        /// Максимальное торможение
        /// </summary>
        [SerializeField] private float MaxBreakTorque;

        [Header("Engine")]
        /// <summary>
        /// Кривая крутящего момента двигателя
        /// </summary>
        [SerializeField] private AnimationCurve engineTorqueCurve;
        /// <summary>
        /// Максимальный крутящий момент
        /// </summary>
        [SerializeField] private float engineMaxTorque;
        //DEBUG
        /// <summary>
        /// Текущий крутящий момент
        /// </summary>
        [SerializeField] private float engineTorque;
        //DEBUG
        /// <summary>
        /// Текущие обороты двигателя
        /// </summary>
        [SerializeField] private float engineRpm;
        /// <summary>
        /// Минимальные обороты двигателя
        /// </summary>
        [SerializeField] private float engineMinRpm;
        /// <summary>
        /// Максимальные обороты двигателя
        /// </summary>
        [SerializeField] private float engineMaxRpm;
        
        [Header("Gearbox")]
        /// <summary>
        /// Передачи
        /// </summary>
        [SerializeField] private float[] gears;
        /// <summary>
        /// Финальная передача дифференциала (на что домножается ближе к колесу)
        /// </summary>
        [SerializeField] private float finalDriveRatio;

        //DEBUG
        /// <summary>
        /// Выбранная передача
        /// </summary>
        [SerializeField] private float selectedGear;
        /// <summary>
        /// Индекс выбранной передачи
        /// </summary>
        [SerializeField] private int selectedGearIndex;
        /// <summary>
        /// Передача заднего хода
        /// </summary>
        [SerializeField] private float rearGear;
        /// <summary>
        /// Обороты, при которых нужно переключать передачу вверх
        /// </summary>
        [SerializeField] private float upShiftEngineRpm;
        /// <summary>
        /// Обороты, при которых нужно переключать передачу вниз
        /// </summary>
        [SerializeField] private float downShiftEngineRpm;

        /// <summary>
        /// Максимальная скорость
        /// </summary>
        [SerializeField] private int maxSpeed;

        /// <summary>
        /// Линейная скорость
        /// </summary>
        public float LinearVelocity => chassis.LinearVelocity;

        /// <summary>
        /// Нормализованная линейная скорость
        /// </summary>
        public float NormalizedLinearVelocity => chassis.LinearVelocity / maxSpeed;

        /// <summary>
        /// Скорость колеса
        /// </summary>
        public float WheelSpeed => chassis.GetWheelSpeed();

        public int MaxSpeed => maxSpeed;
        public float EngineRpm => engineRpm;
        public float EngineMaxRpm => engineMaxRpm;

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

        public Rigidbody Rigidbody => chassis == null ? GetComponent<CarChassis>().Rigidbody : chassis.Rigidbody;

        private void Start()
        {
            chassis = GetComponent<CarChassis>();
        }

        private void Update()
        {
            linearVelocity = LinearVelocity;

            UpdateEngineTorque();
            AutoGearShift();

            if (LinearVelocity >= maxSpeed)
            {
                engineTorque = 0;
            }

            chassis.MotorTorque = engineTorque * ThrottleControl;
            chassis.SteerAngle = MaxSteerAngle * SteerControl;
            chassis.BrakeTorque = MaxBreakTorque * BrakeControl;
        }

        /// <summary>
        /// Обновление крутящего момента двигателя
        /// </summary>
        private void UpdateEngineTorque()
        {
            engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * selectedGear * finalDriveRatio);
            engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

            engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear) * gears[0];
        }

        #region Gearbox

        /// <summary>
        /// Повысить передачу
        /// </summary>
        public void UpGear()
        {
            ShiftGear(selectedGearIndex + 1);
        }

        /// <summary>
        /// Понизить передачу
        /// </summary>
        public void DownGear()
        {
            ShiftGear(selectedGearIndex - 1);
        }

        /// <summary>
        /// Включить заднюю передачу
        /// </summary>
        public void ShiftToReverseGear()
        {
            selectedGear = rearGear;

            GearChanged?.Invoke(GetSelectedGearName());
        }

        /// <summary>
        /// Включить первую передачу
        /// </summary>
        public void ShiftToFirstGear()
        {
            ShiftGear(0);

            GearChanged?.Invoke(GetSelectedGearName());
        }

        /// <summary>
        /// Включить нейтральную передачу
        /// </summary>
        public void ShiftToNeutral()
        {
            selectedGear = 0;

            GearChanged?.Invoke(GetSelectedGearName());
        }

        /// <summary>
        /// Переключить передачу
        /// </summary>
        /// <param name="gearIndex">Индекс передачи</param>
        private void ShiftGear(int gearIndex)
        {
            gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
            selectedGear = gears[gearIndex];
            selectedGearIndex = gearIndex;

            GearChanged?.Invoke(GetSelectedGearName());
        }

        /// <summary>
        /// Автоматическое переключение передач
        /// </summary>
        private void AutoGearShift()
        {
            if (selectedGear < 0) return;

            if (engineRpm >= upShiftEngineRpm)
            {
                UpGear();
            }

            if (engineRpm < downShiftEngineRpm)
            {
                DownGear();
            }
        }

        /// <summary>
        /// Возвращает выбранную передачу
        /// </summary>
        /// <returns>Выбранная передача</returns>
        public string GetSelectedGearName()
        {
            if (selectedGear == rearGear) return "R";

            if (selectedGear == 0) return "N";

            return (selectedGearIndex + 1).ToString();
        }

        #endregion
    }
}