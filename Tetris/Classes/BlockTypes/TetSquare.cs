using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris {
    public class TetSquare {
        public Vector2 squarePosition = Vector2.Zero;
        public Texture2D square;
        public bool isFilled;

        public TetSquare() {
            square = null;
            isFilled = false;
        }

        public TetSquare(Texture2D square, Vector2 squarePosition) {
            this.square = square;
            this.squarePosition = squarePosition;
        }

        public TetSquare(Vector2 squarePosition, bool isFilled) {
            this.squarePosition = squarePosition;
            this.isFilled = isFilled;
        }

        public Texture2D getTexture() {
            return square;
        }
        
        public void moveDown(int units) {
            squarePosition.Y += units; 
        }

        public void moveLeft(int units) {
            squarePosition.X -= units; 
        }

        public void moveRight(int units) {
            squarePosition.X += units;
        }

        public Vector2 getSquarePos() {
            return squarePosition;
        }
    }
}
