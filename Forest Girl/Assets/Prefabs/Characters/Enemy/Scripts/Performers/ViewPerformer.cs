using UnityEngine;

namespace Enemy
{
    public class ViewPerformer : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _viewRadius;
        [SerializeField] private float _viewAngle;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstructionMask;

        public bool CanSeePlayer { get; private set; }
        public Transform PlayerTransform { get { return _playerTransform; } }
        public float ViewRadius { get { return _viewRadius;} }
        public float ViewAngle { get { return _viewAngle;} }

        private void Update()
        {
            FieldOfViewCheck();
        }

        public void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _viewRadius, _targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < _viewAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                        CanSeePlayer = true;
                    else
                        CanSeePlayer = false;
                }
                else
                    CanSeePlayer = false;
            }
            else if (CanSeePlayer)
                CanSeePlayer = false;
        }
    }
}