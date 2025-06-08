using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.CrowdDemo
{
    public class Agent : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private SmoothRotator _rotator;

        public Transform DestinationTransform { get; set; }
        public bool IsArrived { get; private set; }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _rotator = GetComponent<SmoothRotator>();

            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
        }

        private void Update()
        {
            if (!DestinationTransform)
            {
                return;
            }

            if (0.1f < (DestinationTransform.position - _navMeshAgent.destination).magnitude)
            {
                _navMeshAgent.destination = DestinationTransform.position;
            }

            var sqrStoppingDistance = _navMeshAgent.stoppingDistance * _navMeshAgent.stoppingDistance;
            IsArrived = (transform.position - DestinationTransform.position).sqrMagnitude <= sqrStoppingDistance;

            var direction = _navMeshAgent.nextPosition - transform.position;
            if (direction != Vector3.zero)
            {
                _rotator.SetDesiredForward(direction);
            }

            transform.position = _navMeshAgent.nextPosition;
        }
    }
}
