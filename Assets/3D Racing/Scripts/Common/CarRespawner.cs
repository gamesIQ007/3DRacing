using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Респавн автомобиля
    /// </summary>
    public class CarRespawner : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Car>, IDependency<CarInputControl>
    {
        /// <summary>
        /// Высота респавна
        /// </summary>
        [SerializeField] private float respawnHeight;

        /// <summary>
        /// Чекпоинт респавна
        /// </summary>
        private TrackPoint respawnTrackPoint;

        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        /// <summary>
        /// Автомобиль
        /// </summary>
        private Car car;
        public void Construct(Car obj) => car = obj;

        /// <summary>
        /// Управление автомобилем
        /// </summary>
        private CarInputControl carControl;
        public void Construct(CarInputControl obj) => carControl = obj;

        private void Start()
        {
            raceStateTracker.TrackPointPassed += OnTrackPointPassed;
        }

        private void OnDestroy()
        {
            raceStateTracker.TrackPointPassed -= OnTrackPointPassed;
        }

        private void OnTrackPointPassed(TrackPoint point)
        {
            respawnTrackPoint = point;
        }

        private void OnRespawn()
        {
            if (respawnTrackPoint == null) return;

            if (raceStateTracker.State != RaceState.Race) return;

            car.Respawn(respawnTrackPoint.transform.position + respawnTrackPoint.transform.up * respawnHeight, respawnTrackPoint.transform.rotation);

            carControl.Reset();
        }
    }
}