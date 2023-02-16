using UnityEngine;

namespace Racing
{
    /// <summary>
    /// Эффекты колеса
    /// </summary>
    public class WheelEffect : MonoBehaviour
    {
        /// <summary>
        /// Колёса
        /// </summary>
        [SerializeField] private WheelCollider[] wheels;

        /// <summary>
        /// Дым из-под колёс
        /// </summary>
        [SerializeField] private ParticleSystem[] wheelsSmoke;

        /// <summary>
        /// Звуки колёс
        /// </summary>
        [SerializeField] private new AudioSource audio;

        /// <summary>
        /// Предел проскальзывания колеса вперёд
        /// </summary>
        [SerializeField] private float forwardSlipLimit;
        /// <summary>
        /// Предел проскальзывания колеса вбок
        /// </summary>
        [SerializeField] private float sidewaySlipLimit;

        /// <summary>
        /// Префаб следов заноса
        /// </summary>
        [SerializeField] private GameObject skidPrefab;

        /// <summary>
        /// Пересечение колеса с землёй
        /// </summary>
        private WheelHit wheelHit;
        /// <summary>
        /// Следы заноса
        /// </summary>
        private Transform[] skidTrail;

        private void Start()
        {
            skidTrail = new Transform[wheels.Length];
        }

        private void Update()
        {
            // проскальзывание
            bool isSlip = false;

            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].GetGroundHit(out wheelHit);

                if (wheels[i].isGrounded)
                {
                    if (Mathf.Abs(wheelHit.forwardSlip) > forwardSlipLimit || Mathf.Abs(wheelHit.sidewaysSlip) > sidewaySlipLimit)
                    {
                        if (skidTrail[i] == null)
                        {
                            skidTrail[i] = Instantiate(skidPrefab).transform;
                        }

                        if (skidTrail[i] != null)
                        {
                            skidTrail[i].position = wheels[i].transform.position - wheelHit.normal * wheels[i].radius;
                            skidTrail[i].forward = -wheelHit.normal;

                            wheelsSmoke[i].transform.position = skidTrail[i].position;
                            wheelsSmoke[i].Emit(1);

                            if (audio.isPlaying == false)
                            {
                                audio.Play();
                            }
                        }

                        isSlip = true;

                        continue;
                    }
                }

                skidTrail[i] = null;
                wheelsSmoke[i].Stop();

                if (isSlip == false)
                {
                    audio.Stop();
                }
            }
        }
    }
}