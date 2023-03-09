using UnityEngine;

namespace Racing
{
    [CreateAssetMenu]

    /// <summary>
    /// Настройка разрешения экрана
    /// </summary>
    public class ResolutionSetting : Setting
    {
        /// <summary>
        /// Массив доступных разрешений
        /// </summary>
        [SerializeField]
        private Vector2Int[] availableResolutions = new Vector2Int[]
        {
            new Vector2Int(800, 600),
            new Vector2Int(1280, 720),
            new Vector2Int(1600, 900),
            new Vector2Int(1920, 1080)
        };

        /// <summary>
        /// Индекс текущего разрешения
        /// </summary>
        private int currentResolutionIndex = 0;

        public override bool isMinValue { get => currentResolutionIndex == 0; }
        public override bool isMaxValue { get => currentResolutionIndex == availableResolutions.Length - 1; }

        /// <summary>
        /// Сохранение
        /// </summary>
        private void Save()
        {
            PlayerPrefs.SetInt(title, currentResolutionIndex);
        }

        public override void SetNextValue()
        {
            if (isMaxValue == false)
            {
                currentResolutionIndex++;
            }
        }

        public override void SetPreviousValue()
        {
            if (isMinValue == false)
            {
                currentResolutionIndex--;
            }
        }

        public override object GetValue()
        {
            return availableResolutions[currentResolutionIndex];
        }

        public override string GetStringValue()
        {
            return availableResolutions[currentResolutionIndex].x + "x" + availableResolutions[currentResolutionIndex].y;
        }

        public override void Apply()
        {
            Screen.SetResolution(availableResolutions[currentResolutionIndex].x, availableResolutions[currentResolutionIndex].y, true);

            Save();
        }

        public override void Load()
        {
            currentResolutionIndex = PlayerPrefs.GetInt(title, availableResolutions.Length - 1);
        }
    }
}