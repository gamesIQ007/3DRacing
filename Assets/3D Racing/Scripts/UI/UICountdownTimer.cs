using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// �������� ������ �� ������
    /// </summary>
    public class UICountdownTimer : MonoBehaviour, IDependency<RaceStateTracker>
    {
        /// <summary>
        /// �����
        /// </summary>
        [SerializeField] private Text text;

        /// <summary>
        /// ������ ��������� �����
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

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
            text.text = raceStateTracker.CoundownTimer.Value.ToString("F0");

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