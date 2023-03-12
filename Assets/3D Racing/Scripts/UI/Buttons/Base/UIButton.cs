using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Кнопка интерфейса
    /// </summary>
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        /// <summary>
        /// Возможно ли взаимодействовать?
        /// </summary>
        [SerializeField] protected bool interactable = true;
        public bool Interactable => interactable;

        /// <summary>
        /// В фокусе ли?
        /// </summary>
        private bool focus;
        public bool Focus => focus;

        /// <summary>
        /// Событие при щелчке (для редактора)
        /// </summary>
        public UnityEvent OnClick;

        /// <summary>
        /// События при наведении указателя, выведении и щелчке (для кода)
        /// </summary>
        public event UnityAction<UIButton> PointerEnter;
        public event UnityAction<UIButton> PointerExit;
        public event UnityAction<UIButton> PointerClick;

        /// <summary>
        /// Установить фокус
        /// </summary>
        public virtual void SetFocus()
        {
            if (interactable == false) return;

            focus = true;
        }

        /// <summary>
        /// Снять фокус
        /// </summary>
        public virtual void SetUnFocus()
        {
            if (interactable == false) return;

            focus = false;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (interactable == false) return;

            PointerEnter?.Invoke(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (interactable == false) return;

            PointerExit?.Invoke(this);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (interactable == false) return;
            
            PointerClick?.Invoke(this);
            OnClick?.Invoke();
        }

        /// <summary>
        /// Поставить возможность взаимодействия
        /// </summary>
        /// <param name="active">Активно?</param>
        public virtual void SetInteractable(bool active)
        {
            interactable = active;
        }
    }
}