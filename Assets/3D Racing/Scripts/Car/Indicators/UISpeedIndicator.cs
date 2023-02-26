using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Индикатор скорости автомобиля
    /// </summary>
    public class UISpeedIndicator : MonoBehaviour, IDependency<Car>
    {
        /// <summary>
        /// Автомобиль
        /// </summary>
        private Car car;
        public void Construct(Car obj) => car = obj;

        /// <summary>
        /// Текст скорости
        /// </summary>
        [SerializeField] private Text text;

        private void Update()
        {
            text.text = car.LinearVelocity.ToString("F0");
        }
    }
}