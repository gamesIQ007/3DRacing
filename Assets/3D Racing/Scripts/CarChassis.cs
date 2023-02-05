﻿using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(Rigidbody))]

    /// <summary>
    /// Шасси автомобиля. Физика всего автомобиля.
    /// </summary>
    public class CarChassis : MonoBehaviour
    {
        /// <summary>
        /// Колёсные оси
        /// </summary>
        [SerializeField] private WheelAxle[] wheelAxles;

        /// <summary>
        /// Длина колёсной базы
        /// </summary>
        [SerializeField] private float wheelBaseLength;

        /// <summary>
        /// Центр масс
        /// </summary>
        [SerializeField] private Transform centerOfMass;

        /// <summary>
        /// Угловое сопротивление.
        /// </summary>
        [Header("Angular Drag")]
        [SerializeField] private float angularDragMin;
        [SerializeField] private float angularDragMax;
        [SerializeField] private float angularDragFactor;

        /// <summary>
        /// Прижимная сила
        /// </summary>
        [Header("Down Force")]
        [SerializeField] private float downForceMin;
        [SerializeField] private float downForceMax;
        [SerializeField] private float downForceFactor;

        /// <summary>
        /// Линейная скорость в км/ч
        /// </summary>
        public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;

        private new Rigidbody rigidbody;

        //DEBUG

        /// <summary>
        /// Крутящий момент
        /// </summary>
        public float MotorTorque;

        /// <summary>
        /// Торможение
        /// </summary>
        public float BrakeTorque;

        /// <summary>
        /// Угол поворота
        /// </summary>
        public float SteerAngle;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            if (centerOfMass != null)
            {
                rigidbody.centerOfMass = centerOfMass.localPosition;
            }
        }

        private void FixedUpdate()
        {
            UpdateAngularDrag();

            UpdateDownForce();

            UpdateWheelAxles();
        }

        /// <summary>
        /// Обновление колёсных осей
        /// </summary>
        private void UpdateWheelAxles()
        {
            // Количество колёс с мотором
            int amountMotorWheel = 0;
            for (int i = 0; i < wheelAxles.Length; i++)
            {
                if (wheelAxles[i].IsMotor)
                {
                    amountMotorWheel += 2;
                }
            }

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                wheelAxles[i].Update();
                wheelAxles[i].ApplyMotorTorque(MotorTorque / amountMotorWheel);
                wheelAxles[i].ApplySteerAngle(SteerAngle, wheelBaseLength);
                wheelAxles[i].ApplyBreakTorque(BrakeTorque);
            }
        }

        /// <summary>
        /// Сопротивление вращению
        /// </summary>
        private void UpdateAngularDrag()
        {
            rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
        }

        /// <summary>
        /// Обновление прижимной силы
        /// </summary>
        private void UpdateDownForce()
        {
            float downForce = Mathf.Clamp(downForceFactor * LinearVelocity, downForceMin, downForceMax);
            rigidbody.AddForce(-transform.up * downForce);
        }
    }
}