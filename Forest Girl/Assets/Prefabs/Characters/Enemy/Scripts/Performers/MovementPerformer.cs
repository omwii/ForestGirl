using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class MovementPerformer
    {
        private NavMeshAgent _meshAgent;

        //Public Methods
        public void SetTargetMovementPoint(Vector3 targetPoint)
        {
            _meshAgent.SetDestination(targetPoint);
        }

        //Constructor
        public MovementPerformer(NavMeshAgent meshAgent)
        {
            _meshAgent = meshAgent;
        }
    }
}