
using UnityEngine;

namespace Eggmergency.Scripts.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "EggmergencyConfig", menuName = "Configs/Eggmergency/GameConfig")]
    public class ScriptableGameConfig : ScriptableObject
    {
        public int EggsPerMatch = 100;
    }
}
