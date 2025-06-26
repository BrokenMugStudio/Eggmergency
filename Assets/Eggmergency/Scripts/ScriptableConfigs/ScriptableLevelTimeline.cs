using Eggmergency.Scripts.Data;
using Eggmergency.Scripts.Enums;
using UnityEngine;

namespace Eggmergency.Scripts.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "LevelTimeline", menuName = "Configs/Eggmergency/Level")]
    public class ScriptableLevelTimeline : ScriptableObject
    {
        public TimelineEvent[] TimelineEvents;

        public float GetLastEventTime()
        {
            var maxTime = 0f;
            for (int i = 0; i < TimelineEvents.Length; i++)
            {
                var t = TimelineEvents[i].SpawnTime;
                if (t > maxTime)
                {
                    maxTime = t;
                }
            }

            return maxTime;
        }

        public int GetEggCount()
        {
            return GetCount(eObjectType.Egg);
        }
        public int GetEggCount(float time)
        {
            return GetCount(eObjectType.Egg, time);
        }
        private int GetCount(eObjectType objectType)
        {
            var count = 0;
            for (int i = 0; i < TimelineEvents.Length; i++)
            {
                if (TimelineEvents[i].Type == objectType)
                {
                    count++;
                }
            }

            return count;
        }
        private int GetCount(eObjectType objectType,float time)
        {
            var count = 0;
            for (int i = 0; i < TimelineEvents.Length; i++)
            {
                if (TimelineEvents[i].Type == objectType && TimelineEvents[i].SpawnTime > time)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
