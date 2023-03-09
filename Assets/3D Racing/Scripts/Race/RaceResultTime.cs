using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Racing
{
    /// <summary>
    /// Итоговое время заезда
    /// </summary>
    public class RaceResultTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
    {
        /// <summary>
        /// Пометка для сохранения
        /// </summary>
        public const string SaveMark = "_player_best_time";

        /// <summary>
        /// Событие обновления результатов заезда
        /// </summary>
        public event UnityAction ResultUpdated;

        /// <summary>
        /// Время трассы на золото
        /// </summary>
        [SerializeField] private float goldTime;
        public float GoldTime => goldTime;

        /// <summary>
        /// Лучшее время игрока
        /// </summary>
        private float playerRecordTime;
        public float PlayerRecordTime => playerRecordTime;

        /// <summary>
        /// Текущее время
        /// </summary>
        private float currentTime;
        public float CurrentTime => currentTime;

        /// <summary>
        /// Есть ли рекорд
        /// </summary>
        public bool RecordWasSet => playerRecordTime != 0;

        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        /// <summary>
        /// Замер времени заезда
        /// </summary>
        private RaceTimeTracker raceTimeTracker;
        public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

        private void Awake()
        {
            Load();
        }

        private void Start()
        {
            raceStateTracker.Completed += OnRaceCompleted;
        }

        private void OnDestroy()
        {
            raceStateTracker.Completed -= OnRaceCompleted;
        }

        private void OnRaceCompleted()
        {
            // Абсолютный рекорд
            float absoluteRecord = GetAbsoluteRecord();

            if (raceTimeTracker.CurrentTime < absoluteRecord || playerRecordTime == 0)
            {
                playerRecordTime = raceTimeTracker.CurrentTime;

                Save();
            }

            currentTime = raceTimeTracker.CurrentTime;

            ResultUpdated?.Invoke();
        }

        /// <summary>
        /// Получить абсолютный рекорд заезда
        /// </summary>
        /// <returns>Время заезда</returns>
        public float GetAbsoluteRecord()
        {
            if (playerRecordTime < goldTime && playerRecordTime != 0)
            {
                return playerRecordTime;
            }
            else
            {
                return goldTime;
            }
        }

        /// <summary>
        /// Загрузка
        /// </summary>
        private void Load()
        {
            playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
        }

        /// <summary>
        /// Сохранение
        /// </summary>
        private void Save()
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
        }
    }
}