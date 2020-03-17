using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Shapes.Demo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            var pos1 = new Vector2(1, 0);
            var pos2 = new Vector2(1+100, 0);
            var pos3 = new Vector2(1+100, 0+100);
            var pos4 = new Vector2(1, 0+100);

            _spriteBatch.DrawLine(pos1, pos2, Color.Orange);
            _spriteBatch.DrawLine(pos2, pos3, Color.Orange);
            _spriteBatch.DrawLine(pos3, pos4, Color.Orange);
            _spriteBatch.DrawLine(pos4, pos1, Color.Orange);

            _spriteBatch.DrawRectangle(new Rectangle(1, 1+100, 100, 100), Color.Yellow);

            _spriteBatch.FillRectangle(new Rectangle(1, 1*2+100*2, 100, 100), Color.Violet);

            pos1 = new Vector2(200, 1);
            pos2 = new Vector2(200, 1+100);
            _spriteBatch.DrawLine(pos2, pos1, Color.Red);
            _spriteBatch.DrawLine(pos1, pos2, Color.Orange);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
