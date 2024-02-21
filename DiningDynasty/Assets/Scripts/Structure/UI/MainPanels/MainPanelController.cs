using Structure.Managers;
using Structure.UI.MainPanels.UIControllers;
using Structure.UI.PanelControllers;
using UnityEngine;

namespace Structure.UI.MainPanels
{
    public class MainPanelController : FadePanelController
    {
        [SerializeField] private GameState state;
        [SerializeField] private UIControllerBase uiControllerBase;

        public GameState State => state;
        
        private void OnGameStateChange(GameState gameState)
        {
            SetActive(gameState == state);
        }
        
        private void OnEnable()
        {
            GameController.OnGameStateChange += OnGameStateChange;
        }

        private void OnDisable()
        {
            GameController.OnGameStateChange -= OnGameStateChange;
        }
    }
}