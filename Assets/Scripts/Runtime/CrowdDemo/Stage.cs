using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runtime.CrowdDemo
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private Transform waypointsRoot;

        private Transform[] _waypointArray;

        public IReadOnlyList<Transform> WaypointList => _waypointArray;

        private void Awake()
        {
            _waypointArray = waypointsRoot
                .GetComponentsInChildren<Transform>()
                .Where(t => t != waypointsRoot)
                .ToArray();
        }
    }
}
