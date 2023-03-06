using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]

    /// <summary>
    /// Звук кнопок
    /// </summary>
    public class UIButtonSound : MonoBehaviour
    {
        /// <summary>
        /// Звуки
        /// </summary>
        [SerializeField] private AudioClip hover;
        [SerializeField] private AudioClip click;

        /// <summary>
        /// Источник звука
        /// </summary>
        private new AudioSource audio;

        /// <summary>
        /// Массив кнопок
        /// </summary>
        private UIButton[] buttons;

        private void Start()
        {
            audio = GetComponent<AudioSource>();

            buttons = GetComponentsInChildren<UIButton>(true);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].PointerEnter += OnPointerEnter;
                buttons[i].PointerClick += OnPointerClick;
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].PointerEnter -= OnPointerEnter;
                buttons[i].PointerClick -= OnPointerClick;
            }
        }

        private void OnPointerEnter(UIButton button)
        {
            audio.PlayOneShot(hover);
        }

        private void OnPointerClick(UIButton button)
        {
            audio.PlayOneShot(click);
        }
    }
}