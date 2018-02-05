using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

namespace Perception
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private InputHandler input;
        private TestGrid grid;
        private List<Texture2D> image_grid;
        private Texture2D player;
        private Texture2D instructions;
        private Texture2D box;
        private int current_pos;
        private int pos_x;
        private int pos_y;
        const int BLOCK_SIZE = 100;
        private int score = -1;
        private int high_score = -1;
        private double end_level_time = 0;
        private int game_timer = 0;
        private SpriteFont game_font;
        private SpriteFont menu_font;
        private bool menu_active = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 700;

            input = new InputHandler();
            grid = new TestGrid();
            image_grid = new List<Texture2D>();
            current_pos = 0;
            pos_x = 100;
            pos_y = 100;
            readHighScore();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            progressToNextLevel(0);
            player = Content.Load<Texture2D>("player");
            instructions = Content.Load<Texture2D>("instructions");
            box = Content.Load<Texture2D>("box");
            game_font = Content.Load<SpriteFont>("score");
            menu_font = Content.Load<SpriteFont>("menu");
            // TODO: use this.Content to load your game content here
        }

        private void progressToNextLevel(double currentGameTime)
        {
            grid.ResetGrid();
            image_grid.Clear();
            for (int i = 0; i < grid.grid.Length; i++)
            {
                image_grid.Add(Content.Load<Texture2D>(grid.grid[i]));
            }
            pos_x = 100;
            pos_y = 100;
            current_pos = 0;
            score++;
            if(score > high_score)
            {
                writeHighScore(score);
                high_score = score;
            }
            end_level_time = currentGameTime + 10;
        }

        private void readHighScore()
        {
            try {
                //StreamReader score_file = File.OpenText("highscore.txt");
                using (StreamReader score_file = File.OpenText("highscore.txt"))
                {
                    high_score = System.Convert.ToInt16(score_file.ReadToEnd());
                }
            }
            catch(Exception e)
            {
                File.WriteAllText("highscore.txt", "0");
                high_score = 0;
            }
        }

        private void writeHighScore(int new_score)
        {
            File.WriteAllText("highscore.txt", new_score.ToString());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Exit Game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Timer
            var timer = end_level_time - gameTime.TotalGameTime.Seconds;
            game_timer = (int)timer;
            if(game_timer <= 0)
            {
                menu_active = true;
            }

            //Movement
            input.Update();

            if (menu_active)
            {
                if (input.HasReleasedKey(Keys.R))
                {
                    score = -1;
                    progressToNextLevel(gameTime.TotalGameTime.Seconds);
                    menu_active = false;
                }
            }
            else
            {
                if (pos_x >= 500 && pos_y >= 500)
                {
                    progressToNextLevel(gameTime.TotalGameTime.Seconds);
                }

                var currentStep = "";

                if (input.HasReleasedKey(Keys.Up))
                    currentStep = "up";
                if (input.HasReleasedKey(Keys.Down))
                    currentStep = "down";
                if (input.HasReleasedKey(Keys.Left))
                    currentStep = "left";
                if (input.HasReleasedKey(Keys.Right))
                    currentStep = "right";
                if(currentStep == grid.grid[current_pos])
                {
                    switch (currentStep)
                    {
                        case "up":
                            pos_y -= BLOCK_SIZE;
                            current_pos-=5;
                            break;
                        case "down":
                            pos_y += BLOCK_SIZE;
                            current_pos+=5;
                            break;
                        case "left":
                            pos_x -= BLOCK_SIZE;
                            current_pos--;
                            break;
                        case "right":
                            pos_x += BLOCK_SIZE;
                            current_pos++;
                            break;
                    }
                }
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            if (menu_active)
            {
                spriteBatch.Draw(box, new Rectangle(150, 140, 400, 50), Color.White);
                spriteBatch.DrawString(menu_font, "Perception", new Vector2(230, 100), Color.White);
                spriteBatch.DrawString(menu_font, "Deception", new Vector2(280, 140), Color.Red);
                spriteBatch.DrawString(menu_font, "[press 'r' to start]", new Vector2(200, 550), Color.White);
                spriteBatch.Draw(box, new Rectangle(130, 395, 440, 100), Color.White);                
                spriteBatch.Draw(player, new Rectangle(300, 250, BLOCK_SIZE, BLOCK_SIZE), Color.White);
                spriteBatch.DrawString(game_font, "Instructions", new Vector2(300, 370), Color.White);
                spriteBatch.Draw(instructions, new Rectangle(160, 400, 372, 90), Color.White);
            }
            else
            {
                var column = 100;
                var row = 100;

                for (int i = 0; i < image_grid.Count; i++)
                {
                    spriteBatch.Draw(image_grid[i], new Rectangle(column, row, BLOCK_SIZE, BLOCK_SIZE), Color.White);
                    if(column >= (100 + 4*BLOCK_SIZE))
                    {
                        column = 100;
                        row += 100;
                    }
                    else
                    {
                        column += 100;
                    }
                }

                spriteBatch.Draw(player, new Rectangle(pos_x, pos_y, BLOCK_SIZE, BLOCK_SIZE), Color.White);
                spriteBatch.DrawString(game_font, "Score: " + score, new Vector2(100, 620), Color.White);
                spriteBatch.DrawString(game_font, "High score: " + high_score, new Vector2(100, 650), Color.White);
                spriteBatch.DrawString(game_font, "Timer: ", new Vector2(500, 620), Color.White);
                var timer_colour = Color.White;
                if (game_timer <= 3)
                {
                    timer_colour = Color.Red;
                }
                spriteBatch.DrawString(game_font, game_timer.ToString(), new Vector2(570, 620), timer_colour);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
