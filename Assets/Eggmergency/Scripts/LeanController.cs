using System;
using Eggmergency.Scripts.ScriptableConfigs;
using UnityEngine;

namespace Eggmergency.Scripts
{
    public class LeanController : MonoBehaviour
    {
        private const string k_LeanAnimationKey = "Lean";
        private GameConfig _gameConfig=>GameConfig.Instance;
        
        [SerializeField] private Animator _animator;
        [SerializeField] [Range(-1,1f)]private float _input = 0;

        [SerializeField] [Range(-1,1f)]private float _leanDirection = 0;
        [SerializeField] private float _leanMovementRange =>_gameConfig.LeanMovementRange;
        [SerializeField] private float _transitionSpeed =>_gameConfig.LeanMovementSpeed;

        public int LeanValue=>Mathf.RoundToInt(_leanDirection);
    
    
        public void SetLean(float Lean)
        {
            _input = Lean;

        }
        private void Update()
        {
            _leanDirection =Mathf.Lerp(_leanDirection,_input,_transitionSpeed*Time.deltaTime); ;
            _animator.SetFloat(k_LeanAnimationKey,(_leanDirection+1)*.5f);
            transform.localPosition=-Vector3.right*(_leanDirection*_leanMovementRange);
        }
    }
}
