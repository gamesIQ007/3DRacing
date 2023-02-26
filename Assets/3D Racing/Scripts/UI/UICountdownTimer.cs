using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Обратный отсчёт на экране
    /// </summary>
    public class UICountdownTimer : MonoBehaviour
    {
        /// <summary>
        /// Текст
        /// </summary>
        [SerializeField] private Text text;
        /// <summary>
        /// Таймер
        /// </summary>
        [SerializeField] private Timer countdownTimer;

        /// <summary>
        /// Трекер состояния гонки
        /// </summary>
        [SerializeField] private RaceStateTracker raceStateTracker;

        private void Start()
        {
            raceStateTracker.PreparationStarted += OnPreparationStarted;
            raceStateTracker.Started += OnRaceStarted;

            text.enabled = false;
        }

        private void OnDestroy()
        {
            raceStateTracker.PreparationStarted -= OnPreparationStarted;
            raceStateTracker.Started -= OnRaceStarted;
        }

        private void Update()
        {
            text.text = countdownTimer.Value.ToString("F0");

            if (text.text == "0")
            {
                text.text = "GO!";
            }
        }

        private void OnPreparationStarted()
        {
            text.enabled = true;
            enabled = true;
        }

        private void OnRaceStarted()
        {
            text.enabled = false;
            enabled = false;
        }
    }
}