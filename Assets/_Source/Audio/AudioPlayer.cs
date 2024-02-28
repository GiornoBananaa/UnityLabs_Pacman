using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        
        private Dictionary<string,Sound> _sounds;
        private Dictionary<string,AudioSource> _soundSources;
        
        public bool MusicIsMuted { get; private set; }
        public bool SoundIsMuted { get; private set; }
        public float SoundVolume { get; private set; }
        public float MusicVolume { get; private set; }

        public void Construct(Sound[] sounds)
        {
            _soundSources = new Dictionary<string, AudioSource>();
            _sounds = new Dictionary<string, Sound>();
            SoundVolume = PlayerPrefs.GetFloat("SoundVolume",0.5f);
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume",0.5f);
            
            foreach (Sound sound in sounds)
            {
                AddSound(sound);
            }
            
            SoundVolumeChange(SoundVolume);
            MusicVolumeChange(MusicVolume);
        }
        
        
        public void SoundVolumeChange(float volume)
        {
            SoundVolume = volume;
            foreach (Sound sound in _sounds.Values)
            {
                if(sound.IsMusic) continue;
                
                sound.SetSourceVolume(_soundSources[sound.Name],SoundVolume);
            }
        }
        
        public void MusicVolumeChange(float volume)
        {
            MusicVolume = volume;
            foreach (Sound sound in _sounds.Values)
            {
                if(!sound.IsMusic) continue;
                sound.SetSourceVolume(_soundSources[sound.Name],MusicVolume);
            }
        }
    
        public void EnableSound(bool enable)
        {
            SoundIsMuted = enable;
            foreach (AudioSource source in _soundSources.Values)
            {
                if(source == _musicSource) continue;
                    source.mute = !enable;
            }
        }
        
        public void EnableMusic(bool enable)
        {
            MusicIsMuted = enable;
            
            _musicSource.mute = !enable;
        }
        
        private void AddSound(Sound sound)
        {
            _sounds.Add(sound.Name,sound);
            if (sound.IsMusic)
            {
                _soundSources.Add(sound.Name, _musicSource);
            }
            else
            {
                AudioSource source = new GameObject(sound.Name + "Source").AddComponent<AudioSource>();
                source.transform.parent = gameObject.transform;
                _soundSources.Add(sound.Name, source);
            }
            if (sound.PlayOnAwake)
            {
                Play(sound);
            }
        }
        
        public void Play(Sound sound)
        {
            Play(sound.Name);
        }
        
        public void Stop(string name)
        {
            if (_soundSources.TryGetValue(name, out var source))
            {
                Sound sound = _sounds[name];
                if (source == _musicSource && sound.Clip != _musicSource.clip)
                {
                    return;
                }
                source.Stop();
            }
            else
                Debug.LogWarning("Sound " + name + " not found");
        }
        
        public void StopMusic()
        {
            _musicSource.Stop();
        }
        
        public void Play(string name)
        {
            if (_soundSources.TryGetValue(name, out var source))
            {
                Sound sound = _sounds[name];
                sound.SetSourceVariables(source,sound.IsMusic?MusicVolume:SoundVolume);
                source.Play();
            }
            else
            {
                Debug.LogWarning("Sound " + name + " not found");
            }
        }
        
        private void OnDestroy()
        {
            Debug.Log("Save"+MusicVolume);
            PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
            PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
            PlayerPrefs.Save();
        }
    }
}
