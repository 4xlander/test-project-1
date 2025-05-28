using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class DronePathView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navAgent;
        [SerializeField] private LineRenderer _lineRenderer;
        [Space]
        [SerializeField] private Color _pathColor = Color.cyan;
        [SerializeField] private float _lineWidth = 0.1f;
        [SerializeField] private float _updateInterval = 0.1f;

        private bool showPath;
        private float lastUpdateTime;

        private void Start()
        {
            SetupLineRenderer();
        }

        public void Tick()
        {
            if (!showPath || _navAgent == null)
                return;

            if (Time.time - lastUpdateTime > _updateInterval)
            {
                DrawPath();
                lastUpdateTime = Time.time;
            }
        }

        public void SetPathVisibility(bool visible)
        {
            showPath = visible;
            if (_lineRenderer != null)
            {
                _lineRenderer.enabled = visible;
            }

            if (!visible)
            {
                ClearPath();
            }
        }

        public void UpdatePathColor(Color newColor)
        {
            _pathColor = newColor;
            if (_lineRenderer != null)
            {
                _lineRenderer.startColor = _pathColor;
                _lineRenderer.endColor = _pathColor;
            }
        }

        private void DrawPath()
        {
            if (_navAgent == null || !_navAgent.hasPath)
            {
                ClearPath();
                return;
            }

            var path = _navAgent.path;
            var pathCorners = path.corners;

            if (pathCorners.Length < 2)
            {
                ClearPath();
                return;
            }

            _lineRenderer.positionCount = pathCorners.Length;

            for (int i = 0; i < pathCorners.Length; i++)
            {
                var point = pathCorners[i];
                _lineRenderer.SetPosition(i, point);
            }
        }

        private void ClearPath()
        {
            if (_lineRenderer != null)
                _lineRenderer.positionCount = 0;
        }
        
        private void SetupLineRenderer()
        {
            _lineRenderer.startColor = _pathColor;
            _lineRenderer.endColor = _pathColor;
            _lineRenderer.startWidth = _lineWidth;
            _lineRenderer.endWidth = _lineWidth;
            _lineRenderer.useWorldSpace = true;
            _lineRenderer.positionCount = 0;
        }
    }
}
