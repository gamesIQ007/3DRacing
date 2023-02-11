using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Подвеска
    /// </summary>
    public class SuspensionArm : MonoBehaviour
    {
        /// <summary>
        /// Цель смещения
        /// </summary>
        [SerializeField] private Transform target;

        /// <summary>
        /// Фактор смещения
        /// </summary>
        [SerializeField] private float factor;

        /// <summary>
        /// Базовое смещение
        /// </summary>
        private float baseOffset;

        private void Start()
        {
            baseOffset = target.localPosition.y;
        }

        private void Update()
        {
            transform.localEulerAngles = new Vector3(0, 0, (target.localPosition.y - baseOffset) * factor);
        }
    }
}