using System;
using Eggmergency.Scripts.Enums;
using Eggmergency.Scripts.ScriptableConfigs;
using UnityEngine;

namespace Eggmergency.Scripts
{
    
    public class GameController : MonoBehaviour
    {
        [SerializeField] private ScriptableLevelTimeline _levelTimeline;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private PlayerInstanceController[] _playerInstances;
        
        public static eGameState GameState;
        private float _levelDurration;
        private int _eggCount;
        public eGameState _gameState
        {
            get{return GameState;}
            set
            {
                GameState = value;
                GameEvents.TriggerGameStateChanged(value);

            }
        }
        private float _time;

        private void OnEnable()
        {
            Initialize();
            GameEvents.StartButtonClicked += StartGame;
            GameEvents.ReplayButtonClicked += ResetGame;
        }

        private void OnDisable()
        {
            GameEvents.StartButtonClicked -= StartGame;
            GameEvents.ReplayButtonClicked -= ResetGame;

        }

        private void ResetGame()
        {
            Initialize();
        }
        private void Initialize()
        {
            _gameState = eGameState.Idle;
            _levelDurration = _levelTimeline.GetLastEventTime() + 5;
            _levelController.Initialize(_levelTimeline);
            GameEvents.TriggerPlayerInstanceChanged(_playerInstances);
            for (int i = 0; i < _playerInstances.Length; i++)
            {
                _playerInstances[i].Initialize(i,_playerInstances.Length);
            }
        }
        private void Update()
        {
            if (_gameState == eGameState.Playing)
            {
                _time+= Time.deltaTime;
                _levelController.UpdateTime(_time);
                for (int i = 0; i < _playerInstances.Length; i++)
                {
                    _playerInstances[i].UpdateTime(_time);
                }

                if (_time >= _levelDurration)
                {
                    CompleteGame();
                }
                
            }
        }

        private void StartGame()
        {
            _time = 0;
            _gameState = eGameState.Playing;
            
        }

        private void CompleteGame()
        {
            _gameState = eGameState.Completed;

        }
    }
}
