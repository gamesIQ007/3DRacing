using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Постройка цепочки контрольных точек
    /// </summary>
    public static class TrackCircuitBuilder
    {
        /// <summary>
        /// Настройка чекпоинтов
        /// </summary>
        /// <param name="trackTransform">Трансформ чекпоинта</param>
        /// <param name="type">Тип трассы</param>
        /// <returns>Массив чекпоинтов</returns>
        public static TrackPoint[] Build(Transform trackTransform, TrackType type)
        {
            TrackPoint[] points = new TrackPoint[trackTransform.childCount];

            ResetPoints(trackTransform, points);

            MakeLinks(points, type);

            MarkPoints(points, type);

            return points;
        }

        /// <summary>
        /// Сброс настроек чекпоинтов
        /// </summary>
        private static void ResetPoints(Transform trackTransform, TrackPoint[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = trackTransform.GetChild(i).GetComponent<TrackPoint>();

                if (points[i] == null)
                {
                    Debug.LogError("Нет скрипта TrackPoint на одном из дочерних объектов.");
                    return;
                }

                points[i].Reset();
            }
        }

        /// <summary>
        /// Создаём связи между чекпоинтами
        /// </summary>
        /// <param name="points">Массив чекпоинтов</param>
        /// <param name="type">Тип трассы</param>
        private static void MakeLinks(TrackPoint[] points, TrackType type)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                points[i].Next = points[i + 1];
            }
            if (type == TrackType.Circular)
            {
                points[points.Length - 1].Next = points[0];
            }
        }

        /// <summary>
        /// Пометка чекпоинтов последний/первый
        /// </summary>
        /// <param name="points">Массив чекпоинтов</param>
        /// <param name="type">Тип трассы</param>
        private static void MarkPoints(TrackPoint[] points, TrackType type)
        {
            points[0].IsFirst = true;

            if (type == TrackType.Sprint)
            {
                points[points.Length - 1].IsLast = true;
            }
            if (type == TrackType.Circular)
            {
                points[0].IsLast = true;
            }
        }
    }
}