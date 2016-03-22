// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen

namespace src
{
    public class MenuButton : SDLGameObject
    {
        private bool released;

        public delegate void callback();

        callback call;

        private enum button_state
        {
            MOUSE_OUT,
            MOUSE_OVER,
            CLICKED
        }

        public MenuButton(LoaderParams pParams, callback incall)
            : base(ref pParams)
        {
            call = new callback(incall);
            currentFrame = (int)button_state.MOUSE_OUT;
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            Vector2D mousePos = InputHandler.Instance.getMousePosition;
            if (mousePos.X < (position.X + w)
                && mousePos.X > position.X
                && mousePos.Y < (position.Y + h)
                && mousePos.Y > position.Y)
            {
                if (InputHandler.Instance.getMouseButtonState(mouse_buttons.LEFT)
                    && released)
                {
                    currentFrame = (int)button_state.CLICKED;
                    call();
                    released = false;
                }
                else if (InputHandler.Instance.getMouseButtonState(mouse_buttons.LEFT))
                {
                    released = true;
                    currentFrame = (int)button_state.MOUSE_OVER;
                }
            }
            else
            {
                currentFrame = (int)button_state.MOUSE_OUT;
            }
        }

        public override void Clean()
        {
            base.Clean();
        }
    }
}

