using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Замер времени заезда
    /// </summary>
    public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>
    {
        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        /// <summary>
        /// Текущее время
        /// </summary>
        private float currentTime;
        public float CurrentTime => currentTime;

        private void Start()
        {
            raceStateTracker.Started += OnRaceStarted;
            raceStateTracker.Completed += OnRaceCompleted;

            enabled = false;
        }

        private void OnDestroy()
        {
            raceStateTracker.Started -= OnRaceStarted;
            raceStateTracker.Completed -= OnRaceCompleted;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
        }

        private void OnRaceStarted()
        {
            currentTime = 0;
            enabled = true;
        }

        private void OnRaceCompleted()
        {
            enabled = false;
        }
    }
}