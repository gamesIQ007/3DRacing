using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Кнопка настройки
    /// </summary>
    public class UISettingButton : UISelectableButton, IScriptableObjectProperty
    {
        /// <summary>
        /// Настройка
        /// </summary>
        [SerializeField] private Setting setting;

        /// <summary>
        /// Текст заголовка
        /// </summary>
        [SerializeField] private Text titleText;
        /// <summary>
        /// Текст значения
        /// </summary>
        [SerializeField] private Text valueText;

        /// <summary>
        /// Кнопка назад
        /// </summary>
        [SerializeField] private Image previousImage;
        /// <summary>
        /// Кнопка вперёд
        /// </summary>
        [SerializeField] private Image nextImage;

        private void Start()
        {
            ApplyProperty(setting);
        }

        private void UpdateInfo()
        {
            titleText.text = setting.Title;
            valueText.text = setting.GetStringValue();

            previousImage.enabled = !setting.isMinValue;
            nextImage.enabled = !setting.isMaxValue;
        }

        /// <summary>
        /// Применить следующее значение
        /// </summary>
        public void SetNextValueSetting()
        {
            setting?.SetNextValue();
            setting?.Apply();
            UpdateInfo();
        }

        /// <summary>
        /// Применить предыдущее значение
        /// </summary>
        public void SetPreviousValueSetting()
        {
            setting?.SetPreviousValue();
            setting?.Apply();
            UpdateInfo();
        }

        /// <summary>
        /// Применить свойства
        /// </summary>
        /// <param name="property">Свойства</param>
        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;
            if (property is Setting == false) return;

            setting = property as Setting;

            UpdateInfo();
        }
    }
}