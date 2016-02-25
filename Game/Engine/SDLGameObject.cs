// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen

namespace src
{
    public class SDLGameObject : GameObject
    {
        public int w, h, currentRow, currentFrame;
        public Vector2D position;
        public Vector2D velocity;
        public Vector2D acceleration;
        public string id;

        public SDLGameObject(ref LoaderParams pParams) : base(ref pParams)
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

        public override void Draw()
        {
            TextureManager.Instance.DrawFrame(id, (int)position.X, (int)position.Y, w, h, currentRow, currentFrame, Game.Instance.getRenderer);
        }

        public override void Update()
        {
            velocity += acceleration;
            position += velocity;
        }

        public override void Clean()
        {
        }


    }
}
