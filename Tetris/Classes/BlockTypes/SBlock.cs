using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris {
    class SBlock : TetBlock {
        
        public SBlock(Vector2 origin, Texture2D square) : base(origin, square) {

            for(int row = 0; row < boundingBox.GetLength(1); row++) {
                for(int col = 0; col < boundingBox.GetLength(0); col++) {
                    boundingBox[row, col] = new TetSquare(square, new Vector2(90 + 30 * col, 0 + row * 30));
                }
            }

            boundingBox[0, 1].isFilled = true;
            boundingBox[0, 2].isFilled = true;
            boundingBox[1, 0].isFilled = true;
            boundingBox[1, 1].isFilled = true;
        }
    }
}
