using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TreDe
{
    [Serializable]
    public class TestSave
    {
        public string Name;
    }
    public class SaveState : State
    {
        SingleScreenRender render;
        int savepart;
        bool finished;
        PlayState playState;

        public SaveState(PlayState playState)
        {
            finished = false;
            this.playState = playState;
        }

        public override void OnEnter()
        {
            render = new SingleScreenRender(Manager);
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Update(GameTime gameTime)
        {
            savepart++;
            switch (savepart)
            {
                case 1: { SaveTerrain(); break; }
                case 2: { SaveActors(); break; }
                case 3: { SaveItems(); break; }
                case 4: { finished = true; break; }
            }

            if (finished)
            {
                // Hack to signal save game exists
                File.WriteAllText("save", "Savegameexists");
                Manager.Pop();
                Manager.Push(new MainMenuState());
            }
        }

        private void SaveItems()
        {
            render.display.WriteLine("Saving items :");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("Items", FileMode.Create, FileAccess.Write);

            bf.Serialize(fs, playState.GOmanager.ItemDictionary);

            fs.Close();
        }

        private void SaveActors()
        {
            render.display.WriteLine("Saving Actors...");

            // Remove player form ActorsList. Player is saved as a separate
            // file.
          
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("Actors", FileMode.Create, FileAccess.Write);
            FileStream ps = new FileStream("Player", FileMode.Create, FileAccess.Write);

            bf.Serialize(fs, playState.GOmanager.ActorsDictionary);
            bf.Serialize(ps, playState.GOmanager.player);

            fs.Close();
            ps.Close();
        }

        private void SaveTerrain()
        {

            render.display.WriteLine("Saving terrain :");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("Terrain", FileMode.Create, FileAccess.Write);


            bf.Serialize(fs, playState.Terrain);

            fs.Close();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch);
            base.Draw(spriteBatch);

        }
    }
}
