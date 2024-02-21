using System;
using System.Collections;
using Core;
using GameStateSystem;
using ScoreSystem;
using UnityEditor.VersionControl;
using UnityEngine;

namespace PacmanSystem
{
    public class PacmanCollisionDetector : MonoBehaviour, IPacmanRevengeEffector
    {
        [SerializeField] private LayerMask _bonusLayerMask;
        [SerializeField] private LayerMask _bigBonusLayerMask;
        [SerializeField] private LayerMask _ghostLayerMask;
        private int _bonusScore;
        private int _ghostKillScore;
        private int _ghostKilled;
        private int _bigBonusTime;
        private bool _attackGhost;
        private Coroutine _bigBonusDeactivationCoroutine;
        private ScoreCounter _scoreCounter;
        private Health _pacmanHealth;
        private GameStateMachine _gameStateMachine;
        
        public void Construct(ScoreCounter scoreCounter, Health pacmanHealth,GameStateMachine gameStateMachine,int bigBonusTime,int ghostKillScore, int bonusScore)
        {
            _scoreCounter = scoreCounter;
            _bonusScore = bonusScore;
            _gameStateMachine = gameStateMachine;
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
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_bonusLayerMask.Contains(other.gameObject.layer))
            {
                _scoreCounter.AddScore(_bonusScore);
                other.gameObject.SetActive(false);
            }
            if (_bigBonusLayerMask.Contains(other.gameObject.layer))
            {
                _scoreCounter.AddScore(_bonusScore);
                other.gameObject.SetActive(false);
                _gameStateMachine.ChangeState<PacmanRevengeGameState>();
                _bigBonusDeactivationCoroutine = StartCoroutine(BigBonusDeactivation());
            }
            else if (_ghostLayerMask.Contains(other.gameObject.layer))
            {
                if (!_attackGhost)
                {
                    if (_pacmanHealth.LooseHeart())
                    {
                        _gameStateMachine.ChangeState<LooseGameState>();
                    }
                }
                else
                {
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
