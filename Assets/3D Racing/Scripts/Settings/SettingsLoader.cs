using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Загрузка настроек
    /// </summary>
    public class SettingsLoader : MonoBehaviour
    {
        /// <summary>
        /// Массив настроек
        /// </summary>
        [SerializeField] private Setting[] allSettings;

        private void Awake()
        {
            for (int i = 0; i < allSettings.Length; i++)
            {
                allSettings[i].Load();
                allSettings[i].Apply();
            }
        }
    }
}