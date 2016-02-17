using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris {

    //Represents a single tetris shape, to be extended into several different objects
    public class TetBlock {
        protected Vector2 position;
        protected Texture2D square;
        protected TetSquare[] blockSquares = new TetSquare[4];
        public TetSquare[,] boundingBox = new TetSquare[3, 3];
        public TetSquare[,] prevBox;
        

        public TetBlock(Vector2 origin, Texture2D square) {
            this.position = origin;
            this.square = square;

            prevBox = boundingBox;

            for(int row = 0; row < boundingBox.GetLength(0); row++) {
                for(int col = 0; col < boundingBox.GetLength(1); col++) {
                    boundingBox[col, row] = new TetSquare();
                }
            }
        }

        public virtual void rotateRight(int n) {
            prevBox = boundingBox;
            Vector2 origin = boundingBox[0, 0].squarePosition;
            TetSquare[,] rotatedMatrix = new TetSquare[n, n];

            for(int row = 0; row < n; row++) {
                for(int col = 0; col < n; col++) {
                    rotatedMatrix[row, col] = boundingBox[n - col - 1, row];
                }
            }

            boundingBox = rotatedMatrix;
            calculateBoundingPos(origin);
            isValidRotation();
        }

        //helper method for rotating matrixes
        public void calculateBoundingPos(Vector2 origin) {
            for(int row = 0; row < boundingBox.GetLength(1); row++) {
                for(int col = 0; col < boundingBox.GetLength(0); col++) {
                    boundingBox[row, col] = new TetSquare(new Vector2(origin.X + 30 * col, origin.Y + row * 30), boundingBox[row, col].isFilled);
                }
            }
        }

        public virtual void moveDown(int units) {
            foreach(TetSquare boundingSquare in boundingBox) {
                boundingSquare.moveDown(units);
            }
        }

        public virtual void moveLeft(int units) {
            prevBox = boundingBox;

            if(isValidMove(units, "left")) {
                foreach(TetSquare boundingSquare in boundingBox) {
                    boundingSquare.moveLeft(units);
                }
            }
        }

        public virtual void moveRight(int units) {
            prevBox = boundingBox;

            if(isValidMove(units, "right")) {
                foreach(TetSquare boundingSquare in boundingBox) {
                    boundingSquare.moveRight(units);
                }
            }          
        }

        public virtual void Draw(SpriteBatch batch) {
            foreach(TetSquare boundingSquare in boundingBox) {
                if(boundingSquare.isFilled) {
                    batch.Draw(square, boundingSquare.squarePosition, Color.White);
                }
            }
        }

        //checks if movement left or right is valid
        public bool isValidMove(int units, string direction) {
            foreach(TetSquare boundingSquare in boundingBox) {
                if(boundingSquare.isFilled) {
                    if(boundingSquare.squarePosition.X + units > 270 && direction == "right") {
                        return false;
                    } else if(boundingSquare.squarePosition.X - units < 0 && direction == "left") {
                        return false;
                    } else if(boundingSquare.squarePosition.Y == 630 && direction == "down") {
                        return false;
                    }
                }
            }
            return true;
        }

        //checks to see if the current rotation doesn't move the shape out of the play area
        //sets the current bounding box to the previous bounding box if an error is found
        public void isValidRotation() {
            foreach(TetSquare block in boundingBox) {
                if(block.isFilled == true) {
                    if(block.squarePosition.X < 0 || block.squarePosition.X > 270) {
                        boundingBox = prevBox;
                    }
                    if(block.squarePosition.Y < 0 || block.squarePosition.Y > 630) {
                        boundingBox = prevBox;
                    }
                }
            }
        }

        public TetSquare getSquare(int row, int col) {
            try {
                return boundingBox[row, col];
            } catch(IndexOutOfRangeException i) {
                Debug.Write("error");
                return new TetSquare();
            }
        }

        public void setCurrBoundingBox() {
            boundingBox = prevBox;
        }

        public void setPrevBoundingBox() {
            prevBox = boundingBox;
        }

        public TetSquare[,] getBoundingBox() {
            return boundingBox;
        }

        public Vector2 getPos() {
            return position;
        }
    }
}
