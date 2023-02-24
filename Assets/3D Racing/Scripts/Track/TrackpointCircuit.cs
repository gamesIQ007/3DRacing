using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    /// <summary>
    /// Тип трассы
    /// </summary>
    public enum TrackType
    {
        /// <summary>
        /// Круговая
        /// </summary>
        Circular,
        /// <summary>
        /// Спринт
        /// </summary>
        Sprint
    }

    /// <summary>
    /// Цепочка контрольных точек
    /// </summary>
    public class TrackpointCircuit : MonoBehaviour
    {
        /// <summary>
        /// Контрольная точка пересечена
        /// </summary>
        public event UnityAction<TrackPoint> TrackPointTriggered;

        /// <summary>
        /// Круг завершён
        /// </summary>
        public event UnityAction<int> LapCompleted;

        /// <summary>
        /// Тип трассы
        /// </summary>
        [SerializeField] private TrackType type;

        /// <summary>
        /// Контрольные точки
        /// </summary>
        private TrackPoint[] points;

        /// <summary>
        /// Количество пройденных кругов
        /// </summary>
        private int lapsCompleted = -1;

        private void Awake()
        {
            BuildCircuit();
        }

        private void Start()
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].Triggered += OnTrackPointTriggered;
            }

            points[0].AssignAsTarget();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].Triggered -= OnTrackPointTriggered;
            }
        }

        /// <summary>
        /// Прохождение контрольной точки
        /// </summary>
        /// <param name="trackPoint">Контрольная точка</param>
        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            if (trackPoint.IsTarget == false) return;

            trackPoint.Passed();
            trackPoint.Next?.AssignAsTarget();

            TrackPointTriggered?.Invoke(trackPoint);

            if (trackPoint.IsLast)
            {
                lapsCompleted++;

                if (type == TrackType.Sprint)
                {
                    LapCompleted?.Invoke(lapsCompleted);
                }

                if (type == TrackType.Circular)
                {
                    if (lapsCompleted > 0)
                    {
                        LapCompleted?.Invoke(lapsCompleted);
                    }
                }
            }
        }

        [ContextMenu(nameof(BuildCircuit))]
        /// <summary>
        /// Постройка цепочки контрольных точек
        /// </summary>
        private void BuildCircuit()
        {
            points = TrackCircuitBuilder.Build(transform, type);
        }
    }
}