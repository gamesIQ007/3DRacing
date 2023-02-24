using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    /// <summary>
    /// Контрольная точка
    /// </summary>
    public class TrackPoint : MonoBehaviour
    {
        /// <summary>
        /// Событие при триггере контрольной точки
        /// </summary>
        public event UnityAction<TrackPoint> Triggered;

        /// <summary>
        /// Виртуальный метод при проезде через чекпоинт
        /// </summary>
        protected virtual void OnPassed() { }
        /// <summary>
        /// Виртуальный метод при назначении чекпоинта активным
        /// </summary>
        protected virtual void OnAssignAsTarget() { }

        /// <summary>
        /// Следующий чекпоинт
        /// </summary>
        public TrackPoint Next;

        /// <summary>
        /// Первый ли чекпоинт
        /// </summary>
        public bool IsFirst;

        /// <summary>
        /// Последний ли чекпоинт
        /// </summary>
        public bool IsLast;

        /// <summary>
        /// Является ли чекпоинт целью?
        /// </summary>
        protected bool isTarget;
        public bool IsTarget => isTarget;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.GetComponent<Car>() == null) return;

            Triggered?.Invoke(this);
        }

        /// <summary>
        /// Прохождение точки
        /// </summary>
        public void Passed()
        {
            isTarget = false;
            OnPassed();
        }

        /// <summary>
        /// Установить точку целью
        /// </summary>
        public void AssignAsTarget()
        {
            isTarget = true;
            OnAssignAsTarget();
        }

        /// <summary>
        /// Сброс настроек чекпоинта
        /// </summary>
        public void Reset()
        {
            Next = null;
            IsFirst = false;
            IsLast = false;
        }
    }
}