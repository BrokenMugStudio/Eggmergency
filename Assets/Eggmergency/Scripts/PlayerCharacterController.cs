using System;
using DG.Tweening;
using Eggmergency.Scripts.Data;
using Eggmergency.Scripts.Enums;
using UnityEngine;

namespace Eggmergency.Scripts
{
    public class PlayerCharacterController : MonoBehaviour
    {
        [SerializeField] private bool _isMine;
        [SerializeField] private LeanController _leanController;
        [SerializeField] private ParticleSystem _failFx;
       // [SerializeField] private ParticleSystem _successFx;
        public int LeanValue=>_leanController.LeanValue;
       

        private void Update()
        {
            if (!_isMine)
            {
                return;
            }
            var input=Input.GetAxisRaw("Horizontal");
            _leanController.SetLean(input);
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
    }
}
