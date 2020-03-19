using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TreDe
{
    public class PlayState : State
    {
        public int WorldWidth;
        public int WorldHeight;
        public int WorldDepth;
        public byte[,,] Terrain;        // 3D map grid of the gameworld terrain


        public PlayStateRender renderer;
        public GameObjectManager GOmanager;

        public InputHandler input;

        public Vector2 CameraPosition;

        public PhysEngine PhysE;

        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%
        //  EVENT TESTING :

        public delegate void HappeningEventHandler(object sender, HappeningArgs e); //1.declare delegate
        public event HappeningEventHandler HappeningEvent;  //2.declare event

        public void RaiseHappeningEvent(HappeningArgs args)          // 3.method to raise event
        {
            HappeningEvent?.Invoke(this, args);
        }
        // %%%%%%%%%%%%%%%%%%%%%%%%%%%


        internal void LoadFromSave(Dictionary<int, Actor> actors, Player player, Dictionary<int, Item> items, byte[,,] terrain)
        {
            GOmanager = new GameObjectManager(this);
            Terrain = terrain;

            // Load player from separate file, and insert at index 0 in
            // actorslist.
            player.GOmanager = GOmanager;  // introducing...
            GOmanager.player = player;     // and back at you..

            GOmanager.NewActor(player);

            // set correct gomanager to all restored actors, and insert into
            // actorslist:
            foreach (KeyValuePair<int, Actor> kvp in actors)
            {
                kvp.Value.GOmanager = GOmanager;
                GOmanager.NewActor(kvp.Value);
            }
           

            foreach (KeyValuePair<int, Item> kvp in items)
            {
                GOmanager.DropItemOnTerrain(kvp.Value);
            }
        }

    internal void GenerateNew()
    {
        Settings s = (Settings)Manager.Game.Services.GetService(typeof(ISettings));
        GOmanager = new GameObjectManager(this);
        GOmanager.InitializeItems();

        // World generation 
        // ================
        WorldWidth = s.WorldWidth;
        WorldHeight = s.WorldHeight;
        WorldDepth = s.WorldDepth;
        Terrain = new byte[WorldWidth, WorldHeight, WorldDepth];
        WorldGen WG = new WorldGen(WorldWidth, WorldHeight, WorldDepth);

        // Test make 100 random houses
        for (int i = 0; i < 100; i++)
        {
            WG.GenerateRandomHouse();
        }
        // populate the houses with an owner npc
        foreach (Building b in WG.Buildings)
        {
            Actor a = GOmanager.CreateRandomNPC(b.Rectangle.Center.X, b.Rectangle.Center.Y, 0);
            b.Owner = a;
        }

        GOmanager.CreateNewPlayer(15, 25, 0);

        MapReader mr = new MapReader(WG);

        Terrain = mr.ReadMap();
    }

    public override void OnEnter()
    {
        renderer = new PlayStateRender(Manager, this);
        input = (InputHandler)Manager.Game.Services.GetService(typeof(IIhandler));

        // Set the camera to zero:
        CameraPosition = Vector2.Zero;

        // Read into the raw files. Store Prefab objects in the static class
        // ReadFromRaw :
        ReadFromRaw.Read();

            // Read into the static dictionary the different bodyplans
       BodyPlans.InitializeBodyPlans();


            // new instance of the physicsEngine:
            PhysE = new PhysEngine(this);
    }


    internal bool IsBlocked(int x, int y, int z)
    {
        return TileManager.MapByteToTile[Terrain[x, y, z]].blocked;
    }

    internal bool Interact(int x, int y, int z)
    {
        switch ((TileType)Terrain[x, y, z])
        {
            case TileType.DoorClosed:
                {
                    Structure s = TileManager.MapStringToStructure["DoorOpen"];
                    for (int layer = 0; layer < s.tiles.Length; layer++)
                    {
                        Terrain[x, y, layer] = (byte)s.tiles[layer];
                    }
                    RaiseHappeningEvent(new HappeningArgs(TypeOfComponent.PHYSIC, "DOOR OPENED"));
                    return true;
                }
            case TileType.DoorOpen:
                {
                    Structure s = TileManager.MapStringToStructure["DoorClosed"];
                    for (int layer = 0; layer < s.tiles.Length; layer++)
                    {
                        Terrain[x, y, layer] = (byte)s.tiles[layer];
                    }
                    RaiseHappeningEvent(new HappeningArgs(TypeOfComponent.PHYSIC, "DOOR CLOSED"));
                    return true;
                }

            default: { break; }
        }

        return false;

    }

    public override void Update(GameTime gameTime)
    {
        if (input.WasKeyPressed(Keys.Escape))
        {
            Manager.Push(new MainMenuState());
        }

        if (input.WasKeyPressed(Keys.Up)) { GOmanager.player.Move(0, -1, 0); }
        if (input.WasKeyPressed(Keys.Down)) { GOmanager.player.Move(0, 1, 0); }
        if (input.WasKeyPressed(Keys.Left)) { GOmanager.player.Move(-1, 0, 0); }
        if (input.WasKeyPressed(Keys.Right)) { GOmanager.player.Move(1, 0, 0); }
        if (input.WasKeyPressed(Keys.OemPlus)) { GOmanager.player.Move(0, 0, 1); }
        if (input.WasKeyPressed(Keys.OemMinus)) { GOmanager.player.Move(0, 0, -1); }

        if (input.WasKeyPressed(Keys.G)) { GOmanager.player.PickupItem(); }
        if (input.WasKeyPressed(Keys.I)) { Manager.Push(new ItemManagerState(GOmanager.player)); }

        if (input.IsKeyPressed(Keys.Space))
        {
            PhysE.AddWaterTop(GOmanager.player.position.X + 3,
                              GOmanager.player.position.Y);
        }

        PhysE.Update(gameTime);
        renderer.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        renderer.Draw(spriteBatch);
    }
}
}
