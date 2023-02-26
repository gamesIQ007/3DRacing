using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Управление камерой
    /// </summary>
    public class CarCameraController : MonoBehaviour, IDependency<Car>, IDependency<RaceStateTracker>
    {
        /// <summary>
        /// Камера
        /// </summary>
        [SerializeField] private new Camera camera;
        /// <summary>
        /// Камера, следующая за автомобилем
        /// </summary>
        [SerializeField] private CarCameraFollow follower;
        /// <summary>
        /// Тряска камеры автомобиля
        /// </summary>
        [SerializeField] private CarCameraShaker shaker;
        /// <summary>
        /// Камера-корректор FOV автомобиля
        /// </summary>
        [SerializeField] private CarCameraFovCorrector fovCorrector;
        /// <summary>
        /// Путь пролёта камеры
        /// </summary>
        [SerializeField] private CameraPathFollower pathFollower;

        /// <summary>
        /// Автомобиль
        /// </summary>
        private Car car;
        public void Construct(Car obj) => car = obj;

        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private void Awake()
        {
            follower.SetProperties(car, camera);
            shaker.SetProperties(car, camera);
            fovCorrector.SetProperties(car, camera);
        }

        private void Start()
        {
            raceStateTracker.PreparationStarted += OnPreparationStarted;
            raceStateTracker.Completed += OnCompleted;

            follower.enabled = false;
            pathFollower.enabled = true;
        }

        private void OnDestroy()
        {
            raceStateTracker.PreparationStarted -= OnPreparationStarted;
            raceStateTracker.Completed -= OnCompleted;
        }

        private void OnPreparationStarted()
        {
            follower.enabled = true;
            pathFollower.enabled = false;
        }

        private void OnCompleted()
        {
            follower.enabled = false;
            pathFollower.enabled = true;
            pathFollower.StartMoveToNearestPoint();
            pathFollower.SetLookTarget(car.transform);
        }
    }
}