using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TreDe
{
   
    internal class LoadState : State
    {
        SingleScreenRender render;
        InputHandler input;

        byte[,,] terrain = new byte[800, 400, 8];
        Dictionary<int, Actor> Actors = new Dictionary<int, Actor>();
        Player Player;
        Dictionary<int, Item> Items = new Dictionary<int, Item>();

        public override void OnEnter()
        {
            render = new SingleScreenRender(Manager);
            input = (InputHandler)Manager.Game.Services.GetService(typeof(IIhandler));

            render.display.WriteLine("Load state");
            LoadItems();
            LoadActors();
            LoadTerrain();
            base.OnEnter();
        }

        private void LoadTerrain()
        {
            string filename = @"C:\Users\kroll\source\repos\TreDe\TreDe\bin\Windows\x86\Debug\Terrain";

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            try
            {
                terrain = (byte[,,])bf.Deserialize(fs);
            }
            catch
            {
                render.display.WriteLine("Error reading terrain...");
            }
            finally
            {
                fs.Close();
            }
        }

        private void LoadActors()
        {
            string filename = @"C:\Users\kroll\source\repos\TreDe\TreDe\bin\Windows\x86\Debug\Actors";
            string playerfile = @"C:\Users\kroll\source\repos\TreDe\TreDe\bin\Windows\x86\Debug\Player";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            FileStream ps = new FileStream(playerfile, FileMode.Open, FileAccess.Read);

            try
            {
                Actors = (Dictionary<int, Actor>)bf.Deserialize(fs);
                Player = (Player)bf.Deserialize(ps);

            }
            catch
            {
                render.display.WriteLine("Error reading actors...");
            }
            finally
            {
                fs.Close();
                ps.Close();
            }
        }

        private void LoadItems()
        {
            #region oldcode
            /*
            string actorfile = @"C:\Users\kroll\source\repos\TreDe\TreDe\bin\Windows\x86\Debug\Actors.dat";
            string actorstring = File.ReadAllText(actorfile);
            actors = JsonSerializer.Deserialize<Actors>(actorstring);
            
            string itemfile = @"C:\Users\kroll\source\repos\TreDe\TreDe\bin\Windows\x86\Debug\Items.dat";
            string[] itemstring = File.ReadAllLines(itemfile);
            
            foreach ( string line in itemstring)
            {
                Item item = new Item(null);
                string[] tokens = line.Split(':');
                item.Name = tokens[0].Trim();
                item.Glyph = Convert.ToInt32(tokens[1]);
                item.color = 
            }
            */
            #endregion

            string filename = @"C:\Users\kroll\source\repos\TreDe\TreDe\bin\Windows\x86\Debug\Items";
                          
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            try
            {
                Items = (Dictionary<int, Item>)bf.Deserialize(fs);
            }
            catch
            {
                render.display.WriteLine("Error reading items...");
            }
            finally
            {
                fs.Close();
            }

        }

        public override void OnExit()
        {

            base.OnExit();
        }

        public override void Update(GameTime gameTime)
        {
            if (input.WasKeyPressed(Keys.Escape))
            {
                Manager.ClearStack();
                Manager.Push(new MainMenuState());
                PlayState playState = new PlayState();
                Manager.Push(playState);
                playState.LoadFromSave(Actors, Player, Items, terrain);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

    }
}