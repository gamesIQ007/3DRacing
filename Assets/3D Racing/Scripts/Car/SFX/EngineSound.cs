using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]

    /// <summary>
    /// Звуки двигателя
    /// </summary>
    public class EngineSound : MonoBehaviour, IDependency<Car>
    {
        /// <summary>
        /// Автомобиль
        /// </summary>
        private Car car;
        public void Construct(Car obj) => car = obj;

        /// <summary>
        /// Модификатор высоты звука
        /// </summary>
        [SerializeField] private float pitchModifier;
        /// <summary>
        /// Модификатор громкости
        /// </summary>
        [SerializeField] private float volumeModifier;
        /// <summary>
        /// Модификатор оборотов
        /// </summary>
        [SerializeField] private float rpmModifier;

        /// <summary>
        /// Базовая высота звука
        /// </summary>
        [SerializeField] private float basePitch = 1.0f;
        /// <summary>
        /// Базовая громкость
        /// </summary>
        [SerializeField] private float baseVolume = 0.4f;

        /// <summary>
        /// Источник звука
        /// </summary>
        private AudioSource engineAudioSource;

        private void Start()
        {
            engineAudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            engineAudioSource.pitch = basePitch + pitchModifier * ((car.EngineRpm / car.EngineMaxRpm) * rpmModifier);
            engineAudioSource.volume = baseVolume + volumeModifier * (car.EngineRpm / car.EngineMaxRpm);
        }
    }
}