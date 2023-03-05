using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Вывод информации перед началом гонки
    /// </summary>
    public class UIInformationBeforeRaceStarted : MonoBehaviour, IDependency<RaceStateTracker>
    {
        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        /// <summary>
        /// Текст
        /// </summary>
        [SerializeField] private Text info;

        private void Start()
        {
            raceStateTracker.PreparationStarted += OnPreparationStarted;

            info.enabled = true;
        }

        private void OnDestroy()
        {
            raceStateTracker.PreparationStarted -= OnPreparationStarted;
        }

        private void OnPreparationStarted()
        {
            info.enabled = false;
        }
    }
}