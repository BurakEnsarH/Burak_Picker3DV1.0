using System.Collections.Generic;
using System.Linq;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Transform> layers = new List<Transform>();

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.onClosePanel += OnClosePanel;
            CoreUISignals.Instance.onCloseAllPanels += OnCloseAllPanels;
        }

        private void UnSubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
            CoreUISignals.Instance.onClosePanel -= OnClosePanel;
            CoreUISignals.Instance.onCloseAllPanels -= OnCloseAllPanels;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        [Button("OnOpenPanel")]
        private void OnOpenPanel(UIPanelTypes panelType, int layerValue)
        {
            OnClosePanel(layerValue);
            Instantiate(Resources.Load<GameObject>($"Screens/{panelType.ToString()}Panel"), layers[layerValue]);
        }

        [Button("OnClosePanel")]
        private void OnClosePanel(int layerValue)
        {
            if (layers[layerValue].childCount > 0)
                Destroy(layers[layerValue].GetChild(0).gameObject);
        }

        [Button("OnCloseAllPanels")]
        private void OnCloseAllPanels()
        {
            foreach (var t in layers.Where(t => t.childCount > 0))
            {
                Destroy(t.GetChild(0).gameObject);
            }
        }
    }
}