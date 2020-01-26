namespace TreDe
{
    public interface ISettings { }
    public class Settings:ISettings
    {
        public int TileSize;
        public Settings(int TileSize = 10)
        {
            this.TileSize = TileSize;
        }
    }
}