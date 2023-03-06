using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Кнопка спавна кнопок гонок
    /// </summary>
    public class UIRaceButtonSpawner : MonoBehaviour
    {
        /// <summary>
        /// Родитель
        /// </summary>
        [SerializeField] private Transform parent;
        /// <summary>
        /// Префаб кнопки гонки
        /// </summary>
        [SerializeField] private UIRaceButton prefab;
        /// <summary>
        /// Массив свойств заездов
        /// </summary>
        [SerializeField] private RaceInfo[] properties;

        /// <summary>
        /// Спавн кнопок
        /// </summary>
        [ContextMenu(nameof(Spawn))]
        public void Spawn()
        {
            if (Application.isPlaying) return;

            GameObject[] allObjects = new GameObject[parent.childCount];

            for (int i = 0; i < parent.childCount; i++)
            {
                allObjects[i] = parent.GetChild(i).gameObject;
            }

            for (int i = 0; i < allObjects.Length; i++)
            {
                DestroyImmediate(allObjects[i]);
            }

            for (int i = 0; i < properties.Length; i++)
            {
                UIRaceButton button = Instantiate(prefab, parent);
                button.ApplyProperty(properties[i]);
            }
        }
    }
}