using System;
using DG.Tweening;
using Eggmergency.Scripts.ScriptableConfigs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Eggmergency.Scripts.UI
{
    public class PlayerDisplayUI:MonoBehaviour
    {
        private PlayerInstanceController _playerInstance;
        [SerializeField]private TextMeshProUGUI _playerNameText;
        [SerializeField]private TextMeshProUGUI _playerScoreText;
        [SerializeField]private Image _playerNameBgImage;
        private Color _playerColor;

        private void OnEnable()
        {
            GameEvents.OnScoreChanged += OnScoreChanged;
        }

      

        private void OnDisable()
        {
            GameEvents.OnScoreChanged -= OnScoreChanged;
        }
        private void OnScoreChanged(PlayerInstanceController player, int score)
        {
            if (_playerInstance == player)
            {
                _playerScoreText.text = score.ToString();
                _playerScoreText.transform.DOKill();
                _playerScoreText.transform.localScale = Vector3.one;
                _playerScoreText.transform.DOPunchScale(Vector3.one*.15f,.25f );
            }
        }

        public void Initialize(int playerIndex,PlayerInstanceController player)
        {
            _playerInstance=player;
            _playerNameText.text =player.PlayerType==ePlayerType.Player? "P"+(playerIndex+1):"COM";
            _playerScoreText.text = "0";
            _playerColor=GameConfig.Instance.GetPlayerColor(playerIndex);
            _playerNameBgImage.color = _playerColor;
            _playerScoreText.color=_playerColor;

        }

       
    }
}