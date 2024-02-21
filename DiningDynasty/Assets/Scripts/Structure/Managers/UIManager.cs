using System.Linq;
using Structure.UI.MainPanels;
using UnityEngine;

namespace Structure.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private MainPanelController[] mainPanels;
        [SerializeField] private AlwaysOnPanelController alwaysOnPanelController;

        public AlwaysOnPanelController AlwaysOnPanelController => alwaysOnPanelController;
        public MainPanelController GetMainPanel(GameState state) => mainPanels.FirstOrDefault(p => p.State == state);
        
        public void ActivateState(GameState state)
        {
            foreach (var panel in mainPanels)
                panel.SetActive(state == panel.State, true);
        }
    }
}