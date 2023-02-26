using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Интерфейс зависимости
    /// </summary>
    public interface IDependency<T>
    {
        void Construct(T obj);
    }

    /// <summary>
    /// Зависимости сцены
    /// </summary>
    public class SceneDependencies : MonoBehaviour
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

        private void Awake()
        {
            MonoBehaviour[] monoInScene = FindObjectsOfType<MonoBehaviour>();

            for (int i = 0; i < monoInScene.Length; i++)
            {
                Bind(monoInScene[i]);
            }
        }

        /// <summary>
        /// Привязка зависимостей
        /// </summary>
        private void Bind(MonoBehaviour mono)
        {
            if (mono is IDependency<TrackpointCircuit>) (mono as IDependency<TrackpointCircuit>).Construct(trackpointCircuit);
            if (mono is IDependency<RaceStateTracker>) (mono as IDependency<RaceStateTracker>).Construct(raceStateTracker);
            if (mono is IDependency<CarInputControl>) (mono as IDependency<CarInputControl>).Construct(carInputControl);
            if (mono is IDependency<Car>) (mono as IDependency<Car>).Construct(car);
            if (mono is IDependency<CarCameraController>) (mono as IDependency<CarCameraController>).Construct(carCameraController);
        }
    }
}