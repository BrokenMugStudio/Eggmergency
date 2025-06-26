using System;
using TMPro;
using UnityEngine;

namespace Eggmergency.Scripts.UI
{
    public class GameScreen:ScreenBase
    {
        [SerializeField] private TextMeshProUGUI _eggCountText;

        private void OnEnable()
        {
            GameEvents.EggCountChanged += OnEggCountChanged;
        }

        private void OnEggCountChanged(int count)
        {
            _eggCountText.text = count.ToString();
        }
    }
}