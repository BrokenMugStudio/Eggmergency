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
        
        private eGameState _gameState;
        private float _levelDurration;
        private int _eggCount;
        public eGameState GameState
        {
            get{return _gameState;}
            set
            {
                _gameState = value;
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
            GameState = eGameState.Idle;
            _levelDurration = _levelTimeline.GetLastEventTime() + 5;
            _levelController.Initialize(_levelTimeline);
            for (int i = 0; i < _playerInstances.Length; i++)
            {
                _playerInstances[i].Initialize();
            }
        }
        private void Update()
        {
            if (GameState == eGameState.Playing)
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
            GameState = eGameState.Playing;
            
        }

        private void CompleteGame()
        {
            GameState = eGameState.Completed;

        }
    }
}
