using System;
using Eggmergency.Scripts.Data;
using Eggmergency.Scripts.Enums;

namespace Eggmergency.Scripts
{
    public static class GameEvents
    {
        //Game State Events
        public static Action<eGameState> GameStateChanged ;
        
        public static void TriggerGameStateChanged(eGameState gameState)=>GameStateChanged?.Invoke(gameState);
        
        
        //Level Events
        public static Action<TimelineEvent> SpawnObject;
        public static Action<PlayerCharacterController> OnCatchEgg;
        public static Action<PlayerCharacterController> OnCatchBomb;
        public static Action<PlayerInstanceController,int> OnScoreChanged;
        public static Action<int> EggCountChanged;


        public static void TriggerSpawnObject(TimelineEvent timelineEvent)=>SpawnObject?.Invoke(timelineEvent);
        public static void TriggerCatchEgg(PlayerCharacterController player)=>OnCatchEgg?.Invoke(player);
        public static void TriggerCatchBomb(PlayerCharacterController player)=>OnCatchBomb?.Invoke(player);
        public static void TriggerScoreChange(PlayerInstanceController playerInstance, int score)=>OnScoreChanged?.Invoke(playerInstance, score);
        public static void TriggerEggCountChange(int count)=>EggCountChanged?.Invoke(count);

        
        //UI Events

        public static Action StartButtonClicked;
        public static Action ReplayButtonClicked;
        
        public static void TriggerStartButtonClicked() => StartButtonClicked?.Invoke();
        public static void TriggerReplayClicked() => ReplayButtonClicked?.Invoke();


       
    }
}