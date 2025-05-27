using UnityEngine;

namespace Game
{
    public class DroneView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;

        public Rigidbody Rigidbody => _rb;
    }
}
