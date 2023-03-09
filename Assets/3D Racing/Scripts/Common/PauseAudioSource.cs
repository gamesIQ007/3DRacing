using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]

    /// <summary>
    /// Пауза звуков
    /// </summary>
    public class PauseAudioSource : MonoBehaviour, IDependency<Pauser>
    {
        /// <summary>
        /// Источник звука
        /// </summary>
        private new AudioSource audio;

        private Pauser pauser;
        public void Construct(Pauser obj) => pauser = obj;

        private void Start()
        {
            audio = GetComponent<AudioSource>();

            pauser.PauseStateChange += OnPauseStateChange;
        }

        private void OnDestroy()
        {
            pauser.PauseStateChange -= OnPauseStateChange;
        }

        private void OnPauseStateChange(bool pause)
        {
            if (pause)
            {
                audio.Stop();
            }
            if (pause == false)
            {
                audio.Play();
            }
        }
    }
}