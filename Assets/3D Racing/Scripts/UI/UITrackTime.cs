using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// ����������� ������� ������
    /// </summary>
    public class UITrackTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
    {
        /// <summary>
        /// ������ ��������� �����
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        /// <summary>
        /// ����� ������� ������
        /// </summary>
        private RaceTimeTracker timeTracker;
        public void Construct(RaceTimeTracker obj) => timeTracker = obj;

        /// <summary>
        /// �����
        /// </summary>
        [SerializeField] private Text text;

        private void Start()
        {
            raceStateTracker.Started += OnRaceStarted;
            raceStateTracker.Completed += OnRaceCompleted;

            text.enabled = false;
            enabled = false;
        }

        private void OnDestroy()
        {
            raceStateTracker.Started -= OnRaceStarted;
            raceStateTracker.Completed -= OnRaceCompleted;
        }

        private void Update()
        {
            text.text = StringTime.SecondToTimeString(timeTracker.CurrentTime);
        }

        private void OnRaceStarted()
        {
            text.enabled = true;
            enabled = true;
        }

        private void OnRaceCompleted()
        {
            text.enabled = false;
            enabled = false;
        }
    }
}