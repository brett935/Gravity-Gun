using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CSC316_Gravity_Gun
{
    /// <summary> 
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        /// <summary>
        /// The model of the ground
        /// </summary>
        private Model floor;
        /// <summary>
        /// The 3D position of the camera in world space.
        /// </summary>
        private Vector3 cameraPosition;
        /// <summary>
        /// Field of View in radians
        /// </summary>
        private float fov;
        /// <summary>
        /// Camera X rotation around player
        /// </summary>
        private float rotationX;
        /// <summary>
        /// Camera Y rotation around player
        /// </summary>
        private float rotationY;
        /// <summary>
        /// Position of the player in 3D space
        /// </summary>
        private Vector3 playerPos;
        /// <summary>
        /// The center of the window in pixels.
        /// </summary>
        Point windowCenter;
        /// <summary>
        /// The sensitivity of the players camera
        /// </summary>
        float lookSensitivity;
        /// <summary>
        /// The font used to draw 2D text to the screen.
        /// </summary>
        SpriteFont letterFont;
        /// <summary>
        /// When true debug mode will be activated and UI information related to debugging will be displayed.
        /// </summary>
        private bool debugMode;
        /// <summary>
        /// The model for an enemy character.
        /// </summary>
        private Model enemyCharacter;
        /// <summary>
        /// List containing all enemies in the world.
        /// </summary>
        private List<Entity> enemyList;
        /// <summary>
        /// Random number generator used for coordinates.
        /// </summary>
        Random random;
        /// <summary>
        /// List containing terrain and non-living objects
        /// </summary>
        private List<Entity> landscapeList;
        /// <summary>
        /// A stationary landscape model
        /// </summary>
        private Model landscape;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            #region camera init
            cameraPosition = new Vector3(0, 10, -50); //initial camera position
            fov = MathHelper.PiOver4;
            rotationX = 0;
            rotationY = 0;
            #endregion

            windowCenter = new Point(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2); //get window center on initialization
            lookSensitivity = 0.01f; //set initial camera sensitivity

            playerPos = new Vector3(0, 0, 0); //initial player position

            debugMode = false; //debug mode is turned off on initialization

            random = new Random(); //seed random number generator

            #region initialize/populate list containing enemies and objects
            //TODO: Create Enemy class and then change the type of the enemy list from Entity to Enemy
            enemyList = new List<Entity>();

            //generate enemies for testing
            for (int i = 0; i < 5; i++)
            {
                //give the enemies random coordinates
                int randomXCoordinate = random.Next(-50, 50);
                int randomZCoordinate = random.Next(-50, 50);
                //enemyList.Add(new Entity("testEnemy", new Vector3(randomXCoordinate, 10, randomZCoordinate), new Vector3(0, 0, 0)));
            }
            #endregion

            #region initialize/populate landscape list
            landscapeList = new List<Entity>();

            //generate terrain for testing
            for (int i = 0; i < 5; i=i+20)
            {
                //give the landscape semi-random coordinates
                landscapeList.Add(new Entity("tile", new Vector3(i+5, i+5, i+5), new Vector3(0, 0, 0)));
            }
            #endregion

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

            floor = Content.Load<Model>("floor");
            letterFont = Content.Load<SpriteFont>("letterfont");
            enemyCharacter = Content.Load<Model>("cube");
            landscape = Content.Load<Model>("landscapeCube");
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
            #region process user input
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Vector3 move = new Vector3(0, 0, -1); //amount to move the player

            rotationY -= (Mouse.GetState().X - windowCenter.X) * lookSensitivity;
            rotationX -= (Mouse.GetState().Y - windowCenter.Y) * lookSensitivity;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                rotationX -= 0.01f; //rotate camera up
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                rotationX += 0.01f; //rotate camera down
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                rotationY += 0.01f; //rotate camera left
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                rotationY -= 0.01f; ; //rotate camera right

            //directions relative to the player
            Vector3 forward = Vector3.Transform(move, Matrix.CreateRotationY(rotationY));
            Vector3 backward = -forward;
            Vector3 up = Vector3.Up;
            Vector3 right = Vector3.Cross(forward, up);
            Vector3 left = -right;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                playerPos += left; //move player to the left
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                playerPos += right; //move player to the right
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                playerPos += forward; //move player forward
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                playerPos += backward; //move player backward
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                fov = MathHelper.Clamp(fov + MathHelper.ToRadians(1), 0.1f, MathHelper.Pi - 0.1f); //increase field of view
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                fov = MathHelper.Clamp(fov - MathHelper.ToRadians(1), 0.1f, MathHelper.Pi - 0.1f); //decrease field of view
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                debugMode = !debugMode; //toggle debug mode and show/hide debugging content
            }
            #endregion

            Mouse.SetPosition(windowCenter.X, windowCenter.Y); //center the mouse in the game window (fixes camera rotating uncontrollably)

            #region update enemies
            foreach (Entity enemy in enemyList)
            {
                enemy.Update(gameTime); //update each enemy

                float colliderRadius = landscape.Meshes[0].BoundingSphere.Radius; //radius of terrains bounding sphere
                if (enemy.position.Y < (0 + colliderRadius)) //check for collision with the floor
                {
                    enemy.position = new Vector3(enemy.position.X, colliderRadius, enemy.position.Z); //fix position (place the object touching the correct surface)
                    float bounce = 1f; //multiplier for direction change
                    enemy.velocity = bounce * Vector3.Dot(-enemy.velocity, Vector3.Up) * Vector3.Up + enemy.velocity; //fix velocity 2* (-V dot N) * N + V
                }

            }
            #endregion

            #region update landscape
            foreach (Entity terrain in landscapeList)
            {
                terrain.Update(gameTime); //update each terrain
                 
                float colliderRadius = landscape.Meshes[0].BoundingSphere.Radius; //radius of terrains bounding sphere
                if (terrain.position.Y < (0 + colliderRadius) ) //check for collision with the floor
                {
                    terrain.position = new Vector3(terrain.position.X, colliderRadius, terrain.position.Z); //fix position (place the object touching the correct surface)
                    float bounce = 1f; //multiplier for direction change
                    terrain.velocity = bounce * Vector3.Dot(-terrain.velocity, Vector3.Up) * Vector3.Up + terrain.velocity; //fix velocity 2* (-V dot N) * N + V
                }
            }
            #endregion

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            #region setup graphics
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //how we place the model in the world
            Matrix world = Matrix.Identity;
            //how the world relates to the camera
            Matrix view = Matrix.CreateLookAt(Vector3.Transform(cameraPosition, Matrix.CreateRotationX(rotationX) * Matrix.CreateRotationY(rotationY)) + playerPos, playerPos, Vector3.Up);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(fov, 1, 0.001f, 1000.0f);
            #endregion

            world = Matrix.CreateScale(100); //scale the world for the floor
            floor.Draw(world, view, projection); //draw the floor in the world

            #region draw enemies
            world = Matrix.CreateScale(0.025f); //scale the world to be the same for all enemies
            foreach (Entity enemy in enemyList)
            {
                world *= Matrix.CreateTranslation(enemy.position); //translate the world relative to the enemy position
                enemyCharacter.Draw(world, view, projection); //draw an enemy model
            }
            #endregion

            #region draw entities (landscape)
            //world = Matrix.CreateScale(0.025f); //scale the world the same for all landscape
            foreach (Entity terrain in landscapeList)
            {
                //TODO: Draw different types of landscape here
                //System.Diagnostics.Debug.WriteLine("Draw landscape object.");
                world *= Matrix.CreateTranslation(terrain.position); //translate the world relative to the landscape position
                landscape.Draw(world, view, projection); //draw landscape model
            }
            #endregion

            if (debugMode)
            {
                #region draw UI display
                spriteBatch.Begin();

                string text = "Debugging Activated \nX: " + playerPos.X.ToString() + "\nZ: " + playerPos.Z.ToString(); //text to draw to UI
            
                Vector2 UIPosition = new Vector2(30, 30); //position of user interface on 2D screen
                spriteBatch.DrawString(letterFont, text, UIPosition, Color.Green, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

                spriteBatch.End();
                #endregion
                #region reset graphics device after using spriteBatch
                GraphicsDevice.BlendState = BlendState.Opaque;
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
                #endregion
            }
            

            base.Draw(gameTime);
        }
    }
}
