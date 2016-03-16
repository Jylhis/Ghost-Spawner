// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace src
{
    public class LevelParser
    {
        // TODO
        private int tileSize, width, height;

        private void parseTilesets(XmlElement tilesetRoot, List<Tileset> tilesets)
        {
            TextureManager.Instance.Load(tilesetRoot.FirstChild.Attributes["source"].Value, tilesetRoot.GetAttribute("name"), Game.Instance.getRenderer);

            Tileset tileset = new Tileset();
            tileset.width = int.Parse(tilesetRoot.FirstChild.Attributes["width"].Value);
            tileset.height = int.Parse(tilesetRoot.FirstChild.Attributes["height"].Value);
            tileset.firstGridID = int.Parse(tilesetRoot.Attributes["firstgid"].Value);
            tileset.tileWidth = int.Parse(tilesetRoot.Attributes["tilewidth"].Value);
            tileset.tileHeight = int.Parse(tilesetRoot.Attributes["tileheight"].Value);
            try
            {
                tileset.spacing = int.Parse(tilesetRoot.FirstChild.Attributes["spacing"].Value);
            }
            catch (System.NullReferenceException)
            {

                tileset.spacing = 0;
            }
            
            tileset.margin = int.Parse(tilesetRoot.Attributes["margin"].Value);
            tileset.name = tilesetRoot.Attributes["name"].Value;

            tileset.numColumns = tileset.width / (tileset.tileWidth + tileset.spacing);

            tilesets.Add(tileset);
        }

        private void parseTileLayer(XmlElement pTileElement, List<Layer> pLayers, List<Tileset> pTilesets)
        {
            TileLayer pTileLayer = new TileLayer(tileSize, ref pTilesets);

            // tile data
            List<List<int>> data = new List<List<int>>();

            string decodedIDs;
            XmlElement pDataNode = (XmlElement)pTileElement.FirstChild;

            for (XmlElement e = (XmlElement)pTileElement.FirstChild; e != null; e = (XmlElement)e.NextSibling)
            {
                if (e.Name == "data")
                {
                    pDataNode = e;
                }
            }

            for (XmlNode e = pDataNode.FirstChild; e != null; e = e.NextSibling)
            {
                string t = e.InnerText;
                byte[] tmpData = Convert.FromBase64String(t);
                decodedIDs = Encoding.UTF8.GetString(tmpData);
            }
               

                int numGids = width * height * sizeof(int);
                List<int> gids = new List<int>(numGids);
            // TODO: COMPRESS???
            //uncompress((Bytef*)&gids[0], &numGids, (const Bytef*)decodedIDs.c_str(), decodedIDs.size());

            /* List<int> layerRow = new List<int>(width);

             for (int j = 0; j < height; j++)
             {
                 data.Add(layerRow);
             }*/

            for (int rows = 0; rows < height; rows++)
                {
                for (int cols = 0; cols < width; cols++)
                {
                    List<int> tmp = new List<int>(rows * width + cols);
                    //data = rows * width + cols;
                    data.Add(tmp);
                    //data[rows][cols] = tmp;

                    //data[rows][cols] = gids.Capacity;
                    //data[rows][cols] = gids[rows * width + cols];

                    //data[rows].Add(rows * width + cols);
                }

            }

                pTileLayer.setTileIDs(data);

                pLayers.Add(pTileLayer);
            }

        


        public Level ParseLevel(string levelFile)
        {

            XmlDocument LevelDocument = new XmlDocument();

            Console.WriteLine("XML: " + levelFile);
            
            LevelDocument.Load(levelFile);


            Level pLevel = new Level();

            XmlElement pRoot = LevelDocument.DocumentElement;

            tileSize = int.Parse(pRoot.GetAttribute("tilewidth"));
            width = int.Parse(pRoot.GetAttribute("width"));
            height = int.Parse(pRoot.GetAttribute("height"));

            // Parse tilesets
            for (XmlElement e = (XmlElement)pRoot.FirstChild; e != null; e = (XmlElement)e.NextSibling)
            {
                if (e.Name == "tileset")
                {
                    parseTilesets(e, pLevel.getTilesets());
                }
            }
            // parse any object layers
            for (XmlElement e = (XmlElement)pRoot.FirstChild; e != null; e = (XmlElement)e.NextSibling)
            {
                if (e.Name == "layer")
                {
                    parseTileLayer(e, pLevel.getLayers(), pLevel.getTilesets());
                }
            }
            return pLevel;
        }

    }
}


