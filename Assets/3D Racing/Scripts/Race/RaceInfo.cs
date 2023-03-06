using UnityEngine;

namespace Racing
{
    [CreateAssetMenu]

    /// <summary>
    /// Информация о гонке
    /// </summary>
    public class RaceInfo : ScriptableObject
    {
        /// <summary>
        /// Имя сцены
        /// </summary>
        [SerializeField] private string sceneName;
        public string SceneName => sceneName;

        /// <summary>
        /// Спрайт
        /// </summary>
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;

        /// <summary>
        /// Заголовок
        /// </summary>
        [SerializeField] private string title;
        public string Title => title;
    }
}