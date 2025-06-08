using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Runtime.CrowdDemo
{
    public class Demo : MonoBehaviour
    {
        [Range(0, 10)] public int agentCount;
        public Agent agentPrefab;
        public Stage stage;

        private readonly List<Agent> _agentList = new();

        private void Update()
        {
            EnsureAgentCount();

            foreach (var agent in _agentList)
            {
                UpdateAgentDestination(agent);
            }
        }

        private void EnsureAgentCount()
        {
            while (agentCount < _agentList.Count)
            {
                var agent = _agentList[^1];
                _agentList.Remove(agent);
                Destroy(agent.gameObject);
            }

            while (_agentList.Count < agentCount)
            {
                var agent = Instantiate(agentPrefab, transform);
                _agentList.Add(agent);
            }
        }

        private void UpdateAgentDestination(Agent agent)
        {
            if (agent.DestinationTransform && !agent.IsArrived)
            {
                return;
            }

            agent.DestinationTransform = stage.WaypointList[Random.Range(0, stage.WaypointList.Count)];
        }
    }
}
