using Audio;
using Core;
using GhostSystem;
using PacmanSystem;
using UI;
using UnityEngine;

namespace GameStateSystem
{
    public class WinGameState: AState
    {
        private const float _deathAnimationDuration = 2;
        
        private EndGamePanel _winPanel;
        private AudioPlayer _audioPlayer;
        private Ghost[] _ghosts;
        private Pacman _pacman;
        private Timer _gameTimer;
        private string _musicName;
        
        public WinGameState(Timer gameTimer,Pacman pacman,Ghost[] ghosts,EndGamePanel winPanel, AudioPlayer audioPlayer,string musicName)
        {
            _ghosts = ghosts;
            _pacman = pacman;
            _gameTimer = gameTimer;
            _winPanel = winPanel;
            _audioPlayer = audioPlayer;
            _musicName = musicName;
        }
        
        public override void Enter()
        {
            foreach (var ghost in _ghosts)
            {
                ghost.ChangeMovementState<NoMovementState>();
            }
            
            _pacman.EnableMovement(false);
            _gameTimer.SetTimer(_deathAnimationDuration);
            _gameTimer.OnTimerEnd += OpenWinPanel;
            _winPanel.OpenPanel();
            _audioPlayer.Play(_musicName);
        }

        public override void Exit()
        {
            foreach (var ghost in _ghosts)
            {
                ghost.SetDefaultState();
            }
            _pacman.EnableMovement(true);
            _winPanel.ClosePanel();
        }
        
        private void OpenWinPanel()
        {
            _gameTimer.OnTimerEnd -= OpenWinPanel;
            _winPanel.OpenPanel();
        }
    }
}