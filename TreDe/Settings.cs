namespace TreDe
{
    public interface ISettings { }
    public class Settings:ISettings
    {
        public int WorldWidth, WorldHeight, WorldDepth;
        public int TileSize;
        public int TextureTiles;
        public int TextureTileSize;

        public string Font;
    
        public Settings()
        {
            // FONT //
            Font = "cp437T";
            TileSize = 20;
            TextureTiles = 16;
            TextureTileSize = 10;

            // WORLD //
            WorldWidth = 800;
            WorldHeight = 400;
            WorldDepth = 8;
        }
    }
}