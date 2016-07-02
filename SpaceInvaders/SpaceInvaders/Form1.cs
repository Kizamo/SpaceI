/**
 *  Project name:        SpaceInvaders
 *  Project file name:   SpaceInvaders
 *  
 *  Author:              Taylor Thomson
 *  Date:                5 June 16
 *  Language:            C#
 *  Platform:            Visual Studio 2015
 *  
 *  Purpose:             Recreate the classic Space Invaders game using C#. The playwe will control
 *                       a ship by moving their mouse to dodge bombs and clicking to shoot down enemy ships.
 *  Description:         The game has a mother ship, 40 enemy ships, bombs and up to 15 missiles. The mother ship is
 *                       controlled by the player and moves left and right on the bottom of the screen. The mother ship
 *                       can shoot missiles up. Enemy ships move from side to side and decend down the screen and change
 *                       direction when one hits the boundary of the screen. Enemy ships drop bombs down. If the mother 
 *                       ship is hit by a bomb the game ends. It an enemy ship is hit by a missile both the missile and
 *                       ship are destroyed.
 *  Known Bugs:          empty
 *  Additional Features: There is a 1 in 300 chance that when an enemy ship is created it will be a king ship. King ships
 *                       reward more score for kiling them, have a higher chance to shoot, can shoot through other enemy ships
 *                       and their bombs travel faster.
 *                       The game does not end when all enemy ships are destroyed, instead the background image changes, all ships
 *                       are recreated and have their speed and bomb speed increased. The player will recieve the winner game over
 *                       screen if they managed to get past the first set of enemy ships before dying.
 *                       A second timer is used at the beginning of the game to show the title screen and disabled once the title
 *                       screen ends. This is to prevent an extra if statment every time the main timer ticks.
 *                       When the game is not running a song will play on loop. The first time the game is run the song is extended
 *                       and has an extra part at the beginning.
 *                       Sound is played when a missile is shot and when an enmey ship is hit.
 *                       A mute button stops the playback of all sound. It can be clicked again to resume sound playback.
 *                       A pause button pauses the game and gives the option to mute, resume or end the game.
 *                       When the game is lost it can be restarted. The score, enemy ships and background image are reset.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {
        private const int TITLETIME = 50; //Time the title screen with show for in hundredth of a second
        private const int FORMX = 1024; //Client size of the form
        private const int FORMY = 800; //Client size of the form
        private int titleTimer;
        private ThemeSong themeSong;
        private bool paused;
        private bool muted;
        private Graphics graphic;
        private Graphics bufferGraphic;
        private Bitmap bufferImage;
        private Image backImage;
        private bool running;
        private Game game;
        private Point bounds;
        private Random rand;
        private int level;
        public Form1()
        {
            InitializeComponent();
            level = 0;
            backImage = Properties.Resources.planetBG;
            graphic = CreateGraphics();
            bufferImage = new Bitmap(Width, Height);
            bufferGraphic = Graphics.FromImage(bufferImage);
            running = false;
            muted = false;
            this.ClientSize = new Size(FORMX, FORMY);
            graphic = CreateGraphics();
            titleTimer = 0;
            paused = false;
            themeSong = new ThemeSong();
            titleScreen.Enabled = true;
            this.ControlBox = false;
            bounds.X = FORMX;
            bounds.Y = FORMY;
            rand = new Random();

        }

        private void titleScreen_Tick(object sender, EventArgs e) //used to show the title screen, is disabled and never enabled after the title screen is over
        {
            if (titleTimer < TITLETIME)
            {
                titleTimer++;
            }
            else
            {
                this.BackgroundImage = Properties.Resources.startScreen;
                titleScreen.Enabled = false;
                newGame.Visible = true;
                exit.Visible = true;
                mute.Visible = true;
                themeSong.Play();
            }
        }

        private void newGame_Click(object sender, EventArgs e) //starts a new game. Sets the background image to the beginning one, builds/rebuilds all ships and enables the main timer
        {
            backImage = Properties.Resources.planetBG;
            this.BackgroundImage = backImage;
            level = 0;
            game = new Game(bounds, bufferGraphic, rand, muted);
            themeSong.Stop();
            timer1.Enabled = true;
            running = true;
            exit.Visible = false;
            newGame.Visible = false;
            mute.Visible = false;
            pause.Visible = true;
        }

        private void exit_Click(object sender, EventArgs e) //stops playback of the theme song and closes the program
        {
            themeSong.Stop();
            Application.Exit();
        }

        private void pause_Click(object sender, EventArgs e) // pauses or unpauses the game. Makes the exit and mute buttons visible (when paused)
        {
            paused = !paused;
            if (paused)
            {
                exit.Visible = true;
                mute.Visible = true;
                timer1.Enabled = false;
                pause.Image = Properties.Resources.resume;
            }
            else
            {
                exit.Visible = false;
                mute.Visible = false;
                timer1.Enabled = true;
                pause.Image = Properties.Resources.pause;
            }
        }

        private void mute_Click(object sender, EventArgs e) //stops playback of theme song and prevents any sound from playing, clicking again enables sound
        {
            muted = !muted;
            if (muted)
            {
                mute.Image = Properties.Resources.mute;
                if (!running)
                {
                    themeSong.Stop();
                }
                else
                {
                    game.Mute = true;
                }
            }
            else
            {
                mute.Image = Properties.Resources.speaker;
                if (!running)
                {
                    themeSong.Play();
                }
                else
                {
                    game.Mute = false;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e) //Runs the game. Checks if the game is over and displays the appropriate game over image.
        {
            bufferGraphic.DrawImage(backImage, 0, 0);
            game.Run();
            if (game.NextLevel())
            {
                switch (level)
                {
                    case 0:
                        backImage = Properties.Resources.planet_2BG;
                        BackgroundImage = backImage;
                        level++;
                        break;
                    case 1:
                        backImage = Properties.Resources.starsBG;
                        BackgroundImage = backImage;
                        break;
                    default:
                        break;
                }
            }
            if (game.CheckBombHit() || game.GameOver())
            {
                if (level > 0)
                {
                    bufferGraphic.DrawImage(Properties.Resources.win, bounds.X / 2 - 375, bounds.Y / 2 - 300);
                    EndOfGame();
                }
                else
                {
                    bufferGraphic.DrawImage(Properties.Resources.lose, bounds.X / 2 - 375, bounds.Y / 2 - 300);
                    EndOfGame();
                }
            }
            Application.DoEvents();
            graphic.DrawImage(bufferImage, 0, 0);
        }
        private void EndOfGame() //Makes the exit, mute and new game buttons visible. Starts playback of the theme song if not muted.
        {
            pause.Visible = false;
            timer1.Enabled = false;
            newGame.Visible = true;
            exit.Visible = true;
            mute.Visible = true;
            running = false;
            if (!muted)
            {
                themeSong.Play();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e) // passes the mouses location to game.
        {
            if (timer1.Enabled)
            {
                game.MoveMotherShip(e.X);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) // tells game to fire a missile.
        {
            if (timer1.Enabled)
            {
                game.MotherShipShoot();
            }
        }
    }
}
