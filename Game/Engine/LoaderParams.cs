// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
namespace src
{
    public class LoaderParams
    {
        private int x, y, w, h;
        private string id;

        /// <summary>
        /// Initializes a new instance of the <see cref="src.LoaderParams"/> class.
        /// </summary>
        /// <param name="inx">X.</param>
        /// <param name="iny">Y.</param>
        /// <param name="inw">Width.</param>
        /// <param name="inh">Height.</param>
        /// <param name="inid">ID.</param>
        public LoaderParams(int inx, int iny, int inw, int inh, string inid)
        {
            x = inx;
            y = iny;
            w = inw;
            h = inh;
            id = inid;
        }

        /// <summary>
        /// Gets the x.
        /// </summary>
        /// <value>The x.</value>
        public int X
        {
            get
            {
                return x;
            }
        }

        /// <summary>
        /// Gets the y.
        /// </summary>
        /// <value>The y.</value>
        public int Y
        {
            get
            {
                return y;
            }
        }

        /// <summary>
        /// Gets the w.
        /// </summary>
        /// <value>The w.</value>
        public int W
        {
            get
            {
                return w;
            }
        }

        /// <summary>
        /// Gets the h.
        /// </summary>
        /// <value>The h.</value>
        public int H
        {
            get
            {
                return h;
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id
        {
            get
            {
                return id;
            }
        }
    }
}
