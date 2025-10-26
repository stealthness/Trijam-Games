namespace _Scripts.Managers
{
    public class GameTurn
    {
        private TurnType state = TurnType.NoTurn;
        
        
        public TurnType GetCurrentTurn()
        {
            return state;
        }
        
        public void SetTurn(TurnType newTurn)
        {
            state = newTurn;
        }
        
        public void SwitchTurn()
        {
            state = state switch
            {
                TurnType.PlayerTurn => TurnType.EnemyTurn,
                TurnType.EnemyTurn => TurnType.PlayerTurn,
                _ => state
            };
        }
        
        public void ResetTurn()
        {
            state = TurnType.NoTurn;
        }
        
        public void StartPlayerTurn()
        {
            state = TurnType.PlayerTurn;
        }
        
        public void StartEnemyTurn()
        {
            state = TurnType.EnemyTurn;
        }
    }
    
    
    public enum TurnType
    {
        PlayerTurn,
        EnemyTurn,
        NoTurn
    }
}