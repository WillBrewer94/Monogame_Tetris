using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris {
    class IBlock : TetBlock {

        public IBlock(Vector2 origin, Texture2D square) : base(origin, square) {
            boundingBox = new TetSquare[4, 4];

            for(int row = 0; row < boundingBox.GetLength(1); row++) {
                for(int col = 0; col < boundingBox.GetLength(0); col++) {
                    boundingBox[row, col] = new TetSquare(square, new Vector2(90 + 30 * col, -30 + row * 30));
                }
            }

            boundingBox[1, 0].isFilled = true;
            boundingBox[1, 1].isFilled = true;
            boundingBox[1, 2].isFilled = true;
            boundingBox[1, 3].isFilled = true;
        }
    }
}
