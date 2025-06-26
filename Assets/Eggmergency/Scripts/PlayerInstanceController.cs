using System;
using Eggmergency.Scripts.ScriptableConfigs;
using UnityEngine;

namespace Eggmergency.Scripts
{
    public enum ePlayerType
    {
        Player,
        CPU
    }
    public class PlayerInstanceController : MonoBehaviour
    {
        private GameConfig _gameConfig=>GameConfig.Instance;
        [SerializeField] private PlayerCharacterController _player;
        [SerializeField] private EggsAndBombsController _eggsAndBombsController;
        private int _currentScore;
        [SerializeField] private ePlayerType _playerType;
        public ePlayerType PlayerType => _playerType;
        private void OnEnable()
        {
            GameEvents.OnCatchEgg += PlayerCatchEgg;
            GameEvents.OnCatchBomb += PlayerCatchBomb;
        }

        private void OnDisable()
        {
            GameEvents.OnCatchEgg -= PlayerCatchEgg;
            GameEvents.OnCatchBomb -= PlayerCatchBomb;
        }

        private void PlayerCatchEgg(PlayerCharacterController player)
        {
            if (player != _player)return;
            _currentScore += 1;
            GameEvents.TriggerScoreChange(this, _currentScore);

            _player.PlayerCatchEgg();
        }
        private void PlayerCatchBomb(PlayerCharacterController player)
        {
            if (player != _player)return;
            _currentScore =Mathf.Max(_currentScore-5,0);
            GameEvents.TriggerScoreChange(this, _currentScore);
            _player.PlayerCatchBomb();

            
        }
        public void Initialize(int index,int playerCount)
        {
            _currentScore = 0;
            _eggsAndBombsController.Initialize(_player);
            var totalWidth=_gameConfig.PlayerInstanceWidth*playerCount;
            var posX=(((index+1)*_gameConfig.PlayerInstanceWidth) - (totalWidth*.5f))-_gameConfig.PlayerInstanceWidth*.5f;
            transform.position=new Vector3(-posX,0,0);
            _player.SetPlayerTyper(_playerType);
        }

        public void UpdateTime(float time)
        {
            _eggsAndBombsController.UpdateTime(time);
        }
    }
}
