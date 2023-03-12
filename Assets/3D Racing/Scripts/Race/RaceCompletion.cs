using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Racing
{
    /// <summary>
    /// Результаты прохождения трасс
    /// </summary>
    public class RaceCompletion : SingletonBase<RaceCompletion>
    {
        /// <summary>
        /// Файл сохранения
        /// </summary>
        public const string FILENAME = "completion.dat";

        /// <summary>
        /// Внутренний класс данных о результатах завершения эпизодов
        /// </summary>
        [Serializable]
        private class RaceScore
        {
            /// <summary>
            /// Трасса
            /// </summary>
            public RaceInfo race;
            /// <summary>
            /// Время заезда
            /// </summary>
            public float time;
            /// <summary>
            /// Получено ли золото
            /// </summary>
            public bool isGoldTime;
        }

        /// <summary>
        /// Данные о результатах завершения трасс
        /// </summary>
        [SerializeField] private RaceScore[] m_CompletionData;

        private new void Awake()
        {
            base.Awake();
            Saver<RaceScore[]>.TryLoad(FILENAME, ref m_CompletionData);
        }

        /// <summary>
        /// Сохранение результатов заезда
        /// </summary>
        /// <param name="raceTime">Время заезда</param>
        /// <param name="isGoldTime">Получено ли золото</param>
        public void SaveRaceResult(float raceTime, bool isGoldTime)
        {
            foreach (var item in m_CompletionData)
            {
                if (item.race.SceneName == SceneManager.GetActiveScene().name)
                {
                    if (raceTime > item.time)
                    {
                        item.time = raceTime;
                        item.isGoldTime = isGoldTime;
                        Saver<RaceScore[]>.Save(FILENAME, m_CompletionData);
                    }
                }
            }
        }

        /// <summary>
        /// Получаем время заезда
        /// </summary>
        /// <param name="raceName">Имя трассы</param>
        /// <returns>Время заезда</returns>
        public float GetRaceScore(string raceName)
        {
            foreach (var data in m_CompletionData)
            {
                if (raceName == data.race.SceneName)
                {
                    return data.time;
                }
            }
            return 0;
        }

        /// <summary>
        /// Получаем пометку о прохождении трассы на золото
        /// </summary>
        /// <param name="raceInfo">Информация о трассе</param>
        /// <returns>Пройдена ли трасса на золото?</returns>
        public bool GetIsGoldMark(RaceInfo raceInfo)
        {
            foreach (var data in m_CompletionData)
            {
                if (raceInfo == data.race)
                {
                    return data.isGoldTime;
                }
            }
            return false;
        }
    }
}