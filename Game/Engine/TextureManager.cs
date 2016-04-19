/*
 * Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen & Veeti Karttunen
 *
 * This is part of Object-Oriented and GUI Programming course assignment
 * and is licensed under MIT license, see LICENSE file.
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

        SDL.SDL_Color white;

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

        public bool renderText(string text, int x, int y, IntPtr Renderer)
        {
            white.g = 255; white.r = 255; white.b = 255; //white.a = 0;
            IntPtr defFont = SDL_ttf.TTF_OpenFont("Resources/goodTimes.ttf", 30);
            if (defFont == IntPtr.Zero)
            {
                Console.WriteLine(" - Error loading font: " + SDL.SDL_GetError());
                return false;
            }

            IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(defFont, text, white);
            if (surfaceMessage == IntPtr.Zero)
            {
                Console.WriteLine(" - Error creating surface from font: " + SDL.SDL_GetError());
                return false;
            }

            IntPtr Message = SDL.SDL_CreateTextureFromSurface(Renderer, surfaceMessage);
            if (Message != IntPtr.Zero)
            {
                //textureDict[text] = Message;
                SDL.SDL_Rect Message_rect, src_rect;
                src_rect.y = 0;
                src_rect.x = 0;
                Message_rect.x = x;
                Message_rect.y = y;
                src_rect.w = Message_rect.w = 100;
                src_rect.h = Message_rect.h = 100;
                SDL.SDL_RenderCopyEx(Renderer, Message, ref src_rect, ref Message_rect, 0.0, IntPtr.Zero, SDL.SDL_RendererFlip.SDL_FLIP_NONE);


                return true;
            }
            SDL.SDL_FreeSurface(surfaceMessage);
            Console.WriteLine(" - Error creating message: " + SDL.SDL_GetError());


            return false;
        }

        /// <summary>
        /// Load the texture and puts it into dictionary.
        /// </summary>
        /// <param name="path">Path.</param>
        /// <param name="id">Identifier.</param>
        /// <param name="Renderer">Renderer.</param>
        public bool Load(string path, string id, IntPtr Renderer)
        {
            IntPtr tempSurface = SDL_image.IMG_Load(path);
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
        public void Draw(string id, int x, int y, int w, int h, IntPtr Renderer, SDL.SDL_RendererFlip flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE)
        {
            SDL.SDL_Rect srcRect, destRect;

            srcRect.x = srcRect.y = 0;
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

