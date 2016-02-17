using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace Tetris {
    class BlockFactory {
        Texture2D redSquare;
        Vector2 origin;
        Stack<int> blockStack = new Stack<int>();

        public BlockFactory(Texture2D redSquare, Vector2 origin) {
            this.redSquare = redSquare;
            this.origin = origin;
            createSpawnOrder();
        }

        //creates list of 7 nonrepeating integers, representing block spawn order
        public void createSpawnOrder() {
            while(blockStack.Count != 7) {
                Random r = new Random();
                int genNum;

                do {
                    genNum = r.Next(0, 7) + 1;
                } while(blockStack.Contains(genNum));

                blockStack.Push(genNum);
            }
        }

        //pops blockStack to choose next block spawned
        public TetBlock spawnBlock() {
            //if blockStack is empty, creates a new spawn order
            if(blockStack.Count == 0) {
                createSpawnOrder();
            }

            switch(blockStack.Pop()) {
                case 1:
                    IBlock iblock = new IBlock(origin, redSquare);
                    return iblock;
                case 2:
                    JBlock jblock = new JBlock(origin, redSquare);
                    return jblock;
                case 3:
                    LBlock lblock = new LBlock(origin, redSquare);
                    return lblock;
                case 4:
                    OBlock oblock = new OBlock(origin, redSquare);
                    return oblock;
                case 5:
                    SBlock sblock = new SBlock(origin, redSquare);
                    return sblock;
                case 6:
                    TBlock tblock = new TBlock(origin, redSquare);
                    return tblock;
                case 7:
                    ZBlock zblock = new ZBlock(origin, redSquare);
                    return zblock;
                default:
                    TetBlock tetBlock = new TetBlock(origin, redSquare);
                    return tetBlock;
            }
        }
    }
}
