using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Кнопка, которую можно выбрать
    /// </summary>
    public class UISelectableButton : UIButton
    {
        /// <summary>
        /// Изображение-выделение выбранной кнопки
        /// </summary>
        [SerializeField] private Image selectImage;

        /// <summary>
        /// События выбора и снятия выбора
        /// </summary>
        public UnityEvent OnSelect;
        public UnityEvent OnUnSelect;

        public override void SetFocus()
        {
            base.SetFocus();

            selectImage.enabled = true;
            OnSelect?.Invoke();
        }

        public override void SetUnFocus()
        {
            base.SetUnFocus();

            selectImage.enabled = false;
            OnUnSelect?.Invoke();
        }
    }
}