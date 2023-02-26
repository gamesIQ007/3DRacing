using UnityEngine;

namespace Racing
{
    /// <summary>
    /// ������ ����� � �����������
    /// </summary>
    public class RaceKeyboardStarter : MonoBehaviour, IDependency<RaceStateTracker>
    {
        /// <summary>
        /// ������ ��������� �����
        /// </summary>
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        public void OnRaceStart()
        {
            raceStateTracker.LaunchPreparationStart();
        }
    }
}