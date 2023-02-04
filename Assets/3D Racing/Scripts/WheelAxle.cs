using UnityEngine;

namespace Racing
{
    [System.Serializable]

    /// <summary>
    /// Физика колёсной оси
    /// </summary>
    public class WheelAxle
    {
        /// <summary>
        /// Коллайдеры колёс
        /// </summary>
        [SerializeField] private WheelCollider leftWheelCollider;
        [SerializeField] private WheelCollider rightWheelCollider;

        /// <summary>
        /// Трансформы мешей колёс
        /// </summary>
        [SerializeField] private Transform leftWheelMesh;
        [SerializeField] private Transform rightWheelMesh;

        /// <summary>
        /// Вращающая ось
        /// </summary>
        [SerializeField] private bool isMotor;

        /// <summary>
        /// Поворачивающая ось
        /// </summary>
        [SerializeField] private bool isSteer;

        /// <summary>
        /// Обновление
        /// </summary>
        public void Update()
        {
            SyncMeshTransform();
        }

        /// <summary>
        /// Применить угол поворота
        /// </summary>
        /// <param name="steerAngle">Угол</param>
        public void ApplySteerAngle(float steerAngle)
        {
            if (isSteer == false) return;

            leftWheelCollider.steerAngle = steerAngle;
            rightWheelCollider.steerAngle = steerAngle;
        }

        /// <summary>
        /// Применить крутящий момент
        /// </summary>
        /// <param name="motorTorque">Крутящий момент</param>
        public void ApplyMotorTorque(float motorTorque)
        {
            if (isMotor == false) return;

            leftWheelCollider.motorTorque = motorTorque;
            rightWheelCollider.motorTorque = motorTorque;
        }

        /// <summary>
        /// Применить торможение
        /// </summary>
        /// <param name="brakeTorque">Крутящий момент</param>
        public void ApplyBreakTorque(float brakeTorque)
        {
            leftWheelCollider.brakeTorque = brakeTorque;
            rightWheelCollider.brakeTorque = brakeTorque;
        }

        /// <summary>
        /// Синхронизация колёс с трансформом
        /// </summary>
        private void SyncMeshTransform()
        {
            UpdateWheelTransform(leftWheelCollider, leftWheelMesh);
            UpdateWheelTransform(rightWheelCollider, rightWheelMesh);
        }

        /// <summary>
        /// Обновление колеса
        /// </summary>
        /// <param name="wheelCollider">Коллайдер</param>
        /// <param name="wheelTransform">Меш колеса</param>
        private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 position;
            Quaternion rotation;

            wheelCollider.GetWorldPose(out position, out rotation);

            wheelTransform.position = position;
            wheelTransform.rotation = rotation;
        }
    }
}