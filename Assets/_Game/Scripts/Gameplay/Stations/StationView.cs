using System.Collections;
using UnityEngine;

namespace Game
{
    public class StationView : MonoBehaviour
    {
        [SerializeField] private Transform _droneSpawnPoint;
        [Space]
        [SerializeField] private Renderer _stationRenderer;
        [SerializeField] private Color _fractionColor = Color.white;
        [Space]
        [SerializeField, Range(1, 2)] private float _scaleAnimationValue = 1.1f;
        [SerializeField, Range(0, 1)] private float _scaleAnimationDuration = 0.5f;

        private Vector3 _defaultScale;

        public Color FractionColor => _fractionColor;

        private void Start()
        {
            _defaultScale = _stationRenderer.transform.localScale;
            _stationRenderer.material.color = _fractionColor;
        }

        public Transform DroneSpawnPoint => _droneSpawnPoint;

        public void AnimateResourceReceived()
        {
            StartCoroutine(ScaleAnimation());
        }

        private IEnumerator ScaleAnimation()
        {
            _stationRenderer.transform.localScale *= _scaleAnimationValue;

            yield return new WaitForSeconds(_scaleAnimationDuration / 2);

            _stationRenderer.transform.localScale = _defaultScale;
        }
    }
}
