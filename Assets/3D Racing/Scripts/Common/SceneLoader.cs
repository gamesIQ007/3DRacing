using UnityEngine;
using UnityEngine.SceneManagement;

namespace Racing
{
    /// <summary>
    /// Загрузчик сцен
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        /// <summary>
        /// Название сцены с главным меню
        /// </summary>
        private const string MainMenuSceneTitle = "main_menu";

        /// <summary>
        /// Загрузить главное меню
        /// </summary>
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MainMenuSceneTitle);
        }

        /// <summary>
        /// Перезапустить сцену
        /// </summary>
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}