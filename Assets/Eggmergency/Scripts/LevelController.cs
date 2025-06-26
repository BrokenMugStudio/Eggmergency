using System.Collections.Generic;
using System.Linq;
using Eggmergency.Scripts.Data;
using Eggmergency.Scripts.Enums;
using Eggmergency.Scripts.ScriptableConfigs;
using UnityEngine;

namespace Eggmergency.Scripts
{
    public class LevelController : MonoBehaviour
    {
        private ScriptableLevelTimeline _levelTimeline;
        private List<TimelineEvent> _eventQueue;
        private List<TimelineEvent> _eventBacklog;
        private int _eggCount;
        public void Initialize(ScriptableLevelTimeline level)
        {
            _levelTimeline = level;
            _eggCount = _levelTimeline.GetEggCount();
            GameEvents.TriggerEggCountChange(_eggCount);

            _eventQueue = _levelTimeline.TimelineEvents.ToList();
            _eventBacklog=new List<TimelineEvent>();
        }

        public void UpdateTime(float time)
        {
            for (int i = 0; i < _eventQueue.Count; i++)
            {
                if (_eventQueue[i].SpawnTime <= time)
                {
                    var e=_eventQueue[i];
                    ExecuteEvent(_eventQueue[i]);
                    _eventBacklog.Add(_eventQueue[i]);
                    _eventQueue.RemoveAt(i);
                    if (e.Type == eObjectType.Egg)
                    {
                        _eggCount--;
                        GameEvents.TriggerEggCountChange(_eggCount);

                    }

                }
            }
        }

        private void ExecuteEvent(TimelineEvent e)
        {
            GameEvents.TriggerSpawnObject(e);
        }
    }
}
