using System;
using UnityEngine;
using UnityEngine.UI;

namespace Eggmergency.Scripts.UI
{
    public class CompleteScreen:ScreenBase
    {
        [SerializeField]private Button _replayButton;

        private void OnEnable()
        {
            _replayButton.onClick.AddListener(OnReplayButtonClicked);
                
        }

        private void OnDisable()
        {
            _replayButton.onClick.RemoveListener(OnReplayButtonClicked);
        }

        private void OnReplayButtonClicked()
        {
            GameEvents.TriggerReplayClicked();
        }
    }
}