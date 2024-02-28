using Audio;
using Core;
using PacmanSystem;
using ScoreSystem;
using UnityEngine;

namespace GameStateSystem
{
    public class PacmanRevengeGameState: AState
    {
        private IPacmanRevengeEffector[] _pacmanRevengeEffector;
        private AudioPlayer _audioPlayer;
        private ScoreCounter _scoreCounter;
        private string _musicName;
        
        public PacmanRevengeGameState(ScoreCounter scoreCounter,AudioPlayer audioPlayer,string musicName,params IPacmanRevengeEffector[] pacmanRevengeEffector)
        {
            _scoreCounter = scoreCounter;
            _audioPlayer = audioPlayer;
            _musicName = musicName;
            _pacmanRevengeEffector = pacmanRevengeEffector;
        }
        
        public override void Enter()
        {
            _audioPlayer.Play(_musicName);
            foreach (var effector in _pacmanRevengeEffector)
            {
                effector.EnablePacmanRevenge(true);
            }

            _scoreCounter.OnMaxScore += WinGame;
        }
        
        public override void Exit()
        {
            foreach (var effector in _pacmanRevengeEffector)
            {
                effector.EnablePacmanRevenge(false);
            }
        }

        private void WinGame()
        {
            _scoreCounter.OnMaxScore -= WinGame;
            Owner.ChangeState<WinGameState>();
        }
    }
}