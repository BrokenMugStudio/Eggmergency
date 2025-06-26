using System;
using DG.Tweening;
using Eggmergency.Scripts.Data;
using Eggmergency.Scripts.Enums;
using UnityEngine;

namespace Eggmergency.Scripts
{
    public class PlayerCharacterController : MonoBehaviour
    {
        private ePlayerType _playerType;
        [SerializeField] private LeanController _leanController;
        [SerializeField] private ParticleSystem _failFx;
        [SerializeField]private Transform _eggHolder;
        public Transform EggHolder=>_eggHolder;
        public int LeanValue=>_leanController.LeanValue;
        
        //Bot Settings
        private float _leanChangeCooldown;
        private int[] botInputs = new[] { -1, 0, 1 };
        private void Update()
        {
            if (GameController.GameState != eGameState.Playing)
            {
                return;
            }
            if (_playerType==ePlayerType.Player)
            {
                var input=Input.GetAxisRaw("Horizontal");
                _leanController.SetLean(input);
            }else if (_playerType == ePlayerType.CPU)
            {
                if (_leanChangeCooldown <= 0)
                {
                    var fakeInput=botInputs[UnityEngine.Random.Range(0,botInputs.Length)];
                    _leanController.SetLean(fakeInput);
                    _leanChangeCooldown = UnityEngine.Random.Range(2f, 5f);
                }
                else
                {
                    _leanChangeCooldown -= Time.deltaTime;
                }
            }
           
        }

        public void PlayerCatchEgg()
        {
            transform.localScale=Vector3.one;
            transform.DOKill();
            transform.DOPunchScale(Vector3.one * .1f, .1f);
        }
        public void PlayerCatchBomb()
        {
            transform.localScale=Vector3.one;
            transform.DOKill();
            transform.DOPunchScale(new Vector3(-.1f,.2f,-.1f), .15f);
            _failFx?.Play();
        }

        public void SetPlayerTyper(ePlayerType playerType)
        {
            _playerType= playerType;   
        }
    }
}
