using System;
using Eggmergency.Scripts.ScriptableConfigs;
using UnityEngine;

namespace Eggmergency.Scripts
{
    public class PlayerInstanceController : MonoBehaviour
    {
        [SerializeField] private PlayerCharacterController _player;
        [SerializeField] private EggsAndBombsController _eggsAndBombsController;
        private int _currentScore;
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
            _player.PlayerCatchEgg();
        }
        private void PlayerCatchBomb(PlayerCharacterController player)
        {
            if (player != _player)return;
            _currentScore =Mathf.Max(_currentScore-5,0);
            GameEvents.TriggerScoreChange(this, _currentScore);
            _player.PlayerCatchBomb();

            
        }
        public void Initialize()
        {
            _currentScore = 0;
            _eggsAndBombsController.Initialize(_player);
        }

        public void UpdateTime(float time)
        {
            _eggsAndBombsController.UpdateTime(time);
        }
    }
}
