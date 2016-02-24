// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using SDL2;

namespace src
{
	public class InputHandler
	{
		private bool joysticksInit;
		private static InputHandler instance;
		private List<IntPtr> joysticks;

		public static InputHandler Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new InputHandler();
				}
				return instance;
			}
		}

		private InputHandler()
		{

		}

		public void Update()
		{
			SDL.SDL_Event events;
			while (SDL.SDL_PollEvent(out events) != 0)
			{
				if (events.type == SDL.SDL_EventType.SDL_QUIT)
				{
					Game.Instance.Close();
				}
			}
		}

		public void Clean()
		{
			if (joysticksInit)
			{
				for (int i = 0; i < SDL.SDL_NumJoysticks(); i++)
				{
					SDL.SDL_JoystickClose(joysticks[i]);
				}
			}
		}

		public void InitJoysticks()
		{
			joysticks = new List<IntPtr>();

			if (SDL.SDL_WasInit(SDL.SDL_INIT_JOYSTICK) == 0)
			{
				SDL.SDL_InitSubSystem(SDL.SDL_INIT_JOYSTICK);
			}
			if (SDL.SDL_NumJoysticks() > 0)
			{
				for (int i = 0; i < SDL.SDL_NumJoysticks(); i++)
				{
					IntPtr joy = SDL.SDL_JoystickOpen(i);
					if (joy != IntPtr.Zero)
					{
						joysticks.Add(joy);
					}
					else
					{
						Console.WriteLine("Error adding controller: " + SDL.SDL_GetError());
					}
				}
				SDL.SDL_JoystickEventState(SDL.SDL_ENABLE);
				joysticksInit = true;

				Console.WriteLine("Initialised " + joysticks.Count + " joystick(s)");
			}
			else
			{
				joysticksInit = false;
			}
		}

		public bool JoysticksInit
		{
			get
			{
				return joysticksInit;
			}
		}
	}
}
