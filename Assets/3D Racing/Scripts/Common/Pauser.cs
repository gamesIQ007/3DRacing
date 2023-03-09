using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Racing
{
    /// <summary>
    /// Пауза
    /// </summary>
    public class Pauser : MonoBehaviour
    {
        /// <summary>
        /// Состояние паузы изменилось
        /// </summary>
        public event UnityAction<bool> PauseStateChange;

        /// <summary>
        /// Пауза?
        /// </summary>
        private bool isPause;
        public bool IsPause => isPause;

        private void Awake()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            UnPause();
        }

        /// <summary>
        /// Изменить состояние паузы
        /// </summary>
        public void ChangePauseState()
        {
            if (isPause)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }

        /// <summary>
        /// Поставить паузу
        /// </summary>
        public void Pause()
        {
            if (isPause) return;

            Time.timeScale = 0;
            isPause = true;
            PauseStateChange?.Invoke(isPause);
        }

        /// <summary>
        /// Снять паузу
        /// </summary>
        public void UnPause()
        {
            if (isPause == false) return;

            Time.timeScale = 1;
            isPause = false;
            PauseStateChange?.Invoke(isPause);
        }
    }
}