using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.CrowdDemo
{
    public class Agent : MonoBehaviour
    {
        private ForwardMover _mover;
        private NavMeshAgent _navMeshAgent;
        private SmoothRotator _rotator;

        public Transform DestinationTransform { get; set; }
        public bool IsArrived { get; private set; }

        private void Awake()
        {
            _mover = GetComponent<ForwardMover>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _rotator = GetComponent<SmoothRotator>();

            _navMeshAgent.acceleration = 1000;
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
        }

        private void Update()
        {
            if (!DestinationTransform)
            {
                return;
            }

            var sqrStoppingDistance = _navMeshAgent.stoppingDistance * _navMeshAgent.stoppingDistance;
            if ((transform.position - DestinationTransform.position).sqrMagnitude <= sqrStoppingDistance)
            {
                IsArrived = true;
                return;
            }

            IsArrived = false;

            if (0.1f < (DestinationTransform.position - _navMeshAgent.destination).magnitude)
            {
                _navMeshAgent.destination = DestinationTransform.position;
            }

            if (!Mathf.Approximately(_navMeshAgent.speed, _mover.maxSpeed))
            {
                _navMeshAgent.speed = _mover.maxSpeed;
            }

            var diffPosition = _navMeshAgent.nextPosition - transform.position;
            _mover.SetDesiredVelocity(_mover.maxSpeed * diffPosition.normalized);
            _rotator.SetDesiredForward(diffPosition);

            _navMeshAgent.nextPosition = transform.position;
            transform.position = _navMeshAgent.nextPosition;
        }
    }
}
