using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris {

    //A grid of TetSquare objects, represents game board
    class TetBoard {
        TetSquare[][] tetBoard = new TetSquare[22][];
        List<int> rowsToRemove = new List<int>();
        
        public TetBoard(int gridUnit) {
            for(int row = 0; row < tetBoard.Length; row++) {
                tetBoard[row] = new TetSquare[10];
            }

            for(int row = 0; row < tetBoard.Length; row++) {
                TetSquare[] innerArray = tetBoard[row];

                for(int col = 0; col < innerArray.Length; col++) {
                    innerArray[col] = new TetSquare(new Vector2(col * gridUnit, row * gridUnit), false);
                }
            }
        }

        public void checkLineFill() {
            int rowCount = 0;

            for(int row = 0; row < tetBoard.Length; row++) {
                TetSquare[] innerArray = tetBoard[row];
                rowCount = 0;

                for(int col = 0; col < 10; col++) {
                    if(innerArray[col].isFilled) {
                        rowCount++;
                    }
                }

                if(rowCount == 10) {
                    rowsToRemove.Add(row);
                }
            }
        }

        public void removeRows() {
            foreach(int row in rowsToRemove) {
                TetSquare[] innerArray = tetBoard[row];

                for(int col = 0; col < innerArray.Length; col++) {
                    innerArray[col].isFilled = false;
                }

                for(int x = row; x > 1; x--) {
                    moveRowDown(x);
                }
            }

            rowsToRemove.Clear();
        }

        public void moveRowDown(int row) {
            TetSquare[] innerArray = tetBoard[row];
            TetSquare[] aboveArray = tetBoard[row - 1];

            for(int col = 0; col < innerArray.Length; col++) {
                innerArray[col].isFilled = aboveArray[col].isFilled;
            }
        }

        //checks if a cetain square is filled
        public Boolean isSquareFilled(int col, int row) {
            TetSquare[] innerArray = tetBoard[row];

            if(col > 9 || col < 0) {
                return false;
            }

            return innerArray[col].isFilled;
        }

        public void Update(GameTime gameTime) {
            checkLineFill();

            if(rowsToRemove.Count() > 0) {
                removeRows();
            }
        }

        public void Draw(SpriteBatch batch, Texture2D redSquare) {
            for(int row = 0; row < tetBoard.Length; row++) {
                TetSquare[] innerArray = tetBoard[row];

                for(int col = 0; col < innerArray.Length; col++) {
                    if(innerArray[col].isFilled) {
                        batch.Draw(redSquare, innerArray[col].squarePosition, Color.White);
                    }
                }
            }
        }

        public void addToGrid(TetBlock block) {
            int x, y;

            foreach(TetSquare boundingSquare in block.boundingBox){
                if(boundingSquare.isFilled) {
                    Vector2 blockPos = boundingSquare.squarePosition;

                    x = (int)blockPos.X / 30;
                    y = (int)blockPos.Y / 30;

                    try {
                        tetBoard[y][x].isFilled = true;
                    } catch(IndexOutOfRangeException i) {
                        //empty catch
                    }
                }
            }
        }
    }
}
