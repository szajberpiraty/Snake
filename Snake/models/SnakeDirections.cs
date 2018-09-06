namespace Snake.models
{
    //Az enum tulajdonképpen egész számokat használ, és 0 az alapértelmezett értéke. Tetszőleges értékek megadhatóak.
    //Ha olyan alapértelmezett érték kell, ami nem szól bele a többi értékbe, akkor adunk egy None-t
    public enum SnakeDirections
    {
        None, //Alapértelmezett irány
        Left,
        Right,
        Up,
        Down
    }
}