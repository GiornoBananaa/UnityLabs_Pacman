using Audio;
using Core;
using ScoreSystem;

namespace GameStateSystem
{
    public class GameDefaultState: AState
    {
        private ScoreCounter _scoreCounter;
        private AudioPlayer _audioPlayer;
        
        public GameDefaultState(ScoreCounter scoreCounter,AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _scoreCounter = scoreCounter;
        }
        
        public override void Enter()
        {
            _audioPlayer.StopMusic();
            _scoreCounter.OnMaxScore += WinGame;
        }

        public override void Exit()
        {
            _scoreCounter.OnMaxScore -= WinGame;
        }

        private void WinGame() => Owner.ChangeState<WinGameState>();
    }
}