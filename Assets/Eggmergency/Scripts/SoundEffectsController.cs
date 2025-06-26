using System;
using Eggmergency.Scripts.ScriptableConfigs;
using UnityEngine;

namespace Eggmergency.Scripts
{
    public class SoundEffectsController:MonoBehaviour
    {
        private GameConfig _gameConfig=>GameConfig.Instance;
        [SerializeField]private AudioSource _audioSource;

        private void OnEnable()
        {
            GameEvents.OnCatchEgg += (player => PlayCatchEggSound());
            GameEvents.OnCatchBomb += (player => PlayCatchBombSound());
        }

        private void OnDisable()
        {
            GameEvents.OnCatchEgg -= (player => PlayCatchEggSound());
            GameEvents.OnCatchBomb -= (player => PlayCatchBombSound());
        }

        private void PlayCatchEggSound()
        {
            PlaySoundEffect(_gameConfig.CatchEggSFX);

        }

        private void PlayCatchBombSound()
        {
            PlaySoundEffect(_gameConfig.CatchBombSFX);
        }

        private void PlaySoundEffect(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}