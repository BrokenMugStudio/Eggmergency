using System;
using Eggmergency.Scripts.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Eggmergency.Scripts.UI
{
   public class UIController : MonoBehaviour
   {
      public MainMenuScreen MainMenuScreen;
      public GameScreen GameScreen;
      public CompleteScreen CompleteScreen;


      private void OnEnable()
      {
         GameEvents.GameStateChanged += OnGameStateChange;
      }

      private void OnDisable()
      {
         GameEvents.GameStateChanged -= OnGameStateChange;
      }

      private void OnGameStateChange(eGameState gameState)
      {
         switch (gameState)
         {
            case eGameState.Idle: ShowMainMenuScreen();
               break;
            case eGameState.Playing: ShowGameScreen();
               break;
            case eGameState.Completed: ShowCompleteScreen();
               break;
         }
      }

      private void ShowMainMenuScreen()
      {
         HideAll();
         MainMenuScreen.Show();
      }

      private void ShowGameScreen()
      {
         HideAll();
         GameScreen.Show();
      }

      private void ShowCompleteScreen()
      {
         HideAll();
         CompleteScreen.Show();
      }

      private void HideAll()
      {
         MainMenuScreen.Hide();
         GameScreen.Hide();
         CompleteScreen.Hide();
         
      }
   }
}
