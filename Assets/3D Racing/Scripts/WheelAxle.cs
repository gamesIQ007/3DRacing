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
        /// Ширина оси. Так-то можно бы и рассчитать
        /// </summary>
        [SerializeField] private float wheelWidth;

        /// <summary>
        /// Сила против переворачивания (стабилизатор поперечной устойчивости)
        /// </summary>
        [SerializeField] private float antiRollForce;

        /// <summary>
        /// Базовая прямолинейная сила трения
        /// </summary>
        [SerializeField] private float baseForwardStiffness = 1.5f;

        /// <summary>
        /// Фактор стабильности при трении вперёд
        /// </summary>
        [SerializeField] private float stabilityForwardFactor = 1.0f;

        /// <summary>
        /// Базовая боковая сила трения
        /// </summary>
        [SerializeField] private float baseSidewaysStiffness = 2.0f;

        /// <summary>
        /// Фактор стабильности при трении вбок
        /// </summary>
        [SerializeField] private float stabilitySidewaysFactor = 1.0f;

        /// <summary>
        /// Дополнительная прижимная сила колеса
        /// </summary>
        [SerializeField] private float additionalWheelDownForce;

        /// <summary>
        /// Пересечение колеса с землёй
        /// </summary>
        private WheelHit leftWheelHit;
        private WheelHit rightWheelHit;

        public bool IsMotor => isMotor;
        public bool IsSteer => isSteer;

        /// <summary>
        /// Обновление
        /// </summary>
        public void Update()
        {
            UpdateWheelHits();

            ApplyAntiRoll();
            ApplyDownForce();
            CorrectStiffness();

            SyncMeshTransform();
        }

        #region Public

        /// <summary>
        /// Применить угол поворота
        /// </summary>
        /// <param name="steerAngle">Угол</param>
        /// <param name="wheelBaseLenght">Длина колёсной базы</param>
        public void ApplySteerAngle(float steerAngle, float wheelBaseLenght)
        {
            if (isSteer == false) return;

            float radius = Mathf.Abs(wheelBaseLenght * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(steerAngle))));
            float angleSing = Mathf.Sign(steerAngle);

            if (steerAngle > 0)
            {
                leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght / (radius + wheelWidth * 0.5f)) * angleSing;
                rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght / (radius - wheelWidth * 0.5f)) * angleSing;
            }
            else if (steerAngle < 0)
            {
                leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght / (radius - wheelWidth * 0.5f)) * angleSing;
                rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght / (radius + wheelWidth * 0.5f)) * angleSing;
            }
            else
            {
                leftWheelCollider.steerAngle = 0;
                rightWheelCollider.steerAngle = 0;
            }
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
        /// Получаем среднее количество оборотов в минуту
        /// </summary>
        /// <returns>Количество оборотов в минуту</returns>
        public float GetAverageRpm()
        {
            return (leftWheelCollider.rpm + rightWheelCollider.rpm) * 0.5f;
        }

        /// <summary>
        /// Получаем радиус колеса
        /// </summary>
        /// <returns>Радиус колеса</returns>
        public float GetRadius()
        {
            return leftWheelCollider.radius;
        }

        #endregion

        #region Private

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

        /// <summary>
        /// Стабилизатор поперечной устойчивости. Шоб не кувыркалась боком
        /// </summary>
        private void ApplyAntiRoll()
        {
            //Коэффициент сжатия пружин колёс
            float travelL = 1.0f;
            float travelR = 1.0f;

            if (leftWheelCollider.isGrounded)
            {
                travelL = (-leftWheelCollider.transform.InverseTransformPoint(leftWheelHit.point).y - leftWheelCollider.radius) / leftWheelCollider.suspensionDistance;
            }
            if (rightWheelCollider.isGrounded)
            {
                travelR = (-rightWheelCollider.transform.InverseTransformPoint(rightWheelHit.point).y - rightWheelCollider.radius) / rightWheelCollider.suspensionDistance;
            }

            float forceDir = travelL - travelR;

            if (leftWheelCollider.isGrounded)
            {
                leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelCollider.transform.up * -forceDir * antiRollForce, leftWheelCollider.transform.position);
            }
            if (rightWheelCollider.isGrounded)
            {
                rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelCollider.transform.up * forceDir * antiRollForce, rightWheelCollider.transform.position);
            }
        }

        /// <summary>
        /// Прижимная сила для колёс
        /// </summary>
        private void ApplyDownForce()
        {
            if (leftWheelCollider.isGrounded)
            {
                leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * -additionalWheelDownForce * leftWheelCollider.attachedRigidbody.velocity.magnitude,
                    leftWheelCollider.transform.position);
            }
            if (rightWheelCollider.isGrounded)
            {
                rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelHit.normal * -additionalWheelDownForce * rightWheelCollider.attachedRigidbody.velocity.magnitude,
                    rightWheelCollider.transform.position);
            }
        }

        /// <summary>
        /// Корректировка общей силы трения колёс
        /// </summary>
        private void CorrectStiffness()
        {
            // Кривые трения вперёд
            WheelFrictionCurve leftForward = leftWheelCollider.forwardFriction;
            WheelFrictionCurve rightForward = rightWheelCollider.forwardFriction;

            // Кривые бокового трения
            WheelFrictionCurve leftSideways = leftWheelCollider.sidewaysFriction;
            WheelFrictionCurve rightSideways = rightWheelCollider.sidewaysFriction;

            leftForward.stiffness = baseForwardStiffness + Mathf.Abs(leftWheelHit.forwardSlip) * stabilityForwardFactor;
            rightForward.stiffness = baseForwardStiffness + Mathf.Abs(rightWheelHit.forwardSlip) * stabilityForwardFactor;

            leftSideways.stiffness = baseSidewaysStiffness + Mathf.Abs(leftWheelHit.sidewaysSlip) * stabilitySidewaysFactor;
            rightSideways.stiffness = baseSidewaysStiffness + Mathf.Abs(rightWheelHit.sidewaysSlip) * stabilitySidewaysFactor;

            leftWheelCollider.forwardFriction = leftForward;
            rightWheelCollider.forwardFriction = rightForward;

            leftWheelCollider.sidewaysFriction = leftSideways;
            rightWheelCollider.sidewaysFriction = rightSideways;
        }

        /// <summary>
        /// Обновление точек пересечения колёс с землёй
        /// </summary>
        private void UpdateWheelHits()
        {
            leftWheelCollider.GetGroundHit(out leftWheelHit);
            rightWheelCollider.GetGroundHit(out rightWheelHit);
        }

        #endregion
    }
}