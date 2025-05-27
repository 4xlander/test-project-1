using UnityEngine;

namespace Game
{
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Destroys the GameObject attached to this Component
        /// </summary>
        /// <param name="Component">Component instance</param>
        public static void Destroy(this Component component)
        {
            if (component != null && component.gameObject != null)
            {
                Object.Destroy(component.gameObject);
            }
        }

        /// <summary>
        /// Destroys the GameObject after a delay
        /// </summary>
        /// <param name="Component">Component instance</param>
        /// <param name="delay">Delay in seconds before destruction</param>
        public static void Destroy(this Component component, float delay)
        {
            if (component != null && component.gameObject != null)
            {
                Object.Destroy(component.gameObject, delay);
            }
        }
    }
}
