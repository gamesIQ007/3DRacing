using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Активный чекпоинт
    /// </summary>
    public class ActivatedTrackPoint : TrackPoint
    {
        [SerializeField] private GameObject hint;

        private void Start()
        {
            hint.SetActive(isTarget);
        }

        protected override void OnPassed()
        {
            hint.SetActive(false);
        }

        protected override void OnAssignAsTarget()
        {
            hint.SetActive(true);
        }
    }
}