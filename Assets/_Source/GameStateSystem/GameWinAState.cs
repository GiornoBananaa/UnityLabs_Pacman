using System.Buffers;
using Core;
using ScoreSystem;

namespace GameStateSystem
{
    public class GameWinAState: AState
    {
        public GameWinAState()
        {
            
        }
        
        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
    
    public class GameDefaultAState: AState
    {
        private ScoreCounter _scoreCounter;
        
        public GameDefaultAState(ScoreCounter scoreCounter)
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

        private void WinGame() => Owner.ChangeState<GameWinAState>();
    }
}