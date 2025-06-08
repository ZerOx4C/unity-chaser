using System;
using UnityEngine;

namespace Runtime.CrowdDemo
{
    public class ForwardMover : MonoBehaviour
    {
        [Range(0, 10)] public float maxSpeed;

        private Vector3 _desiredDirection;
        private float _desiredSpeed;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void SetDesiredVelocity(Vector3 velocity)
        {
            _desiredSpeed = velocity.magnitude;

            if (velocity != Vector3.zero)
            {
                _desiredDirection = velocity.normalized;
            }
        }

        private void FixedUpdate()
        {
            if (_desiredSpeed == 0)
            {
                _rigidbody.linearVelocity = Vector3.zero;
                return;
            }

            var ratio = Vector3.Dot(transform.forward, _desiredDirection);
            var speed = Mathf.Min(_desiredSpeed, maxSpeed);
            _rigidbody.linearVelocity = ratio * speed * _desiredDirection;
        }
    }
}
