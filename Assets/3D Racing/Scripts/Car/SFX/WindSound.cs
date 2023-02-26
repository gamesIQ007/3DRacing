using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]

    /// <summary>
    /// Звук ветра при движении
    /// </summary>
    public class WindSound : MonoBehaviour, IDependency<Car>
    {
        /// <summary>
        /// Автомобиль
        /// </summary>
        private Car car;
        public void Construct(Car obj) => car = obj;

        /// <summary>
        /// Минимальная нормализованная скорость для появления звука
        /// </summary>
        [SerializeField] [Range(0.0f, 1.0f)] private float minNormalizedSpeedForSound;

        /// <summary>
        /// Источник звука
        /// </summary>
        private AudioSource windAudioSource;

        private void Start()
        {
            windAudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (car.NormalizedLinearVelocity >= minNormalizedSpeedForSound)
            {
                //windAudioSource.volume = car.NormalizedLinearVelocity; // (1 - minNormalizedSpeedForSound);
                windAudioSource.volume = (car.NormalizedLinearVelocity - minNormalizedSpeedForSound) / (1 - minNormalizedSpeedForSound);
            }
            else
            {
                windAudioSource.volume = 0;
            }
        }
    }
}