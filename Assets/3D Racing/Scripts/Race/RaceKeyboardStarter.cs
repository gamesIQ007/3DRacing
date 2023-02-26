using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Запуск гонки с контроллера
    /// </summary>
    public class RaceKeyboardStarter : MonoBehaviour, IDependency<RaceStateTracker>
    {
        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        public void OnRaceStart()
        {
            raceStateTracker.LaunchPreparationStart();
        }
    }
}