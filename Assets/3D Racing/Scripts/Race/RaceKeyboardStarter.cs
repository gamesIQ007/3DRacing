using UnityEngine;

namespace Racing
{
    /// <summary>
    /// ������ ����� � �����������
    /// </summary>
    public class RaceKeyboardStarter : MonoBehaviour
    {
        /// <summary>
        /// ������ ��������� �����
        /// </summary>
        [SerializeField] private RaceStateTracker raceStateTracker;

        public void OnRaceStart()
        {
            raceStateTracker.LaunchPreparationStart();
        }
    }
}