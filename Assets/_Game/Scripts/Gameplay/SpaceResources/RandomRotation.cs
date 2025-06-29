using UnityEngine;

namespace Game
{
    public class RandomRotation : MonoBehaviour
    {
        [Tooltip("Degrees per second")]
        [SerializeField] private float _minAngleSpeed = 10f;

        [Tooltip("Degrees per second")]
        [SerializeField] private float _maxAngleSpeed = 50f;

        private Vector3 _rotationAxis;
        private float _rotationSpeed;

        private void Start()
        {
            _rotationAxis = Random.onUnitSphere;
            _rotationSpeed = Random.Range(_minAngleSpeed, _maxAngleSpeed);
        }

        private void Update()
        {
            transform.Rotate(_rotationAxis, _rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
