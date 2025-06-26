using System;
using System.Collections.Generic;
using Eggmergency.Scripts.Data;
using Eggmergency.Scripts.Enums;
using UnityEngine;

namespace Eggmergency.Scripts
{
    public class EggsAndBombsController : MonoBehaviour
    {
        [SerializeField]private ObjectPool _eggPool;
        [SerializeField]private ObjectPool _bombPool;
        [SerializeField]private float _offsetZ;
        [SerializeField]private float _offsetMultiplierX;
        [SerializeField] private float _startPositionY = 10;
        [SerializeField] private float _objectsFallSpeed = 5;
        [SerializeField] private float _playerHandPositionY = 0.424f;
        
        private Dictionary<TimelineEvent, GameObject> _liveObjects = new Dictionary<TimelineEvent, GameObject>();
        private PlayerCharacterController _player;
        private void OnEnable()
        {
            GameEvents.SpawnObject += SpawnObject;
        }

        private void OnDisable()
        {
            GameEvents.SpawnObject -= SpawnObject;
        }

        private void SpawnObject(TimelineEvent e)
        {
            switch (e.Type)
            {
                case eObjectType.Egg:
                    _liveObjects.Add(e,_eggPool.Dequeue());
                    break;
                case eObjectType.Bomb:
                    _liveObjects.Add(e,_bombPool.Dequeue());
                    break;

            }
        }

        public void Initialize(PlayerCharacterController player)
        {
            _player = player;
            _liveObjects = new Dictionary<TimelineEvent, GameObject>();
            
        }
        
        public void UpdateTime(float time)
        {
            List<TimelineEvent> eventsToRemove = new List<TimelineEvent>();
            foreach (var obj in _liveObjects)
            {
                var targetPosition = EvaluatePosition(obj.Key, time);
                obj.Value.transform.position = targetPosition;
                var handY = _playerHandPositionY + transform.position.y;
                if (targetPosition.y > handY - .1f && targetPosition.y < handY + .1f &&
                    _player.LeanValue == -obj.Key.LaneX)
                {
                     switch (obj.Key.Type)
                     {
                         case eObjectType.Egg:
                             GameEvents.TriggerCatchEgg(_player);
                             var egg = obj.Value.GetComponent<Egg>();
                             egg.TweenToAndQueue(_player.EggHolder, _eggPool);
                             break;
                         case eObjectType.Bomb:GameEvents.TriggerCatchBomb(_player);break;
                         
                     }
                    
                     //GameEvents.RaisePlayerCatch(_player, obj.Key);
                     eventsToRemove.Add(obj.Key);
                }else if (targetPosition.y < -5)
                {
                    eventsToRemove.Add(obj.Key);
                }
            }

            for (int i = 0; i < eventsToRemove.Count; ++i)
            {
                RequeueAndRemove(eventsToRemove[i],_liveObjects[eventsToRemove[i]]);

            }

        }

        private void RequeueAndRemove(TimelineEvent e, GameObject obj)
        {
            switch (e.Type)
            {
                case eObjectType.Egg:
                    //_eggPool.Queue(obj);
                    break;
                case eObjectType.Bomb:
                    _bombPool.Queue(obj);
                    break;

            }            
            _liveObjects.Remove(e);

        }

        private Vector3 EvaluatePosition(TimelineEvent objEvent,float time)
        {
            var liveTime = (time - objEvent.SpawnTime);
            var posX =( objEvent.LaneX * _offsetMultiplierX)+transform.position.x;
            var posY =_startPositionY-(liveTime*_objectsFallSpeed)+transform.position.y;
            var posZ = _offsetZ+transform.position.z;
            return new Vector3(posX, posY, posZ);
        }
         
    }
}
