using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Отображение информации о гонке
    /// </summary>
    public class UIRaceButton : UISelectableButton, IScriptableObjectProperty
    {
        /// <summary>
        /// Информация о гонке
        /// </summary>
        [SerializeField] private RaceInfo raceInfo;

        /// <summary>
        /// Иконка
        /// </summary>
        [SerializeField] private Image icon;
        /// <summary>
        /// Заголовок
        /// </summary>
        [SerializeField] private Text title;

        private void Start()
        {
            ApplyProperty(raceInfo);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (raceInfo == null) return;

            SceneManager.LoadScene(raceInfo.SceneName);
        }

        /// <summary>
        /// Применить свойства
        /// </summary>
        /// <param name="property">Свойства</param>
        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;
            if (property is RaceInfo == false) return;

            raceInfo = property as RaceInfo;

            icon.sprite = raceInfo.Icon;
            title.text = raceInfo.Title;
        }
    }
}