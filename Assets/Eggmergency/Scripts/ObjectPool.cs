#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Eggmergency.Scripts
{
    #if UNITY_EDITOR
    [ExecuteInEditMode]
    #endif
    public class ObjectPool : MonoBehaviour
    {
        
        [SerializeField] private bool _prebake = false;
        [SerializeField] private int _prebakeCount= 10;
        [SerializeField] private Transform _poolHolder;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private List<GameObject> _queue;
        [SerializeField] private List<GameObject> _dequeued;
#if UNITY_EDITOR

        private void OnEnable()
        {
            if (_prebake && !Application.isPlaying)
            {
                Prebake();
                _prebake = false;
            }
        }

        private void Prebake()
        {
            if (_poolHolder != null)
            {
                DestroyImmediate(_poolHolder.gameObject);
            }
            _poolHolder=new GameObject(_prefab.gameObject.name+"PoolHolder").transform;
            _poolHolder.SetParent(transform);
            _queue = new List<GameObject>();
            _dequeued = new List<GameObject>();
            for (int i = 0; i < _prebakeCount; i++)
            {
                InstantiatePrefab();
            }
        }
        private void InstantiatePrefab()
        {
            GameObject obj = PrefabUtility.InstantiatePrefab(_prefab, _poolHolder) as GameObject;
            obj.SetActive(false);

            _queue.Add(obj);

        }
        
#endif

        public void Queue(GameObject obj)
        {
            _dequeued.Remove(obj);
            _queue.Add(obj);
            obj.SetActive(false);
            obj.transform.SetParent(_poolHolder);
        }

        public GameObject Dequeue()
        {
            if (_queue.Count > 0)
            {
                GameObject obj = _queue[0];
                _queue.RemoveAt(0);
                _dequeued.Add(obj);
                obj.SetActive(true);

                return obj;
            }
            else
            {
                InstantiateAndQueue();
                return Dequeue();
            }
        }

        private void InstantiateAndQueue()
        {
            GameObject obj = Instantiate(_prefab, _poolHolder);
            obj.SetActive(false);

            _queue.Add(obj);
        }

    }
}
