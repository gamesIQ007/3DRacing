using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    /// <summary>
    /// Панель итогов заезда
    /// </summary>
    public class UIRaceResultPanel : MonoBehaviour, IDependency<RaceResultTime>
    {
        /// <summary>
        /// Панель итогов
        /// </summary>
        [SerializeField] private GameObject resultPanel;

        /// <summary>
        /// Лучшее время заезда
        /// </summary>
        [SerializeField] private Text recordTime;
        /// <summary>
        /// Текущее время заезда
        /// </summary>
        [SerializeField] private Text currentTime;

        /// <summary>
        /// Замер времени заезда
        /// </summary>
        private RaceResultTime raceResultTime;
        public void Construct(RaceResultTime obj) => raceResultTime = obj;

        private void Start()
        {
            resultPanel.SetActive(false);

            raceResultTime.ResultUpdated += OnResultUpdated;
        }

        private void OnDestroy()
        {
            raceResultTime.ResultUpdated -= OnResultUpdated;
        }

        private void OnResultUpdated()
        {
            resultPanel.SetActive(true);
            recordTime.text = StringTime.SecondToTimeString(raceResultTime.GetAbsoluteRecord());
            currentTime.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime);
        }
    }
}