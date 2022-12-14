using PeggleWars.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using EnumCollection;

namespace PeggleWars.AudioOptions
{
    public class AudioOptionManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AudioMixer _audioMixer;

        #endregion

        #region Public Functions

        //Functions for Menu Sliders with an Audio Mixer Asset
        //Music and EffectSound AudioSource need to have that
        //Mixer assigned to their OutPut
        //Log volume makes slider volume change linear instead
        //of logarihtmic, because decibels are on a log scale
        public void SetMasterVolume(float volume)
        {
            _audioMixer.SetFloat("Master", volume > 0 ? Mathf.Log(volume) *20f : -80f);
        }

        public void SetMusicVolume(float volume)
        {
            _audioMixer.SetFloat("Master", volume > 0 ? Mathf.Log(volume) * 20f : -80f);
        }

        public void SetEffectsVolume(float volume)
        {
            _audioMixer.SetFloat("Master", volume > 0 ? Mathf.Log(volume) * 20f : -80f);
            //Play an exemplary SFX to give the play an auditory volume feedback
            AudioManager.Instance.PlaySoundEffect(SFX.SFX_0002_BasicPeggleHit);
        }

        #endregion

        #region Private Functions

        #endregion

    }
}
