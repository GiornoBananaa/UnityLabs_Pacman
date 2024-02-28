using Audio;
using Core;
using PacmanSystem;
using UnityEngine;

namespace GameStateSystem
{
    public class PacmanRevengeGameState: AState
    {
        private IPacmanRevengeEffector[] _pacmanRevengeEffector;
        private AudioPlayer _audioPlayer;
        private string _musicName;
        
        public PacmanRevengeGameState(AudioPlayer audioPlayer,string musicName,params IPacmanRevengeEffector[] pacmanRevengeEffector)
        {
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
        }
        
        public override void Exit()
        {
            foreach (var effector in _pacmanRevengeEffector)
            {
                effector.EnablePacmanRevenge(false);
            }
        }
    }
}