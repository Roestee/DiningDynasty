﻿using TMPro;
using UnityEngine;

namespace Project.MeshOpener
{
    public class MeshOpenerUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI remainingCostText;

        public void UpdateText(int remainingAmount)
        {
            remainingCostText.text = $"{remainingAmount}";
        }
    }
}