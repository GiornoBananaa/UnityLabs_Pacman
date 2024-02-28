using System;
using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private AudioPlayer _audioPlayer;
        
        private void Start()
        {
            _musicSlider.value = _audioPlayer.MusicVolume;
            _soundSlider.value = _audioPlayer.SoundVolume;

            _musicSlider.onValueChanged.AddListener(_audioPlayer.MusicVolumeChange);
            _soundSlider.onValueChanged.AddListener(_audioPlayer.SoundVolumeChange);
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
        }
        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}
