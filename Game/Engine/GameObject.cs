// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using SDL2;

namespace src
{
	public class GameObject
	{
		protected GameObject(ref LoaderParams pParams)
		{
		}

		public virtual void Draw()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void Clean()
		{
		}
	}
}
