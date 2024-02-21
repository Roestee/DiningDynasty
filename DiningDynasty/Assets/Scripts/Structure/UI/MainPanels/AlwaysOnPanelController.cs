using System.Collections.Generic;
using Structure.Managers;
using Structure.UI.MainPanels.UIControllers;
using Structure.UI.PanelControllers;
using UnityEngine;

namespace Structure.UI.MainPanels
{
    public class AlwaysOnPanelController : FadePanelController
    {
        [SerializeField] private List<GameState> activeStates;
        [SerializeField] private AlwaysOnUIController alwaysOnUIController;

        public AlwaysOnUIController AlwaysOnUIController => alwaysOnUIController;

        private void OnGameStateChange(GameState state)
        {
            SetActive(activeStates.Contains(state));
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