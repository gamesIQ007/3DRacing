using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Интерфейс для спавна ScriptableObjectов
    /// </summary>
    interface IScriptableObjectProperty
    {
        /// <summary>
        /// Применить свойства
        /// </summary>
        /// <param name="property">Свойства</param>
        void ApplyProperty(ScriptableObject property);
    }
}