using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Запуск гонки с контроллера
    /// </summary>
    public class RaceKeyboardStarter : MonoBehaviour
    {
        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        [SerializeField] private RaceStateTracker raceStateTracker;

        public void OnRaceStart()
        {
            raceStateTracker.LaunchPreparationStart();
        }
    }
}