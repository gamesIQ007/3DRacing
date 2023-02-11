using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Индикатор скорости автомобиля
    /// </summary>
    public class UISpeedIndicator : MonoBehaviour
    {
        /// <summary>
        /// Автомобиль
        /// </summary>
        [SerializeField] private Car car;

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