using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Индикатор коробки передач
    /// </summary>
    public class UIGearIndicator : MonoBehaviour, IDependency<Car>
    {
        /// <summary>
        /// Автомобиль
        /// </summary>
        private Car car;
        public void Construct(Car obj) => car = obj;

        /// <summary>
        /// Текст индикатора
        /// </summary>
        [SerializeField] private Text text;

        private void Start()
        {
            car.GearChanged += OnGearChanged;
        }

        private void OnDestroy()
        {
            car.GearChanged -= OnGearChanged;
        }

        /// <summary>
        /// При изменении передачи
        /// </summary>
        /// <param name="gearName">Передача</param>
        private void OnGearChanged(string gearName)
        {
            text.text = gearName;
        }
    }
}