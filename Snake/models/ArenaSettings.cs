namespace Snake.models
{
    public class ArenaSettings
        
    {
        /// <summary>
        /// Játéktér vízszintes mérete
        /// </summary>
        public static int MaxX { get; } = 20;

        /// <summary>
        /// A játéktábla mérete függőleges irányban
        /// </summary>

        public static int MaxY { get; } = 20;

        /// <summary>
        /// Kezdőérték beállítása
        /// </summary>
        public static int MealsCountForStart { get; } = 8;
        public static int SnakeCountForStart { get; } = 5;
    }
}