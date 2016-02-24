// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace src
{
   
    public class Player : SDLGameObject
	{
        int maxVel = 1;

		public Player (LoaderParams pParams) : base(ref pParams)
		{
            //Console.WriteLine ("Init: "+this);
            TextureManager.Instance.Load("Resources/Player.bmp", "player", Game.Instance.getRenderer);
		}

		public override void Draw ()
		{
			//Console.WriteLine ("Calls gameObject Draw from: "+this);
			base.Draw ();
		}

		public override void Update ()
		{
            //Console.WriteLine ("Updated: " + this);
            currentFrame = (int)(((SDL.SDL_GetTicks()) / 100) % 2);
            acceleration.X = 1;
            base.Update();
		}

		public override void Clean ()
		{
			Console.WriteLine ("Calls gameObject Clean from: "+this);
		}
        
	}
}
