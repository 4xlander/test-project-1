using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Game
{
    public class DroneView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navAgent;
        [SerializeField] private DronePathView _dronePathView;

        private Action _onDestination;

        public void Tick()
        {
            var isMoving = _navAgent.velocity.magnitude > 0.2f;

            if (isMoving)
            {
                var rotation = Quaternion.LookRotation(_navAgent.velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }

            if (!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                if (!_navAgent.hasPath || _navAgent.velocity.magnitude == 0f)
                {
                    OnDestinationReached();
                }
            }

            _dronePathView.Tick();
        }

        public void SetPathVisibility(bool visible)
        {
            _dronePathView.SetPathVisibility(visible);
        }

        public void SetSpeed(float value)
        {
            _navAgent.speed = value;
        }

        public void MoveTo(Vector3 targetPosition, Action onComplete)
        {
            _onDestination = onComplete;
            var offset = GetRandomOffset(_navAgent.transform.position, targetPosition, 0.4f, 60f);
            var position = targetPosition + offset;
            _navAgent.SetDestination(position);
        }

        private void OnDestinationReached()
        {
            var prevCallback = _onDestination;
            _onDestination = null;
            prevCallback?.Invoke();
        }

        private Vector3 GetRandomOffset(Vector3 agentPos, Vector3 targetPos, float distance, float angleDeg)
        {
            var direction = (agentPos - targetPos).normalized;

            var halfAngle = angleDeg * 0.5f;
            var randomAngle = Random.Range(-halfAngle, halfAngle);

            var rotation = Quaternion.Euler(0, randomAngle, 0);
            var offsetDir = rotation * direction;

            return offsetDir * distance;
        }
    }
}
