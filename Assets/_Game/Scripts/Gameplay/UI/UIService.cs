using UnityEngine;

namespace Game
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private GameplayUIView _gameplayUIView;

        public GameplayUIView GameplayUIView => _gameplayUIView;
    }
}
