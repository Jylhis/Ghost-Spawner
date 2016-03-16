// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;

namespace src
{
    // TODO
    public class TileLayer : Layer
    {
        private int numColumns, numRows, tileSize;
        private Vector2D position, velocity;
        private List<Tileset> tilesets;
        private List<List<int>> tileIDs;


        /// <summary>
        /// Initializes a new instance of the <see cref="src.TileLayer"/> class.
        /// </summary>
        /// <param name="tileSize">Tile size.</param>
        /// <param name="tilesets">Tilesets.</param>
        public TileLayer(int itileSize, ref List<Tileset> itilesets)
        {
            tileSize = itileSize;
            tilesets = itilesets;
            Vector2D tmp = new Vector2D(0, 0);
            position = tmp;
            velocity = tmp;

            numColumns = (Game.Instance.Width / tileSize);
            numRows = (Game.Instance.Height / tileSize);
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        public override void update()
        {
            position += velocity;
        }

        /// <summary>
        /// Render this instance.
        /// </summary>
        public override void render()
        {
            int x, y, x2, y2 = 0;

            x = (int)position.X / tileSize;
            y = (int)position.Y / tileSize;

            x2 = (int)(position.Y) % tileSize;
            y2 = (int)(position.Y) % tileSize;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    int id = tileIDs[i][j + x];

                    if (id == 0)
                    {
                        continue;
                    }
                    Tileset tileset = getTilesetByID(id);

                    id--;

                    TextureManager.Instance.DrawTile(tileset.name, 2, 2, (j * tileSize) - x2,
                        (i * tileSize) - y2, tileSize, tileSize, (id - (tileset.firstGridID - 1)) / tileset.numColumns,
                        (id - (tileset.firstGridID - 1)) % tileset.numColumns, Game.Instance.getRenderer);
                }
            }
        }

        /// <summary>
        /// Sets the tile I ds.
        /// </summary>
        /// <param name="data">Data.</param>
        public void setTileIDs(List<List<int>> data)
        {
            tileIDs = data;
        }

        /// <summary>
        /// Sets the size of the tile.
        /// </summary>
        /// <param name="inTileSize">In tile size.</param>
        public void setTileSize(int inTileSize)
        {
            tileSize = inTileSize;
        }

        /// <summary>
        /// Gets the tileset by I.
        /// </summary>
        /// <returns>The tileset by I.</returns>
        /// <param name="tileID">Tile I.</param>
        public Tileset getTilesetByID(int tileID)
        {
            return tilesets[tileID];
        }


    }

}

