using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Управление управлением в гонке
    /// </summary>
    public class RaceInputController : MonoBehaviour, IDependency<CarInputControl>, IDependency<RaceStateTracker>
    {
        /// <summary>
        /// Управление автомобилем
        /// </summary>
        private CarInputControl carControl;
        public void Construct(CarInputControl obj) => carControl = obj;

        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

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