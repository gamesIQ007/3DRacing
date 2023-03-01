using System;

namespace Racing
{
    /// <summary>
    /// Перевод времени в строку
    /// </summary>
    public static class StringTime
    {
        /// <summary>
        /// Перевод времени в секундах в форматированное время строкой
        /// </summary>
        /// <param name="second">Время в секундах</param>
        /// <returns>Форматированное время строкой</returns>
        public static string SecondToTimeString(float second)
        {
            return TimeSpan.FromSeconds(second).ToString(@"mm\:ss\.ff");
        }
    }
}