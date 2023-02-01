using UnityEngine;
using UnityEngine.InputSystem;

namespace Racing
{
    /// <summary>
    /// Класс автомобиля
    /// </summary>
    public class Car : MonoBehaviour
    {
        /// <summary>
        /// Коллайдеры колёс
        /// </summary>
        [SerializeField] private WheelCollider[] wheelColliders;
    
        /// <summary>
        /// Меши колёс
        /// </summary>
        [SerializeField] private Transform[] wheelMeshs;

        /// <summary>
        /// Крутящий момент
        /// </summary>
        [SerializeField] private float motorTorque;

        /// <summary>
        /// Торможение
        /// </summary>
        [SerializeField] private float brakeTorque;

        /// <summary>
        /// Угол поворота
        /// </summary>
        [SerializeField] private float steerAngle;

        /// <summary>
        /// Вектор управления движением
        /// </summary>
        private Vector2 moveInputVector;

        /// <summary>
        /// Торможение
        /// </summary>
        private float moveBreak;

        private void Update()
        {
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                wheelColliders[i].motorTorque = moveInputVector.y * motorTorque;
                wheelColliders[i].brakeTorque = moveBreak * brakeTorque;
            }

            wheelColliders[0].steerAngle = moveInputVector.x * steerAngle;
            wheelColliders[1].steerAngle = moveInputVector.x * steerAngle;

            for (int i = 0; i < wheelColliders.Length; i++)
            {
                Vector3 position;
                Quaternion rotation;

                wheelColliders[i].GetWorldPose(out position, out rotation);

                wheelMeshs[i].position = position;
                wheelMeshs[i].rotation = rotation;
            }
        }

        public void OnMove(InputValue input)
        {
            moveInputVector = input.Get<Vector2>();
        }

        public void OnBreak(InputValue input)
        {
            moveBreak = input.Get<float>();
        }
    }
}