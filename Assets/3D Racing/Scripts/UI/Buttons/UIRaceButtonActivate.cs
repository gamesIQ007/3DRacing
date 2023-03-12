using System;
using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Активатор кнопок заездов
    /// </summary>
    public class UIRaceButtonActivate : MonoBehaviour
    {
        /// <summary>
        /// Класс сезона и контейнера с его гонками
        /// </summary>
        [Serializable]
        private class Seasons
        {
            /// <summary>
            /// Сезон
            /// </summary>
            [SerializeField] private UISelectableButton season;
            public UISelectableButton Season => season;
            /// <summary>
            /// Контейнер с трассами
            /// </summary>
            [SerializeField] private Transform container;
            public Transform Container => container;
        }

        /// <summary>
        /// Сезоны
        /// </summary>
        [SerializeField] private Seasons[] seasons;
        
        private void Start()
        {
            for (int i = 0; i < seasons.Length; i++)
            {
                for (int j = 0; j < seasons[i].Container.childCount; j++)
                {
                    UIRaceButton raceButton = seasons[i].Container.GetChild(j).GetComponent<UIRaceButton>();
                    raceButton.SetInteractable(false);
                }
                seasons[i].Season.SetInteractable(false);
            }

            bool newTrackWasOpened = false;
            for (int i = 0; i < seasons.Length; i++)
            {
                for (int j = 0; j < seasons[i].Container.childCount; j++)
                {
                    UIRaceButton raceButton = seasons[i].Container.GetChild(j).GetComponent<UIRaceButton>();
                    raceButton.SetInteractable(true);
                    bool interactable = RaceCompletion.Instance.GetIsGoldMark(raceButton.RaceInfo);

                    if (interactable == false)
                    {
                        newTrackWasOpened = true;
                    }

                    if (interactable == false) break;
                }
                if (seasons[i].Container.GetChild(0).GetComponent<UIRaceButton>().Interactable)
                {
                    seasons[i].Season.SetInteractable(true);
                }
                if (newTrackWasOpened) break;
            }
        }
    }
}