using UnityEngine;
using UnityEngine.SceneManagement;

namespace Racing
{
    /// <summary>
    /// Глобальные зависимости
    /// </summary>
    public class GlobalDependenciesContainer : Dependency
    {
        /// <summary>
        /// Пауза
        /// </summary>
        [SerializeField] private Pauser pauser;

        /// <summary>
        /// Инстанс
        /// </summary>
        private static GlobalDependenciesContainer instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<Pauser>(pauser, monoBehaviourInScene);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            FindAllObjectsToBind();
        }
    }
}