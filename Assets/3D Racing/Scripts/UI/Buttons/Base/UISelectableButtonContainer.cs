using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Контейнер выбираемых кнопок
    /// </summary>
    public class UISelectableButtonContainer : MonoBehaviour
    {
        /// <summary>
        /// Контейнер с кнопками
        /// </summary>
        [SerializeField] private Transform buttonsContainer;

        /// <summary>
        /// Активен ли контейнер кнопок
        /// </summary>
        public bool Interactable;
        public void SetInteractable(bool interactable) => Interactable = interactable;

        /// <summary>
        /// Массив выбираемых кнопок
        /// </summary>
        private UISelectableButton[] buttons;

        /// <summary>
        /// Индекс выбранной кнопки
        /// </summary>
        private int selectButtonIndex = 0;

        private void Start()
        {
            buttons = buttonsContainer.GetComponentsInChildren<UISelectableButton>();

            if (buttons == null)
            {
                Debug.Log("Нет кнопок");
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].PointerEnter += OnPointerEnter;
            }

            if (Interactable == false) return;

            buttons[selectButtonIndex].SetFocus();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].PointerEnter -= OnPointerEnter;
            }
        }

        private void OnPointerEnter(UIButton button)
        {
            SelectButton(button);
        }

        /// <summary>
        /// Выбрать кнопку
        /// </summary>
        /// <param name="button">Кнопка</param>
        private void SelectButton(UIButton button)
        {
            if (Interactable == false) return;

            buttons[selectButtonIndex].SetUnFocus();

            for (int i = 0; i < buttons.Length; i++)
            {
                if (button == buttons[i])
                {
                    selectButtonIndex = i;
                    button.SetFocus();
                    break;
                }
            }
        }

        /// <summary>
        /// Выбрать следующую кнопку
        /// </summary>
        public void SelectNext()
        {

        }

        /// <summary>
        /// Выбрать предыдущую кнопку
        /// </summary>
        public void SelectPrevious()
        {

        }
    }
}