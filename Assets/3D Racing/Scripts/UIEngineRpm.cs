using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Цвет индикатора скорости
    /// </summary>
    [System.Serializable]
    class EngineIndicatorColor
    {
        /// <summary>
        /// Максимальное число оборотов
        /// </summary>
        public float MaxRpm;

        /// <summary>
        /// Цвет
        /// </summary>
        public Color color;
    }

    /// <summary>
    /// Индикатор количества оборотов двигателя
    /// </summary>
    public class UIEngineRpm : MonoBehaviour
    {
        /// <summary>
        /// Автомобиль
        /// </summary>
        [SerializeField] private Car car;

        /// <summary>
        /// Избражение-индикатор оборотов
        /// </summary>
        [SerializeField] private Image[] images;

        /// <summary>
        /// Цвета индикатора скорости
        /// </summary>
        [SerializeField] private EngineIndicatorColor[] colors;

        /// <summary>
        /// Цвет индикатора скорости
        /// </summary>
        private Color indicatorColor;

        private void Update()
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (car.EngineRpm <= colors[i].MaxRpm)
                {
                    indicatorColor = colors[i].color;
                    break;
                }
            }

            for (int i = 0; i < images.Length; i++)
            {
                images[i].fillAmount = car.EngineRpm / car.EngineMaxRpm;
                images[i].color = indicatorColor;
            }
        }
    }
}