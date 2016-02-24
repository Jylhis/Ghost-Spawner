// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace src
{
    public class GameObject
    {

        //protected SDL.SDL_Rect sizePos;
        //protected int currentFrame;
       // protected int currentRow;
      //  protected string id;


        protected GameObject(ref LoaderParams pParams) { }

    
    public virtual void Draw()
    {
        //Console.WriteLine ("Draw: "+this);
        //FIXME: TextureManager.Instance.DrawFrame(this.id, this.sizePos.x, this.sizePos.y, this.sizePos.w, this.sizePos.h, this.currentRow, this.currentFrame, Game.Instance.getRenderer);
    }
    
    public virtual void Update()
    {
        //Console.WriteLine ("Updated: "+this);

    }
    
        public virtual void Clean()
        {
            Console.WriteLine("Cleaned: " + this);
        }
    }
}
