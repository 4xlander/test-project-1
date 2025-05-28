using TMPro;
using UnityEngine;

namespace Game
{
    public class ResourceInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _captionText;
        [SerializeField] private TextMeshProUGUI _amountText;

        public void SetCaption(string text)
        {
            _captionText.text = text;
        }

        public void SetAmount(float amount)
        {
            _amountText.text = amount.ToString(/*"F:1"*/);
        }
    }
}
