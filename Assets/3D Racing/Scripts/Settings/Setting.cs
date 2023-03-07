using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Абстрактный класс настроек
    /// </summary>
    public abstract class Setting : ScriptableObject
    {
        /// <summary>
        /// Название настройки
        /// </summary>
        [SerializeField] protected string title;
        public string Title => title;

        /// <summary>
        /// Минимальное/максимальное значение?
        /// </summary>
        public virtual bool isMinValue { get; }
        public virtual bool isMaxValue { get; }

        /// <summary>
        /// Задать следующее/предыдущее значения
        /// </summary>
        public virtual void SetNextValue() { }
        public virtual void SetPreviousValue() { }

        /// <summary>
        /// Получить значение/строковое значение
        /// </summary>
        /// <returns>Значение</returns>
        public virtual object GetValue() { return default(object); }
        public virtual string GetStringValue() { return string.Empty; }

        /// <summary>
        /// Применить настройки
        /// </summary>
        public virtual void Apply() { }

        /// <summary>
        /// Загрузить настройки
        /// </summary>
        public virtual void Load() { }
    }
}