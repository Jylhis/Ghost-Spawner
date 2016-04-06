/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 24.02.2016
 */
using System;
using System.Collections.Generic;
using SDL2;
using System.Runtime.InteropServices;

namespace src
{
    /// <summary>
    /// Mouse buttons.
    /// </summary>
    public enum MouseButtons
    {
        /// <summary>
        /// Mouse left button.
        /// </summary>
        LEFT,
        /// <summary>
        /// Mouse middle button.
        /// </summary>
        MIDDLE,
        /// <summary>
        /// Mouse right button.
        /// </summary>
        RIGHT
    }

    public class InputHandler
    {
        private const int joystickDeadZone = 10000;
        private bool joysticksInit;
        private static InputHandler instance;
        private List<IntPtr> joysticks = new List<IntPtr>();
        private List<Tuple<Vector2D, Vector2D>> joystickValues = new List<Tuple<Vector2D, Vector2D>>();
        private List<List<bool>> buttonStates = new List<List<bool>>();
        private List<bool> mouseButtonStates = new List<bool>();
        private Vector2D mousePosition = new Vector2D(0, 0);
        private byte[] keystates;
        private int numkeys;

        /// <summary>
        /// Gets the mouse position.
        /// </summary>
        /// <value>The mouse position.</value>
        public Vector2D GetMousePosition
        {
            get
            {
                return mousePosition;
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
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

        /// <summary>
        /// Gets a value indicating whether this <see cref="src.InputHandler"/> joysticks initialised.
        /// </summary>
        /// <value><c>true</c> if joysticks initialised; otherwise, <c>false</c>.</value>
        public bool JoysticksInitialised
        {
            get
            {
                return joysticksInit;
            }
        }


        private InputHandler()
        {
            // Init keyboard
            SDL.SDL_GetKeyboardState(out numkeys);
            keystates = new byte[numkeys];

            // Init mouse
            for (int i = 0; i < 3; i++)
            {
                mouseButtonStates.Add(false);
            }
        }

        private void onKeyDown()
        {
            IntPtr tmpKeystates = SDL.SDL_GetKeyboardState(out numkeys);
            Marshal.Copy(tmpKeystates, keystates, 0, numkeys);
        }

        private void onKeyUp()
        {
            IntPtr tmpKeystates = SDL.SDL_GetKeyboardState(out numkeys);
            Marshal.Copy(tmpKeystates, keystates, 0, numkeys);
        }

        private void onMouseMove(ref SDL.SDL_Event events)
        {
            mousePosition.X = events.motion.x;
            mousePosition.Y = events.motion.y;
        }

        private void onMouseButtonDown(ref SDL.SDL_Event events)
        {
            switch (events.button.button)
            {
                case (byte)SDL.SDL_BUTTON_LEFT:
                    mouseButtonStates[(int)MouseButtons.LEFT] = true;
                    break;
                case (byte)SDL.SDL_BUTTON_MIDDLE:
                    mouseButtonStates[(int)MouseButtons.MIDDLE] = true;
                    break;
                case (byte)SDL.SDL_BUTTON_RIGHT:
                    mouseButtonStates[(int)MouseButtons.RIGHT] = true;
                    break;
                default:
                    break;
            }
        }

        private void onMouseButtonUp(ref SDL.SDL_Event events)
        {
            switch (events.button.button)
            {
                case (byte)SDL.SDL_BUTTON_LEFT:
                    mouseButtonStates[(int)MouseButtons.LEFT] = false;
                    break;
                case (byte)SDL.SDL_BUTTON_MIDDLE:
                    mouseButtonStates[(int)MouseButtons.MIDDLE] = false;
                    break;
                case (byte)SDL.SDL_BUTTON_RIGHT:
                    mouseButtonStates[(int)MouseButtons.RIGHT] = false;
                    break;
                default:
                    break;
            }
        }

        private void onJoystickAxisMove(ref SDL.SDL_Event events)
        {
            int whichOne = events.jaxis.which;
            switch (events.jaxis.axis)
            {
                case 0:  // left stick move left or right
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
                    break;
                case 1:  // left stick move up or down
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
                    break;
                case 3:  // right stick move left or right
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
                    break;
                case 4:  // right stick move up or down
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
                    break;
                default:
                    break;
            }
        }

        private void onJoystickButtonDown(ref SDL.SDL_Event events)
        {
            int whichOne = events.jaxis.which;
            buttonStates[whichOne][events.jbutton.button] = true;
        }

        private void onJoystickButtonUp(ref SDL.SDL_Event events)
        {
            int whichOne = events.jaxis.which;
            buttonStates[whichOne][events.jbutton.button] = false;
        }

        /// <summary>
        /// Gets the button states.
        /// </summary>
        /// <returns><c>true</c>, if button is down, <c>false</c> if button is up.</returns>
        /// <param name="joy">Joystick.</param>
        /// <param name="buttonNumber">Button number.</param>
        public bool GetButtonStates(int joy, int buttonNumber)
        {
            return buttonStates[joy][buttonNumber];
        }

        /// <summary>
        /// Gets the state of the mouse button.
        /// </summary>
        /// <returns><c>true</c>, if mouse button is down, <c>false</c> if button is up.</returns>
        /// <param name="buttonNumber">Button number.</param>
        public bool GetMouseButtonState(MouseButtons buttonNumber)
        {
            return mouseButtonStates[(int)buttonNumber];
        }

        /// <summary>
        /// Reset this instance.
        /// </summary>
        public void Reset()
        {
            mouseButtonStates[(int)MouseButtons.LEFT] = false;
            mouseButtonStates[(int)MouseButtons.MIDDLE] = false;
            mouseButtonStates[(int)MouseButtons.RIGHT] = false;
        }

        /// <summary>
        /// Is the key down.
        /// </summary>
        /// <returns><c>true</c>, if key is down, <c>false</c> if key is up.</returns>
        /// <param name="key">Key.</param>
        public bool IsKeyDown(SDL.SDL_Scancode key)
        {
            if (keystates != null)
            {
                if (keystates[(int)key] == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets x value of specified joystick and a stick.
        /// </summary>
        /// <param name="joy">Joystick.</param>
        /// <param name="stick">Stick.</param>
        public int Xvalue(int joy, int stick)
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

        /// <summary>
        /// Gets y value of specified joystick and a stick.
        /// </summary>
        /// <param name="joy">Joystick.</param>
        /// <param name="stick">Stick.</param>
        public int Yvalue(int joy, int stick)
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

        /// <summary>
        /// Inits the joysticks.
        /// </summary>
        public void InitJoysticks()
        {
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

        /// <summary>
        /// Catches events.
        /// </summary>
        public void Update()
        {
            SDL.SDL_Event events;
            while (SDL.SDL_PollEvent(out events) != 0)
                switch (events.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        Game.Instance.IsRunning = false;
                        break;
                    case SDL.SDL_EventType.SDL_JOYAXISMOTION:
                        onJoystickAxisMove(ref events);
                        break;
                    case SDL.SDL_EventType.SDL_JOYBUTTONDOWN:
                        onJoystickButtonDown(ref events);
                        break;
                    case SDL.SDL_EventType.SDL_JOYBUTTONUP:
                        onJoystickButtonUp(ref events);
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEMOTION:
                        onMouseMove(ref events);
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        onMouseButtonDown(ref events);
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        onMouseButtonUp(ref events);
                        break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        onKeyDown();
                        break;
                    case SDL.SDL_EventType.SDL_KEYUP:
                        onKeyUp();
                        break;
                    default:
                        break;
                }
        }

        /// <summary>
        /// Clean this instance.
        /// </summary>
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
    }
}
