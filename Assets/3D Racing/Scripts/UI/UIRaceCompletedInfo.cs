using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// ���������� � ������� ������
    /// </summary>
    public class UIRaceCompletedInfo : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>, IDependency<RaceTimeTracker>
    {
        /// <summary>
        /// ������ ����������
        /// </summary>
        [SerializeField] private GameObject panelInfo;

        /// <summary>
        /// ����� � �������
        /// </summary>
        [SerializeField] private Text newRecordText;
        /// <summary>
        /// ����� �������� ������� ������
        /// </summary>
        [SerializeField] private Text currentTimeText;
        /// <summary>
        /// ����� ������� ������� ������
        /// </summary>
        [SerializeField] private Text playerRecordText;

        /// <summary>
        /// ������ ��������� �����
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        /// <summary>
        /// ����� ������� ������
        /// </summary>
        private RaceResultTime raceResultTime;
        public void Construct(RaceResultTime obj) => raceResultTime = obj;

        /// <summary>
        /// ����� ������� ������
        /// </summary>
        private RaceTimeTracker raceTimeTracker;
        public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

        private void Start()
        {
            raceStateTracker.Completed += OnRaceCompleted;
            raceResultTime.ResultUpdated += OnResultUpdated;

            panelInfo.SetActive(false);
            newRecordText.enabled = false;
        }

        private void OnDestroy()
        {
            raceStateTracker.Completed -= OnRaceCompleted;
            raceResultTime.ResultUpdated -= OnResultUpdated;
        }

        private void OnRaceCompleted()
        {
            panelInfo.SetActive(true);
            currentTimeText.text = StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
            playerRecordText.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        }

        private void OnResultUpdated()
        {
            newRecordText.enabled = true;
            playerRecordText.text = StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
        }
    }
}