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

        private void parseTilesets(ref XmlElement tilesetRoot, ref List<Tileset> tilesets)
        {
            TextureManager.Instance.Load(tilesetRoot.FirstChild.Attributes["source"].Value, tilesetRoot.GetAttribute("name"), Game.Instance.getRenderer);

            Tileset tileset;
            tileset.width = tilesetRoot.FirstChild.Attributes["width"].Value;
            tileset.height = tilesetRoot.FirstChild.Attributes["height"].Value;
            tileset.FirstGridID = tilesetRoot.FirstChild.Attributes["firstgrid"].Value;
            tileset.tileWidth = tilesetRoot.FirstChild.Attributes["tilewidth"].Value;
            tileset.tileHeight = tilesetRoot.FirstChild.Attributes["tileheight"].Value;
            tileset.spacing = tilesetRoot.FirstChild.Attributes["spacing"].Value;
            tileset.margin = tilesetRoot.FirstChild.Attributes["margin"].Value;
            tileset.name = tilesetRoot.FirstChild.Attributes["name"].Value;

            tileset.numColumns = tileset.width / (tileset.tileWidth + tileset.spacing);

            tilesets.Add(tileset);
        }

        private void parseTileLayer(ref XmlElement pTileElement, List<Layer> pLayers, List<Tileset> pTilesets)
        {
            TileLayer pTileLayer = new TileLayer(tileSize, ref pTilesets);

        // tile data
        List<List<int>> data;

        string decodedIDs;
        XmlElement pDataNode;

            for (XmlElement e = (XmlElement)pTileElement.FirstChild; e != null; e = (XmlElement)e.NextSibling)
            {
                if (e.Value == "data")
                {
                    pDataNode = e;
                }
            }
            for(XmlElement e = (XmlElement)pDataNode.FirstChild; e != null; e = (XmlElement)e.NextSibling)
                {
                // FIXME:
                XmlText text = (XmlText)e.ToText;
                string t = e.InnerText;
                byte[] tmpData = Convert.FromBase64String(t);
                decodedIDs = Encoding.UTF8.GetString(tmpData);

                // TODO: s172 loppu?? zlib? tarviiko?
                List<int> layerRow = new List<int>(width);

                for(int j = 0; j < height; j++)
                {
                    data.Add(layerRow);
                }

                for(int rows = 0; rows < height; rows++)
                {
                    for(int cols = 0; cols < width; cols++)
                    {
                        data[rows][cols] = grids[rows * width + cols];

                    }
                }

                pTileLayer.setTileIDs(ref data);

                pLayers.Add(pTileLayer);
                }
        
        }
    
        
        public Level ParseLevel(ref string levelFile)
        {
        XmlDocument LevelDocument = new XmlDocument();
        LevelDocument.LoadXml(levelFile);

        Level pLevel = new Level();

        XmlElement pRoot = LevelDocument.DocumentElement;

        tileSize = pRoot.GetAttribute("tilewidth");
        width = pRoot.GetAttribute("width");
        height = pRoot.GetAttribute("height");

        // Parse tilesets
        for(XmlElement e = (XmlElement)pRoot.FirstChild; e != null; e = (XmlElement)e.NextSibling)
        {
            if (e.Value == "tileset")
            {
                parseTilesets(ref e, ref pLevel.getTilesets());
            }
        }
        // parse any object layers
        for(XmlElement e = (XmlElement)pRoot.FirstChild; e != null; e = (XmlElement)e.NextSibling)
        {
            if(e.Value == "layer")
            {
                parseTileLayer(ref e, ref pLevel.getLayers(), ref pLevel.getTilesets());
            }
        }
        return pLevel;
        }

    }
}


