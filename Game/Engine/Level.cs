// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;

namespace src
{
    // TODO
    public struct Tileset
    {
        int firstGridID, tileWidth, tileHeight, spacing, margin,
            width, height, numColumns;
        string name;
    }

    public class Level
    {
       

        private List<Layer> layers;

        /// <summary>
        /// Gets the layers.
        /// </summary>
        /// <returns>The layers.</returns>
        public List<Layer> getLayers()
        {
            return layers;
        }

        private List<Tileset> tilesets;

        /// <summary>
        /// Gets the tilesets.
        /// </summary>
        /// <returns>The tilesets.</returns>
        public List<Tileset> getTilesets()
        {
            return tilesets;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="src.Level"/> class.
        /// </summary>
        public Level()
        {
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        public void update()
        {
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].update();
            }
        }

        /// <summary>
        /// Render this instance.
        /// </summary>
        public void render()
        {
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].render();
            }
        }
    }
}

