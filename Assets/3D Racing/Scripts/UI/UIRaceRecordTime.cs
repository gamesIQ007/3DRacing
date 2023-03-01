using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Отображение рекордов трассы
    /// </summary>
    public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>
    {
        /// <summary>
        /// Золото
        /// </summary>
        [SerializeField] private GameObject goldRecordObject;
        /// <summary>
        /// Рекорд игрока
        /// </summary>
        [SerializeField] private GameObject playerRecordObject;

        /// <summary>
        /// Текст золота
        /// </summary>
        [SerializeField] private Text goldRecordText;
        /// <summary>
        /// Текст рекорда
        /// </summary>
        [SerializeField] private Text playerRecordText;

        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        /// <summary>
        /// Замер времени заезда
        /// </summary>
        private RaceResultTime raceResultTime;
        public void Construct(RaceResultTime obj) => raceResultTime = obj;

        private void Start()
        {
            raceStateTracker.Started += OnRaceStarted;
            raceStateTracker.Completed += OnRaceCompleted;

            goldRecordObject.SetActive(false);
            playerRecordObject.SetActive(false);
        }

        private void OnDestroy()
        {
            raceStateTracker.Started -= OnRaceStarted;
            raceStateTracker.Completed -= OnRaceCompleted;
        }

        private void OnRaceStarted()
        {
            if (raceResultTime.PlayerRecordTime > raceResultTime.GoldTime || raceResultTime.RecordWasSet == false)
            {
                goldRecordObject.SetActive(true);
                goldRecordText.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
            }
            if (raceResultTime.RecordWasSet)
            {
                playerRecordObject.SetActive(true);
                playerRecordText.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
            }
        }

        private void OnRaceCompleted()
        {
            goldRecordObject.SetActive(false);
            playerRecordObject.SetActive(false);
        }
    }
}