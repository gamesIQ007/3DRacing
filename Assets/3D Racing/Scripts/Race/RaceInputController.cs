using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Управление управлением в гонке
    /// </summary>
    public class RaceInputController : MonoBehaviour
    {
        /// <summary>
        /// Управление автомобилем
        /// </summary>
        [SerializeField] private CarInputControl carControl;
        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        [SerializeField] private RaceStateTracker raceStateTracker;

        private void Start()
        {
            raceStateTracker.Started += OnRaceStarted;
            raceStateTracker.Completed += OnRaceFinished;

            carControl.enabled = false;
        }

        private void OnDestroy()
        {
            raceStateTracker.Started -= OnRaceStarted;
            raceStateTracker.Completed -= OnRaceFinished;
        }

        private void OnRaceStarted()
        {
            carControl.enabled = true;
        }

        private void OnRaceFinished()
        {
            carControl.Stop();
            carControl.enabled = false;
        }
    }
}