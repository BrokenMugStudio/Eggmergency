
using BrokenMugStudioSDK;
using UnityEngine;

namespace Eggmergency.Scripts.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "EggmergencyConfig", menuName = "Configs/Eggmergency/GameConfig")]
    public class GameConfig : SingletonScriptableObject<GameConfig>
    {
        [SerializeField]private Color[] _playerColors;
        public float PlayerInstanceWidth=1.5f;
        
        [Header("Sound Effects")]
        public AudioClip CatchEggSFX;
        public AudioClip CatchBombSFX;
        public Color GetPlayerColor(int index)
        {
            if (index < _playerColors.Length)
            {
                return _playerColors[index];
            }

            return Color.blue;
        }
    }
}
