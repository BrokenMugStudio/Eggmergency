using System;
using UnityEngine;
using UnityEngine.UI;

namespace Eggmergency.Scripts.UI
{
    public class MainMenuScreen:ScreenBase
    {
        [SerializeField]private Button _startButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartButtonClicked);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartButtonClicked);
        }

        private void StartButtonClicked()
        {
            GameEvents.TriggerStartButtonClicked();
        }
    }
}