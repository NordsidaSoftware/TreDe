using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreDe
{
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
                case 3: { finished = true; break; }
            }

          if (finished)
            {
                Manager.Pop();
                Manager.Push(new MainMenuState());
            }
        }

        private void SaveActors()
        {
            foreach (Actor actor in playState.GOmanager.ActorsList)
            {

            }
        }

        private void SaveTerrain()
        {
            
            render.display.WriteLine("Saving terrain :");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("Terrain", FileMode.Create, FileAccess.Write);

            try
            {
                bf.Serialize(fs, playState.Terrain);
            }
            catch
            {
                render.display.WriteLine("Error writing Terrain...");
            }
            finally
            {
                fs.Close();
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            render.Draw(spriteBatch);
            base.Draw(spriteBatch);
           
        }
    }
}
