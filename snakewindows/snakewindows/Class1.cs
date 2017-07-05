using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace snakewindows
{
    interface IPlayer
    {
        void Update(GameTime gametime);
    }
    public class PlayerAdapter : IPlayer
    {
        private DesktopPlayer thePlayer;

        public PlayerAdapter(DesktopPlayer newPlayer, Vector2 position)
        {
            thePlayer = newPlayer;
        }


        public void Update(GameTime gametime)
        {
            thePlayer.Update(gametime);
        }

    }
    public class AndroidPlayer : IPlayer
    {
        private Vector2 position;

        public AndroidPlayer(Vector2 position)
        {
            this.position = position;
        }
  
        public Vector2 GetPosition()
        {
            return position;
        }

        public void Update(GameTime gametime)
        {

            TouchCollection touchCollection = TouchPanel.GetState();//gets the state of the touch inputs
            if (touchCollection.Count > 0)//just a loop
            {
                if (touchCollection[0].State == TouchLocationState.Moved)//if there is a press, call the method MoveTest (which is still a test)
                {
                    if (touchCollection[0].Position.Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 && this.position.Y < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 225)//not complete cause it doesn't move.
                    {
                        this.position.Y += 25;//move 10px to the right
                    }
                    if (touchCollection[0].Position.Y < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 && this.position.Y > 0)
                    {
                        this.position.Y -= 25;
                    }
                }
            }
        }
    }

    public class DesktopPlayer : IPlayer
    {
        private Vector2 position;

        public DesktopPlayer(Vector2 position)
        {
            this.position = position;

        }


 
        public void Update(GameTime gametime)
        {
            // TODO: Add your update logic here
            KeyboardState kbstate = Keyboard.GetState();
            if (kbstate.IsKeyDown(Keys.Left) == true && this.position.X > 0) //if the left key is pressed and the position isn't at the edge...
            {
                this.position.X -= 10; //move 10px to the left
            }

            if (kbstate.IsKeyDown(Keys.Right) == true && this.position.X + 666 < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)//if the right key is pressed and the position isn't at the edge...
            {
                this.position.X += 10; //move 10pc to the right
            }
        }
    }

}
