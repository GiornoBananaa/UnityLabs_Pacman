using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private string _musicName;
        [SerializeField] private AudioPlayer _audioPlayer;
        [SerializeField] private Button _playButton;
        [SerializeField] private int _gameSceneIndex;
        
        private void Awake()
        {
            AudioDataSO audioDataSO = Resources.Load<AudioDataSO>("AudioDataSO");
            _audioPlayer.Construct(audioDataSO.Sounds);
            _audioPlayer.Play(_musicName);
            _playButton.onClick.AddListener(PlayGame);
        }

        private void PlayGame()
        {
            SceneManager.LoadScene(_gameSceneIndex);
        }
    }
}
