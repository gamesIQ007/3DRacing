using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    /// <summary>
    /// Таймер
    /// </summary>
    public class Timer : MonoBehaviour
    {
        /// <summary>
        /// Событие окончания таймера
        /// </summary>
        public event UnityAction Finished;

        /// <summary>
        /// Время
        /// </summary>
        [SerializeField] private float time;

        /// <summary>
        /// Значение таймера
        /// </summary>
        private float value;
        public float Value => value;

        private void Start()
        {
            value = time;
        }

        private void Update()
        {
            value -= Time.deltaTime;

            if (value <= 0)
            {
                enabled = false;
                Finished?.Invoke();
            }
        }
    }
}