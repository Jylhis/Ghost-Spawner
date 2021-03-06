﻿/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen & Veeti Karttunen
 *
 * This is part of Object-Oriented and GUI Programming course assignment
 * and is licensed under MIT license, see LICENSE file.
 *
 * Created: 25.02.2016
 */
using System;

namespace src
{
    public class MenuButton : GameObject
    {
        private bool released;

        public delegate void callback();

        callback call;

        private enum buttonState
        {
            MOUSE_OUT,
            MOUSE_OVER,
            CLICKED
        }

        public MenuButton(LoaderParams pParams, callback incall)
            : base(ref pParams)
        {
            call = new callback(incall);
            currentFrame = (int)buttonState.MOUSE_OUT;
        }

        public override void Update()
        {
            Vector2D mousePos = InputHandler.Instance.GetMousePosition;
            if (mousePos.X < (Position.X + W)
                && mousePos.X > Position.X
                && mousePos.Y < (Position.Y + H)
                && mousePos.Y > Position.Y)
            {
                if (InputHandler.Instance.GetMouseButtonState(MouseButtons.LEFT)
                    && released)
                {
                    currentFrame = (int)buttonState.CLICKED;
                    call();
                    released = false;
                }
                else if (InputHandler.Instance.GetMouseButtonState(MouseButtons.LEFT))
                {
                    released = true;
                    currentFrame = (int)buttonState.MOUSE_OVER;
                }
            }
            else
            {
                currentFrame = (int)buttonState.MOUSE_OUT;
            }
        }
    }
}

