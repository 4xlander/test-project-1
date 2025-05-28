using TMPro;
using UnityEngine;

namespace Game
{
    public class SpaceResView : MonoBehaviour
    {
        public TextMeshPro AmountText;

        public void UpdateAmount(float amount)
        {
            AmountText.text = amount.ToString();
            AmountText.enabled = amount > 0;
        }
    }
}
