using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace snakewindows
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        List<Vector2> snakePos ;
        Vector2 foodPos;
        SetSpriteVisitor s_visitor;
        Random r = new Random();
        ButtonFactory buttonfactory = new ButtonFactory();
        List<IButton> ButtonList = new List<IButton>();
        private MouseState mousestate;
        private MouseState PrevMouseState;       
        int direction = 1;
        int ldt = -1;
        int LastT = -1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ButtonList.Add(buttonfactory.Create(1));//Create an instance  
            ButtonList.Add(buttonfactory.Create(2));//Create an instance 
        }

        
        protected override void Initialize()
        {

            reset();
            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            texture = base.Content.Load<Texture2D>("box");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D greenbutton = Content.Load<Texture2D>("greenbutton");// make the texture and link
            Texture2D redbutton = Content.Load<Texture2D>("redbutton");
            s_visitor = new SetSpriteVisitor(greenbutton, redbutton);// send the texture to sprite

            foreach(IButton b in ButtonList)// for each one in the list an sprite
            {
                b.Visit(s_visitor);
            }
        }


        protected override void UnloadContent()
        {
           
        }

        void reset()// reset the game 
        {
            direction = 1;
            snakePos = new List<Vector2>();
            for (int i = 0; i < 5; i++)
                snakePos.Add(new Vector2(0, 4 - i));
            foodPos = new Vector2(
                r.Next(0, (GraphicsDevice.PresentationParameters.BackBufferWidth - 1) /16), 
                r.Next(0, (GraphicsDevice.PresentationParameters.BackBufferHeight - 1) / 16));
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && ldt != 1)// Controle the snake through assigning the keys
                direction = 0;
            else
                    if (Keyboard.GetState().IsKeyDown(Keys.Down) && ldt != 0)
                direction = 1;
            else
                        if (Keyboard.GetState().IsKeyDown(Keys.Left) && ldt != 3)
                direction = 2;
            else
                            if (Keyboard.GetState().IsKeyDown(Keys.Right) && ldt != 2)
                direction = 3;
            if (gameTime.TotalGameTime.TotalMilliseconds > LastT + 100)
            {
                
                ldt = direction;
                // making the tale goes after the head
                for (int i = snakePos.Count - 1; i > 0; i--)
                    snakePos[i] = snakePos[i - 1];
                // the result of every direction
                if (direction == 0)
                    snakePos[0] += new Vector2(0, -1);
                if (direction == 1)
                    snakePos[0] += new Vector2(0, 1);
                if (direction == 2)
                    snakePos[0] += new Vector2(-1, 0);
                if (direction == 3)
                    snakePos[0] += new Vector2(1, 0);
                // collision the snake with food
                if (snakePos[0].X == foodPos.X && snakePos[0].Y == foodPos.Y)
                {
                    snakePos.Add(snakePos[snakePos.Count - 1]);
                    foodPos = new Vector2(
                        r.Next(0, (GraphicsDevice.PresentationParameters.BackBufferWidth - 1) / 16),
                        r.Next(0, (GraphicsDevice.PresentationParameters.BackBufferHeight - 1) / 16));
                }
                //collision with itself
                for(int i = 1; i < snakePos.Count; i++)
                    if (snakePos[0].X == snakePos[i].X && snakePos[0].Y == snakePos[i].Y)
                        reset();
                // collision with border
                if (snakePos[0].X * snakePos[0].Y < 0 || 
                    snakePos[0].Y + 1 > GraphicsDevice.PresentationParameters.BackBufferHeight / 16 || 
                    snakePos[0].X + 1 > GraphicsDevice.PresentationParameters.BackBufferWidth / 16)
                    reset();

                LastT = (int)gameTime.TotalGameTime.TotalMilliseconds;
            }
            PrevMouseState = Mouse.GetState();
            mousestate = Mouse.GetState();
            if (mousestate.LeftButton == ButtonState.Pressed && PrevMouseState.LeftButton == ButtonState.Pressed)
            {
              if (mousestate.X > 650 && mousestate.Y < 53)
                    Environment.Exit(0);
              if (mousestate.X < 144 && mousestate.Y < 50)
                    reset();
            }
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            

            foreach(Vector2 pos in snakePos)
                spriteBatch.Draw(texture, new Rectangle((int)pos.X*16, (int)pos.Y*16, 15, 15), Color.Blue);
                spriteBatch.Draw(texture, new Rectangle((int)foodPos.X * 16, (int)foodPos.Y * 16, 15, 15), Color.Yellow);

            foreach (var button in ButtonList)//for every block in the list...
            {
                button.Draw(spriteBatch);//call the method draw
            }
            spriteBatch.End();
        

            base.Draw(gameTime);
        }
    }
 
}

