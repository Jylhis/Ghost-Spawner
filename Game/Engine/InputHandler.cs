// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using SDL2;

namespace src
{
    public enum mouse_buttons
    {
        LEFT = 0,
        MIDDLE = 1,
        RIGHT = 2
    }

    public enum xbox_controller_buttons
    {
        X = 2,
        Y = 3,
        B = 1,
        A = 0,
        LB = 4,
        RB = 5,
        SELECT = 6,
        START = 7,
        XBOX = 8,
        LEFT_THUMB = 9,
        RIGHT_THUMB = 10
    }

    public class InputHandler
    {
        // FIXME: Hiiri position ei toimi kunnolla

        const int joystickDeadZone = 10000;

        private bool joysticksInit;
        private static InputHandler instance;
        private List<IntPtr> joysticks;
        private List<Tuple<Vector2D, Vector2D>> joystickValues;
        private List<List<bool>> buttonStates;
        private List<bool> mouseButtonStates;
        private Vector2D mousePosition;
        private IntPtr keystates;
        private int numkeys;

        public Vector2D getMousePosition
        {
            get
            {
                Console.WriteLine("x:" + mousePosition.X);
                return mousePosition; 
            }
        }

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
            // Init mouse
            mouseButtonStates = new List<bool>();
            mousePosition = new Vector2D(0, 0);

            for (int i = 0; i < 3; i++)
            {
                mouseButtonStates.Add(false);
            }
        }

        public void InitJoysticks()
        {
            // Init Joystick
            if (SDL.SDL_WasInit(SDL.SDL_INIT_JOYSTICK) == 0)
            {
                SDL.SDL_InitSubSystem(SDL.SDL_INIT_JOYSTICK);
                joysticks = new List<IntPtr>();
                joystickValues = new List<Tuple<Vector2D, Vector2D>>();
                buttonStates = new List<List<bool>>();
            }
            if (SDL.SDL_NumJoysticks() > 0)
            {
                for (int i = 0; i < SDL.SDL_NumJoysticks(); i++)
                {
                    IntPtr joy = SDL.SDL_JoystickOpen(i);
                    if (joy != IntPtr.Zero)
                    {
                        joysticks.Add(joy);
                        joystickValues.Add(Tuple.Create(new Vector2D(0, 0), new Vector2D(0, 0)));

                        List<bool> tmpButtons = new List<bool>();
                        for (int j = 0; j < SDL.SDL_JoystickNumButtons(joy); j++)
                        {
                            tmpButtons.Add(false);
                        }
                        buttonStates.Add(tmpButtons);
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

        // Onko parempi tapa tehhä näitä?
        public int xvalue(int joy, int stick)
        {
            if (joystickValues.Count > 0)
            {
                if (stick == 1)
                {
                    return (int)joystickValues[joy].Item1.X;
                }
                else
                {
                    return (int)joystickValues[joy].Item2.X;
                }
            }
            return 0;
        }

        public int yvalue(int joy, int stick)
        {
            if (joystickValues.Count > 0)
            {
                if (stick == 1)
                {
                    return (int)joystickValues[joy].Item1.Y;
                }
                else
                {
                    return (int)joystickValues[joy].Item2.Y;
                }
            }
            return 0;
        }

        public bool getButtonStates(int joy, int buttonNumber)
        {
            return buttonStates[joy][buttonNumber];
        }

        public bool getMouseButtonState(mouse_buttons buttonNumber)
        { 
            return mouseButtonStates[(int)buttonNumber];
        }

        public bool isKeyDown(SDL.SDL_Scancode key)
        {
            if (keystates != IntPtr.Zero)
            {
              /*FIXME:  if (keystates[key] == 1)
                { 
                    return true;
                }
                else
                {
                    return false;
                }*/
            }
            return false;
        }

        public void Update()
        {
            SDL.SDL_Event events;
            while (SDL.SDL_PollEvent(out events) != 0)
            {
                keystates = SDL.SDL_GetKeyboardState(out numkeys);
                // FIXME switch case?

                if (events.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    Game.Instance.IsRunning = false;
                }

                // Mouse buttons
                if (events.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
                {
					
                    if (events.button.button == SDL.SDL_BUTTON_LEFT)
                    {
                        mouseButtonStates[(int)mouse_buttons.LEFT] = true;
                    }
                    if (events.button.button == SDL.SDL_BUTTON_MIDDLE)
                    {
                        mouseButtonStates[(int)mouse_buttons.MIDDLE] = true;
                    }
                    if (events.button.button == SDL.SDL_BUTTON_RIGHT)
                    {
                        mouseButtonStates[(int)mouse_buttons.RIGHT] = true;
                    }
                }
                // Mouse movement
                if (events.type == SDL.SDL_EventType.SDL_MOUSEMOTION)
                {
                    mousePosition.X = events.motion.x;
                    mousePosition.Y = events.motion.y;
                }

                if (events.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
                {
                    if (events.button.button == SDL.SDL_BUTTON_LEFT)
                    {
                        mouseButtonStates[(int)mouse_buttons.LEFT] = false;
                    }
                    if (events.button.button == SDL.SDL_BUTTON_MIDDLE)
                    {
                        mouseButtonStates[(int)mouse_buttons.MIDDLE] = false;
                    }
                    if (events.button.button == SDL.SDL_BUTTON_RIGHT)
                    {
                        mouseButtonStates[(int)mouse_buttons.RIGHT] = false;
                    }
                }

                // Joystick buttons
                if (events.type == SDL.SDL_EventType.SDL_JOYBUTTONUP)
                {
                    int whichOne = events.jaxis.which;
                    buttonStates[whichOne][events.jbutton.button] = true;
                }
                if (events.type == SDL.SDL_EventType.SDL_JOYBUTTONUP)
                {
                    int whichOne = events.jaxis.which;
                    buttonStates[whichOne][events.jbutton.button] = false;
                }

                // Joystick Axis
                if (events.type == SDL.SDL_EventType.SDL_JOYAXISMOTION)
                {
                    int whichOne = events.jaxis.which;

                    // left stick move left or right
                    if (events.jaxis.axis == 0)
                    {
                        if (events.jaxis.axisValue > joystickDeadZone)
                        {
                            joystickValues[whichOne].Item1.X = 1;
                        }
                        else if (events.jaxis.axisValue < -joystickDeadZone)
                        {
                            joystickValues[whichOne].Item1.X = -1;
                        }
                        else
                        {
                            joystickValues[whichOne].Item1.X = 0;
                        }
                    }

                    // left stick move up or down
                    if (events.jaxis.axis == 1)
                    {
                        if (events.jaxis.axisValue > joystickDeadZone)
                        {
                            joystickValues[whichOne].Item1.Y = 1;
                        }
                        else if (events.jaxis.axisValue < -joystickDeadZone)
                        {
                            joystickValues[whichOne].Item1.Y = -1;
                        }
                        else
                        {
                            joystickValues[whichOne].Item1.Y = 0;
                        }
                    }

                    // right stick move left or right
                    if (events.jaxis.axis == 3)
                    {
                        if (events.jaxis.axisValue > joystickDeadZone)
                        {
                            joystickValues[whichOne].Item2.X = 1;
                        }
                        else if (events.jaxis.axisValue < -joystickDeadZone)
                        {
                            joystickValues[whichOne].Item2.X = -1;
                        }
                        else
                        {
                            joystickValues[whichOne].Item2.X = 0;
                        }
                    }

                    // right stick move up or down
                    if (events.jaxis.axis == 4)
                    {
                        if (events.jaxis.axisValue > joystickDeadZone)
                        {
                            joystickValues[whichOne].Item2.Y = 1;
                        }
                        else if (events.jaxis.axisValue < -joystickDeadZone)
                        {
                            joystickValues[whichOne].Item2.Y = -1;
                        }
                        else
                        {
                            joystickValues[whichOne].Item1.Y = 0;
                        }
                    }
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

        public bool JoysticksInitialised
        {
            get
            {
                return joysticksInit;
            }
        }
    }
}
