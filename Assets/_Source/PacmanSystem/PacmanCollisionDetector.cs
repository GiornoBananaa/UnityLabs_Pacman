using System;
using System.Collections;
using Audio;
using Core;
using GameStateSystem;
using ScoreSystem;
using UnityEngine;

namespace PacmanSystem
{
    public class PacmanCollisionDetector : MonoBehaviour, IPacmanRevengeEffector
    {
        private const float _eatCooldownTime = 0.35f;
        
        [SerializeField] private string _bonusSound;
        [SerializeField] private string _bigBonusSound;
        [SerializeField] private string _specialBonusSound;
        [SerializeField] private string _ghostAttackSound;
        [SerializeField] private string _deathSound;
        [SerializeField] private LayerMask _bonusLayerMask;
        [SerializeField] private LayerMask _bigBonusLayerMask;
        [SerializeField] private LayerMask _specialBonus;
        [SerializeField] private LayerMask _ghostLayerMask;
        private int _bonusScore;
        private int _specialBonusScore;
        private int _ghostKillScore;
        private int _ghostKilled;
        private int _bigBonusTime;
        private bool _attackGhost;
        private bool _bonusEating;
        private float _eatCooldownTimeElapsed;
        private Coroutine _bigBonusDeactivationCoroutine;
        private ScoreCounter _scoreCounter;
        private Health _pacmanHealth;
        private GameStateMachine _gameStateMachine;
        private AudioPlayer _audioPlayer;
        
        public void Construct(ScoreCounter scoreCounter, Health pacmanHealth,GameStateMachine gameStateMachine,AudioPlayer audioPlayer,int bigBonusTime,int ghostKillScore, int bonusScore, int specialBonusScore)
        {
            _scoreCounter = scoreCounter;
            _bonusScore = bonusScore;
            _specialBonusScore = specialBonusScore;
            _gameStateMachine = gameStateMachine;
            _audioPlayer = audioPlayer;
            _pacmanHealth = pacmanHealth;
            _bigBonusTime = bigBonusTime;
            _ghostKillScore = ghostKillScore;
        }
        
        public void EnablePacmanRevenge(bool enable)
        {
            _ghostKilled = 0;
            _attackGhost = enable;
            if(_bigBonusDeactivationCoroutine!=null)
                StopCoroutine(_bigBonusDeactivationCoroutine);
        }

        private void Update()
        {
            _eatCooldownTimeElapsed -= Time.deltaTime;
            if(_eatCooldownTimeElapsed<0)
            {
                _audioPlayer.Stop(_bonusSound);
                _bonusEating = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            int layer = other.gameObject.layer;
            if (_bonusLayerMask.Contains(layer))
            {
                _scoreCounter.AddScore(_bonusScore);
                other.gameObject.SetActive(false);
                
                _eatCooldownTimeElapsed = _eatCooldownTime;
                
                if(!_bonusEating)
                {
                    _audioPlayer.Play(_bonusSound);
                    _bonusEating = true;
                }
                
            }
            if (_bigBonusLayerMask.Contains(layer))
            {
                _scoreCounter.AddBonusScore(_bonusScore);
                other.gameObject.SetActive(false);
                _gameStateMachine.ChangeState<PacmanRevengeGameState>();
                _bigBonusDeactivationCoroutine = StartCoroutine(BigBonusDeactivation());
                _audioPlayer.Play(_bigBonusSound);
            }
            else if (_specialBonus.Contains(layer))
            {
                _scoreCounter.AddBonusScore(_specialBonusScore);
                _audioPlayer.Play(_specialBonusSound);
            }
            else if (_ghostLayerMask.Contains(layer))
            {
                if (!_attackGhost)
                {
                    if (_pacmanHealth.LooseHeart())
                    {
                        _gameStateMachine.ChangeState<LooseGameState>();
                    }
                    else
                    {
                        _gameStateMachine.ChangeState<LooseLiveGameState>();
                    }
                    _audioPlayer.Play(_deathSound);
                }
                else
                {
                    _audioPlayer.Play(_ghostAttackSound);
                    _ghostKilled++;
                    _scoreCounter.AddBonusScore(_ghostKillScore*_ghostKilled);
                }
            }
        }

        private IEnumerator BigBonusDeactivation()
        {
            yield return new WaitForSeconds(_bigBonusTime);
            _gameStateMachine.ChangeState<GameDefaultState>();
        }
    }
}
