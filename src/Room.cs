// Copyright 2016 Markus Jylhänkangas, Pauli Kokkonen, Veeti Karttunen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    class Room
    {
        public int[,] walls = new int[,]
        {
            {1,1,1,1},
            {1,0,0,1},
            {1,0,0,1},
            {1,1,1,1}
        };

        public Room()
        {

        }
    }
}
