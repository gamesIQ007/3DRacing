using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    /// <summary>
    /// Состояние гонки
    /// </summary>
    public enum RaceState
    {
        /// <summary>
        /// Подготовка
        /// </summary>
        Preparation,
        /// <summary>
        /// Обратный отсчёт
        /// </summary>
        CountDown,
        /// <summary>
        /// Гонка
        /// </summary>
        Race,
        /// <summary>
        /// Завершена
        /// </summary>
        Passed
    }

    /// <summary>
    /// Отслеживание состояния гонки
    /// </summary>
    public class RaceStateTracker : MonoBehaviour
    {
        /// <summary>
        /// Событие подготовки ко старту
        /// </summary>
        public event UnityAction PreparationStarted;
        /// <summary>
        /// Событие старта
        /// </summary>
        public event UnityAction Started;
        /// <summary>
        /// Событие завершения гонки
        /// </summary>
        public event UnityAction Completed;
        /// <summary>
        /// Событие прохождения чекпоинта
        /// </summary>
        public event UnityAction<TrackPoint> TrackPointPassed;
        /// <summary>
        /// Событие прохождения круга
        /// </summary>
        public event UnityAction<int> LapCompleted;

        /// <summary>
        /// Цепочка контрольных точек
        /// </summary>
        [SerializeField] private TrackpointCircuit trackpointCircuit;
        /// <summary>
        /// Количество кругов в заезде
        /// </summary>
        [SerializeField] private int lapsToComplete;
        /// <summary>
        /// Таймер обратного отсчёта
        /// </summary>
        [SerializeField] private Timer countdownTimer;

        /// <summary>
        /// Состояние гонки
        /// </summary>
        private RaceState state;
        public RaceState State => state;

        private void Start()
        {
            StartState(RaceState.Preparation);

            countdownTimer.enabled = false;
            countdownTimer.Finished += OnCountdownTimerFinished;

            trackpointCircuit.TrackPointTriggered += OnTrackPointTriggered;
            trackpointCircuit.LapCompleted += OnLapCompleted;
        }

        private void OnDestroy()
        {
            countdownTimer.Finished -= OnCountdownTimerFinished;
            trackpointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
            trackpointCircuit.LapCompleted -= OnLapCompleted;
        }

        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            TrackPointPassed?.Invoke(trackPoint);
        }

        private void OnLapCompleted(int lapAmount)
        {
            if (trackpointCircuit.Type == TrackType.Sprint)
            {
                CompleteRace();
            }
            if (trackpointCircuit.Type == TrackType.Circular)
            {
                if (lapAmount == lapsToComplete)
                {
                    CompleteRace();
                }
                else
                {
                    CompleteLap(lapAmount);
                }
            }
        }

        private void OnCountdownTimerFinished()
        {
            StartRace();
        }

        /// <summary>
        /// Запуск подготовки к старту
        /// </summary>
        public void LaunchPreparationStart()
        {
            if (state != RaceState.Preparation) return;
            StartState(RaceState.CountDown);

            countdownTimer.enabled = true;

            PreparationStarted?.Invoke();
        }

        /// <summary>
        /// Старт гонки
        /// </summary>
        private void StartRace()
        {
            if (state != RaceState.CountDown) return;
            StartState(RaceState.Race);

            Started?.Invoke();
        }

        /// <summary>
        /// Завершение гонки
        /// </summary>
        private void CompleteRace()
        {
            if (state != RaceState.Race) return;
            StartState(RaceState.Passed);

            Completed?.Invoke();
        }

        /// <summary>
        /// Прохождение круга
        /// </summary>
        /// <param name="lapAmount">Количество кругов</param>
        private void CompleteLap(int lapAmount)
        {
            LapCompleted?.Invoke(lapAmount);
        }

        /// <summary>
        /// Задать состояние гонки
        /// </summary>
        /// <param name="state">Состояние</param>
        private void StartState(RaceState state)
        {
            this.state = state;
        }
    }
}