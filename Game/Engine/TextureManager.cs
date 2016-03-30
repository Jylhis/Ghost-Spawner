/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
 *
 * Tämä tiedosto on osa Olio- ja käyttöliittymien ohjelmointi kurssin harjoitustyötä.
 *
 * Created: 24.02.2016
 */
using System;
using SDL2;
using System.Collections.Generic;

namespace src
{
    public class TextureManager
    {
        private static TextureManager instance;
        private Dictionary<string, IntPtr> textureDict = new Dictionary<string, IntPtr>();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static TextureManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TextureManager();
                }
                return instance;
            }
        }

        private TextureManager()
        {
        }

        /// <summary>
        /// Load the texture and puts it into dictionary.
        /// </summary>
        /// <param name="path">Path.</param>
        /// <param name="id">Identifier.</param>
        /// <param name="Renderer">Renderer.</param>
        public bool Load(string path, string id, IntPtr Renderer)
        {
            IntPtr tempSurface = IntPtr.Zero;
            // http://stackoverflow.com/questions/22886500/rendering-text-sdl2
            /*if (id == "text")
            {
                IntPtr font = SDL_ttf.TTF_OpenFont("Resources/font.ttf", 12);
                SDL.SDL_Color color;
                color.r = 255;
                color.b = 255;
                color.g = 255;
                color.a = 0;
                tempSurface = SDL_ttf.TTF_RenderText_Solid(font, path, color);
            }
            else
            {*/
            tempSurface = SDL_image.IMG_Load(path);
                
           // }

            if (tempSurface == IntPtr.Zero)
            {
                Console.WriteLine(" - SDL_Load Error: " + SDL.SDL_GetError());
                return false;
            }

            IntPtr Texture = SDL.SDL_CreateTextureFromSurface(Renderer, tempSurface);
            SDL.SDL_FreeSurface(tempSurface);
            if (Texture != IntPtr.Zero)
            {
                textureDict[id] = Texture;
                return true;
            }
            Console.WriteLine(" - Something Wrong in loadTexture");
            return false;
        }


        /// <summary>
        /// Draw texture.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
        /// <param name="Renderer">Renderer.</param>
        /// <param name="flip">Flip.</param>
        public void Draw(string id, int x, int y, int w, int h,ref IntPtr Renderer, SDL.SDL_RendererFlip flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE)
        {
            SDL.SDL_Rect srcRect;
            SDL.SDL_Rect destRect;

            srcRect.x = 0;
            srcRect.y = 0;
            srcRect.w = destRect.w = w;
            srcRect.h = destRect.h = h;
            destRect.x = x;
            destRect.y = y;

            SDL.SDL_RenderCopyEx(Renderer, textureDict[id], ref srcRect, ref destRect, 0.0, IntPtr.Zero, flip);
        }

        /// <summary>
        /// Draws the frame.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
        /// <param name="currentRow">Current row.</param>
        /// <param name="currentFrame">Current frame.</param>
        /// <param name="Renderer">Renderer.</param>
        /// <param name="flip">Flip.</param>
        public void DrawFrame(string id, int x, int y, int w, int h, int currentRow, int currentFrame, IntPtr Renderer, SDL.SDL_RendererFlip flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE, double angle = 0.0)
        {
            SDL.SDL_Rect srcRect;
            SDL.SDL_Rect destRect;

            srcRect.x = w * currentFrame;
            srcRect.y = h * (currentRow - 1);
            srcRect.w = destRect.w = w;
            srcRect.h = destRect.h = h;
            destRect.x = x;
            destRect.y = y;

            SDL.SDL_RenderCopyEx(Renderer, textureDict[id], ref srcRect, ref destRect, angle, IntPtr.Zero, flip);
        }

        /// <summary>
        /// Clears from texture map.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public void ClearFromTextureMap(string id)
        {
            textureDict.Remove(id);
        }
    }
}

