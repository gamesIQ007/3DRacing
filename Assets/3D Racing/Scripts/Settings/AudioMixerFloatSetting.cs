using UnityEngine;
using UnityEngine.Audio;

namespace Racing
{
    [CreateAssetMenu]

    /// <summary>
    /// Настройка громкости звука
    /// </summary>
    public class AudioMixerFloatSetting : Setting
    {
        /// <summary>
        /// Аудиомикшер
        /// </summary>
        [SerializeField] private AudioMixer audioMixer;

        /// <summary>
        /// Название настройки
        /// </summary>
        [SerializeField] private string nameParameter;

        /// <summary>
        /// Минимальное и максимальное реальное значение
        /// </summary>
        [SerializeField] private float minRealValue;
        [SerializeField] private float maxRealValue;

        /// <summary>
        /// Шаг изменения настройки
        /// </summary>
        [SerializeField] private float virtualStep;
        /// <summary>
        /// Минимальное и максимальное виртуальное значение
        /// </summary>
        [SerializeField] private float minVirtualValue;
        [SerializeField] private float maxVirtualValue;

        /// <summary>
        /// Текущее значение
        /// </summary>
        private float currentValue = 0;

        public override bool isMinValue { get => currentValue == minRealValue; }
        public override bool isMaxValue { get => currentValue == maxRealValue; }

        /// <summary>
        /// Добавить значение
        /// </summary>
        /// <param name="value">Значение</param>
        private void AddValue(float value)
        {
            currentValue += value;
            currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
        }

        /// <summary>
        /// Сохранение
        /// </summary>
        private void Save()
        {
            PlayerPrefs.SetFloat(title, currentValue);
        }

        public override void SetNextValue()
        {
            AddValue(Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
        }

        public override void SetPreviousValue()
        {
            AddValue(-Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
        }

        public override string GetStringValue()
        {
            return Mathf.Lerp(minVirtualValue, maxVirtualValue, (currentValue - minRealValue) / (maxRealValue - minRealValue)).ToString();
        }

        public override object GetValue()
        {
            return currentValue;
        }

        public override void Apply()
        {
            audioMixer.SetFloat(nameParameter, currentValue);

            Save();
        }

        public override void Load()
        {
            currentValue = PlayerPrefs.GetFloat(title, 0);
        }
    }
}