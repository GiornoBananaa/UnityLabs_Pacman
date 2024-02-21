using Core;
using ScoreSystem;

namespace GameStateSystem
{
    public class GameDefaultState: AState
    {
        private ScoreCounter _scoreCounter;
        
        public GameDefaultState(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }
        
        public override void Enter()
        {
            _scoreCounter.OnMaxScore += WinGame;
        }

        public override void Exit()
        {
            _scoreCounter.OnMaxScore -= WinGame;
        }

        private void WinGame() => Owner.ChangeState<WinGameState>();
    }
}