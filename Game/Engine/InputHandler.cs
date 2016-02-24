using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Update() { }
        public void Clean() { }

        public void InitJoysticks()
        {
            if (SDL.SDL_WasInit(SDL.SDL_INIT_JOYSTICK) == 0)
            {
                SDL.SDL_InitSubSystem(SDL.SDL_INIT_JOYSTICK);
            }
            if(SDL.SDL_NumJoysticks() > 0)
            {
                for (int i = 0; i < SDL.SDL_NumJoysticks(); i++)
                {
                    IntPtr joy = SDL.SDL_JoystickOpen(i);
                    if(SDL.SDL_JoystickOpened(i) == 1)
                    {
                        joysticks.Add(joy);
                    }
                    else
                    {
                        Console.WriteLine("Error adding controller: "+SDL.SDL_GetError());
                    }
                }
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
