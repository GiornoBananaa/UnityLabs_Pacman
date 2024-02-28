using System;
using Audio;
using BonusSystem;
using Core;
using GhostSystem;
using PacmanSystem;
using ScoreSystem;
using UnityEngine;

namespace GameStateSystem
{
    public class RestartGameState: AState
    {
        private Pacman _pacman;
        private Health _health;
        private ScoreCounter _scoreCounter;
        private Ghost[] _ghosts;
        private GameObject[] _bonuses;
        private SpecialBonusSpawner _specialBonusSpawner;
        private SpecialBonusListView _specialBonusListView;
        private AudioPlayer _audioPlayer;
        private string _musicName;
        
        public RestartGameState(Pacman pacman, Ghost[] ghosts, Health health, ScoreCounter scoreCounter,
            GameObject[] bonuses, SpecialBonusSpawner specialBonusSpawner,
            SpecialBonusListView specialBonusListView, AudioPlayer audioPlayer, string musicName)
        {
            _pacman = pacman;
            _ghosts = ghosts;
            _health = health;
            _scoreCounter = scoreCounter;
            _bonuses = bonuses;
            _specialBonusSpawner = specialBonusSpawner;
            _specialBonusListView = specialBonusListView;
            _audioPlayer = audioPlayer;
            _musicName = musicName;
        }
        
        public override void Enter()
        {
            _pacman.EnableMovement(true);
            _pacman.SetDefaultPosition();
            foreach (var ghost in _ghosts)
            {
                ghost.SetDefaultPosition();
            }

            foreach (var bonus in _bonuses)
            {
                bonus.SetActive(true);
            }
            
            _health.RestoreHealth();
            _scoreCounter.RestoreScore();
            _specialBonusSpawner.ResetBonusCount();
            _specialBonusListView.RestoreBonusList();
            Owner.ChangeState<GameDefaultState>();
            _audioPlayer.Play(_musicName);
        }
        
        public override void Exit() { }
    }
}