// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
namespace src
{
    public class LoaderParams
    {
        private int x;
        private int y;
        private int w;
        private int h;
        private string id;
        public LoaderParams(int inx, int iny, int inw, int inh, string inid)
        {
            x = inx;
            y = iny;
            w = inw;
            h = inh;
            id = inid;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int W { get { return w; } }
        public int H { get { return h; } }
        public string Id { get { return id; } }
    }
}
