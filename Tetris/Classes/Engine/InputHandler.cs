using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris {
    class InputHandler : GameComponent {
        KeyboardState currentState;
        KeyboardState lastState;

        //stores current state on component creation
        public InputHandler(Game game) : base(game) {
            currentState = Keyboard.GetState();
        }

        public KeyboardState getCurrentKeyState() {
            return currentState;
        }

        public KeyboardState getLastKeyState() {
            return lastState;
        }

        //updates the keyboard state, cycles current state to last state
        public override void Update(GameTime gameTime) {
            lastState = currentState;
            currentState = Keyboard.GetState();
        }

        public override void Initialize() {
            base.Initialize();
        }

        public bool isKeyReleased(Keys key) {
            return currentState.IsKeyUp(key) && lastState.IsKeyDown(key);
        }

        public bool isKeyPressed(Keys key) {
            return currentState.IsKeyDown(key) && lastState.IsKeyUp(key);
        }

        public bool isKeyDown(Keys key) {
            return currentState.IsKeyDown(key);
        }

    }
}
