using System;
using TMPro;
using UnityEngine;

namespace Eggmergency.Scripts.UI
{
    public class GameScreen:ScreenBase
    {
        [SerializeField] private TextMeshProUGUI _eggCountText;
        private Transform _playerDisplayHolder;
        [SerializeField]
        private PlayerDisplayUI[] _playerDisplays;
        private void OnEnable()
        {
            GameEvents.EggCountChanged += OnEggCountChanged;
            GameEvents.PlayerInstanceChanged += OnPlayerInstanceChanged;
        }

        private void OnDisable()
        {
            GameEvents.EggCountChanged -= OnEggCountChanged;
            GameEvents.PlayerInstanceChanged -= OnPlayerInstanceChanged;

        }

        private void OnEggCountChanged(int count)
        {
            _eggCountText.text = count.ToString();
        }

        private void OnPlayerInstanceChanged(PlayerInstanceController[] players)
        {
            for (int i = 0; i < _playerDisplays.Length; i++)
            {
                if (i < players.Length)
                {
                    _playerDisplays[i].gameObject.SetActive(true);
                    _playerDisplays[i].Initialize(i, players[i]);
                }
                else
                {
                    _playerDisplays[i].gameObject.SetActive(false);
                }
            }
        }
    }
}