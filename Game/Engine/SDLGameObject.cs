// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using SDL2;

namespace src
{
    public class SDLGameObject : GameObject
    {
        /// <summary>
        /// The w.
        /// </summary>
        /// <summary>
        /// The h.
        /// </summary>
        /// <summary>
        /// The current row.
        /// </summary>
        /// <summary>
        /// The current frame.
        /// </summary>
        public int w, h, currentRow, currentFrame;
        /// <summary>
        /// The position.
        /// </summary>
        /// <summary>
        /// The velocity.
        /// </summary>
        /// <summary>
        /// The acceleration.
        /// </summary>
        public Vector2D position, velocity, acceleration;
        /// <summary>
        /// The identifier.
        /// </summary>
        public string id;

        /// <summary>
        /// Initializes a new instance of the <see cref="src.SDLGameObject"/> class.
        /// </summary>
        /// <param name="pParams">P parameters.</param>
        public SDLGameObject(ref LoaderParams pParams)
            : base(ref pParams)
        {
            position = new Vector2D(pParams.X, pParams.Y);
            velocity = new Vector2D(0, 0);
            acceleration = new Vector2D(0, 0);
            this.w = pParams.W;
            this.h = pParams.H;
            this.id = pParams.Id;

            currentFrame = 1;
            currentRow = 1;
        }

        /// <summary>
        /// Draw this instance.
        /// </summary>
        public override void Draw()
        {
            if (velocity.X < 0)
            {
                TextureManager.Instance.DrawFrame(id,
                    (int)position.X, (int)position.Y,
                    w, h, currentRow, currentFrame,
                    Game.Instance.getRenderer, SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL);
            }
            else
            {
                TextureManager.Instance.DrawFrame(id,
                    (int)position.X, (int)position.Y,
                    w, h, currentRow, currentFrame,
                    Game.Instance.getRenderer);
            }

            //TextureManager.Instance.DrawFrame(id, (int)position.X, (int)position.Y, w, h, currentRow, currentFrame, Game.Instance.getRenderer);
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        public override void Update()
        {
            velocity += acceleration;
            position += velocity;
        }

        /// <summary>
        /// Clean this instance.
        /// </summary>
        public override void Clean()
        {
        }


    }
}
