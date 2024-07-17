using UnityEngine;

namespace Enemy
{
    public class PatrolPerformer
    {
        private Transform[] _patrolPoints;

        public Vector3 ChooseRandomPatrolPoint()
        {
            return _patrolPoints[Random.Range(0, _patrolPoints.Length)].position;
        }

        //Constructor
        public PatrolPerformer(Transform[] patrolPoints)
        {
            _patrolPoints = patrolPoints;
        }
    }
}