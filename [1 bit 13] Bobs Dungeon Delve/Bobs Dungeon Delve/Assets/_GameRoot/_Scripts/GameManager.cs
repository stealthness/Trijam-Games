namespace _GameRoot._Scripts
{
    public class GameManager
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance ??= new GameManager();

        public void RestartGame()
        {
            // Implement game restart logic here
        }
    }
}