using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;


namespace Tetris {
    public class TetrisGame : Game {
        #region Variables

        //objects
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TetBoard tetBoard;
        List<TetBlock> blockList;
        BlockFactory blockFactory;
        Texture2D squareTex;        
        TetBlock currBlock;
        Vector2 origin = Vector2.Zero;

        //game components
        InputHandler inputHandler;

        //variables
        int gridUnit = 30; 
        int screenWidth = 300;
        int screenHeight = 660;
        float elapsedTime;
        float lockDelay; //resets to one each time a rotation happens
        bool blockSpawned = false;

        #endregion

        public TetrisGame() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        #region InitializationMethods

        //handles any initialization to be done before main game loop
        protected override void Initialize() {
            //screen initialization
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            //game component initialization
            Components.Add(inputHandler = new InputHandler(this));

            base.Initialize();

            //game object initialization
            blockFactory = new BlockFactory(squareTex, origin);
            blockList = new List<TetBlock>();
            tetBoard = new TetBoard(gridUnit);
        }

        //loads any assets used by the game before main game loop begins
        protected override void LoadContent() {
            //create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            squareTex = Content.Load<Texture2D>("redsquare");
        }

        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
            //not used
        }

        #endregion

        #region GameLoopMethods

        //main game loop, runs game logic like collision, input, or movement
        protected override void Update(GameTime gameTime) {
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            spawnBlock(gameTime);
            moveTetBlock(gameTime);
            checkMoveCollision(gameTime);
            tetBoard.Update(gameTime);

            base.Update(gameTime);
        }

        //draws game on screen when called
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            currBlock.Draw(spriteBatch);
            tetBoard.Draw(spriteBatch, squareTex);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion

        #region GameLoopHelperMethods

        //checks if a block has been spawned, and if not, spawns one and shifts focus to it
        public void spawnBlock(GameTime gameTime) {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(!blockSpawned) {
                currBlock = blockFactory.spawnBlock();
                blockList.Add(currBlock);
                blockSpawned = true;
            }
        }

        //handles tetris block movement and input
        public void moveTetBlock(GameTime gameTime) {
            inputHandler.Update(gameTime);

            lockDelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(elapsedTime > 1000 && blockSpawned) {
                elapsedTime -= 1000;
                currBlock.moveDown(gridUnit);
            }

            if(blockSpawned) {
                if(inputHandler.isKeyPressed(Keys.Left)) {
                    currBlock.moveLeft(gridUnit);
                }
                if(inputHandler.isKeyPressed(Keys.Right)) {
                    currBlock.moveRight(gridUnit);
                }
                if(inputHandler.isKeyDown(Keys.Down)) {
                    currBlock.moveDown(gridUnit);
                }
                if(inputHandler.isKeyPressed(Keys.Space)) {
                    lockDelay = 1;
                    currBlock.rotateRight(currBlock.boundingBox.GetLength(0));
                }

                checkMoveCollision(gameTime);

            }
        }

        //checks for a collision on next move
        public void checkMoveCollision(GameTime gameTime) {
            int x, y;

            if(!currBlock.isValidMove(30, "down")) {
                blockSpawned = false;
                tetBoard.addToGrid(currBlock);

            } else { 
                foreach(TetSquare boundingSquare in currBlock.boundingBox) {
                    if(boundingSquare.isFilled) {
                        Vector2 squarePos = boundingSquare.squarePosition;

                        x = (int)squarePos.X / 30;
                        y = (int)squarePos.Y / 30;
           
                        if(tetBoard.isSquareFilled(x, y + 1) && lockDelay == 0) {
                            blockSpawned = false;
                            tetBoard.addToGrid(currBlock);
                            break;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
