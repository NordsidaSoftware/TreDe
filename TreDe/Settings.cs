namespace TreDe
{
    public interface ISettings { }
    public class Settings:ISettings
    {
        public int WorldWidth, WorldHeight, WorldDepth;
        public int TileSize;
        public Settings(int TileSize = 20, int WorldWidth = 800, int WorldHeight = 400, 
            int WorldDepth = 8)
        {
            this.TileSize = TileSize;
            this.WorldWidth = WorldWidth;
            this.WorldHeight = WorldHeight;
            this.WorldDepth = WorldDepth;
        }
    }
}