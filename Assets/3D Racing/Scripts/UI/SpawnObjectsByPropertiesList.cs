using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Кнопка спавна объектов со свойствами
    /// </summary>
    public class SpawnObjectsByPropertiesList : MonoBehaviour
    {
        /// <summary>
        /// Родитель
        /// </summary>
        [SerializeField] private Transform parent;
        /// <summary>
        /// Префаб
        /// </summary>
        [SerializeField] private GameObject prefab;
        /// <summary>
        /// Массив свойств
        /// </summary>
        [SerializeField] private ScriptableObject[] properties;

        /// <summary>
        /// Спавн кнопок
        /// </summary>
        [ContextMenu(nameof(SpawnInEditMode))]
        public void SpawnInEditMode()
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
                GameObject gameObject = Instantiate(prefab, parent);
                IScriptableObjectProperty scriptableObjectProperty = gameObject.GetComponent<IScriptableObjectProperty>();
                scriptableObjectProperty.ApplyProperty(properties[i]);
            }
        }
    }
}