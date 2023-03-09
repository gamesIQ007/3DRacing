using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Панель паузы в интерфейсе
    /// </summary>
    public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
    {
        /// <summary>
        /// Панель паузы
        /// </summary>
        [SerializeField] private GameObject panel;

        /// <summary>
        /// Пауза
        /// </summary>
        private Pauser pauser;
        public void Construct(Pauser obj) => pauser = obj;

        private void Start()
        {
            panel.SetActive(false);

            pauser.PauseStateChange += OnPauseStateChange;
        }

        private void OnDestroy()
        {
            pauser.PauseStateChange -= OnPauseStateChange;
        }

        private void OnPauseStateChange(bool isPause)
        {
            panel.SetActive(isPause);
        }

        /// <summary>
        /// Изменить состояние паузы
        /// </summary>
        private void OnPauseChangeState()
        {
            pauser.ChangePauseState();
        }

        /// <summary>
        /// Снять паузу
        /// </summary>
        public void UnPause()
        {
            pauser.UnPause();
        }
    }
}