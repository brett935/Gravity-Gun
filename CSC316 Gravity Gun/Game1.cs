using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            //Vector3 playerForward = Vector3.Backward;
            //playerForward = Vector3.Transform(playerForward, Matrix.CreateRotationX(rotationX) * Matrix.CreateRotationY(rotationY));
            #endregion

            Mouse.SetPosition(windowCenter.X, windowCenter.Y); //center the mouse in the game window (fixes camera rotating uncontrollably)

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

            world = Matrix.CreateScale(100);
            floor.Draw(world, view, projection); //draw the floor in the world

            base.Draw(gameTime);
        }
    }
}
