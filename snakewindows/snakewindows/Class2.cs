using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace snakewindows
{
    //Simple Factory Design Pattern for spawning the buttons
    class ButtonFactory
    {
        public IButton Create(int id)
        {
            if ((id == 1)) //if the id equals 1, make an instance 
            {
                return new GreenButton(new Vector2(1,1 ), Color.White);// return this class
            }
            if ((id == 2)) //if the id equals 2, make an instance 
            {
                return new RedButton(new Vector2(650,1), Color.White);
            }
            else
            {
                throw new Exception("wrong input mate");
            }
        }
    }
    interface IButtonVisitor
    {
        void onGreen(GreenButton Button);
        void onRed(RedButton Button);
    }
    class SetSpriteVisitor : IButtonVisitor //visitor to set the sprite, depending on which class the methods are called
    {
        private Texture2D greenbutton;
        private Texture2D redbutton;
        public SetSpriteVisitor(Texture2D greenbutton, Texture2D redbutton)
        {
            this.greenbutton = greenbutton;
            this.redbutton = redbutton;
        }
        public void onGreen(GreenButton Button) // we make a sprite for green
        {
            Button.SetSprite(greenbutton);
        }
        public void onRed(RedButton Button) // we make a sprite for red
        {
            Button.SetSprite(redbutton);
        }
    }
    interface IButton //interface for the buttons
    {
        void Draw(SpriteBatch spritebatch);
        void SetSprite(Texture2D texture);
        void Visit(IButtonVisitor v);
    }
    class GreenButton : IButton
    {
        public Texture2D greenbutton;
        private Vector2 position;
        private Color color;
        

        public GreenButton(Vector2 position, Color color) : base() //constructor for creating 
        {
            this.position = position;
            this.color = color;
        }

        public void Visit(IButtonVisitor v) //when visited, returns loads the sprite depending on the type of button
        {
            v.onGreen(this); 
        }
        public void SetSprite(Texture2D greenbutton) //sets the sprite, so the sprite will be loaded
        {
            this.greenbutton = greenbutton;
        }
        public void Draw(SpriteBatch spriteBatch) //method to draw 
        {
            spriteBatch.Draw(greenbutton, position, color);
        }

    }

    class RedButton : IButton
    {
        private Texture2D redbutton;
        private Vector2 position;
        private Color color;
        

        public RedButton(Vector2 position, Color color) : base() //constructor for creating 
        {
            this.position = position;
            this.color = color;
        }
        public void Visit(IButtonVisitor v) //when visited, returns loads the sprite depending on the type of button
        {
            v.onRed(this);
        }
        public void SetSprite(Texture2D redbutton) //sets the sprite, so the sprite will be loaded
        {
            this.redbutton = redbutton;
        }
        public void Draw(SpriteBatch spriteBatch) //method to draw 
        {
            spriteBatch.Draw(redbutton, position, color);
        }
    }
    
    
}

