using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Зависимости сцены
    /// </summary>
    public class SceneDependenciesContainer : Dependency
    {
        /// <summary>
        /// Цепочка контрольных точек
        /// </summary>
        [SerializeField] private TrackpointCircuit trackpointCircuit;
        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        [SerializeField] private RaceStateTracker raceStateTracker;
        /// <summary>
        /// Управление автомобилем
        /// </summary>
        [SerializeField] private CarInputControl carInputControl;
        /// <summary>
        /// Автомобиль
        /// </summary>
        [SerializeField] private Car car;
        /// <summary>
        /// Контроллер камеры
        /// </summary>
        [SerializeField] private CarCameraController carCameraController;
        /// <summary>
        /// Замер времени заезда
        /// </summary>
        [SerializeField] private RaceTimeTracker raceTimeTracker;
        /// <summary>
        /// Итоговое время заезда
        /// </summary>
        [SerializeField] private RaceResultTime raceResultTime;

        private void Awake()
        {
            FindAllObjectsToBind();
        }

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<TrackpointCircuit>(trackpointCircuit, monoBehaviourInScene);
            Bind<RaceStateTracker>(raceStateTracker, monoBehaviourInScene);
            Bind<CarInputControl>(carInputControl, monoBehaviourInScene);
            Bind<Car>(car, monoBehaviourInScene);
            Bind<CarCameraController>(carCameraController, monoBehaviourInScene);
            Bind<RaceTimeTracker>(raceTimeTracker, monoBehaviourInScene);
            Bind<RaceResultTime>(raceResultTime, monoBehaviourInScene);
        }
    }
}