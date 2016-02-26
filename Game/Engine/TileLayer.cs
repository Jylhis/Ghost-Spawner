// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;

namespace src
{
    public class TileLayer : Layer
    {
        private int numColumnsm, numRows, tileSize;
        private Vector2D position, velocity;
        private List<Tileset> tilesets;
        private List<List<int>> tileIDs;


        /// <summary>
        /// Initializes a new instance of the <see cref="src.TileLayer"/> class.
        /// </summary>
        /// <param name="tileSize">Tile size.</param>
        /// <param name="tilesets">Tilesets.</param>
        public TileLayer(int tileSize, ref List<Tileset> tilesets)
        {
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        public override void update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Render this instance.
        /// </summary>
        public override void render()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the tile I ds.
        /// </summary>
        /// <param name="data">Data.</param>
        public void setTileIDs(ref List<List<int>> data)
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

