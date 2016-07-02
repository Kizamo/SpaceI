using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Game
    {
        private const int KINGCHANCE = 300; //chance that a ship will spawn as a king ship.
        private const int SHIPSIZE = 70; // Sets how far apart the ships are from each other, include the size of the ship.
        private const int SHIPCOLUMN = 10; //amount of ships per comumn
        private const int SHIPROW = 4; // amount of rows of ships
        private const int KINGBOMBX = 14; //X size of the king ships bomb
        private const int KINGBOMBY = 20; //Y size of the king ships bomb
        private const int BOMBX = 9;//X size of the ships bomb
        private const int BOMBY = 13; // Y size of the king ships bomb
        private const int ENEMYSIZEY = 30;// size of the enemy ships
        private const int KINGSIZEY = 34; //size of the king ship
        private const int BOMBSTARTSPEED = 40;
        private const int SHIPSTARTSPEED = 20;
        private const int KINGBOMBSPEED = 50;
        private const int MAXMISSILELIFE = 70; // total amount of ticks the missile can live for
        private const int MAXMISSILES = 15; // total amount of missiles at a time
        private const int BOMBCHANCE = 100; // chance that a ship will drop a bomb
        private const int KINGBOMBCHANCE = 50; // chance that a king will drop a bomb
        private const int SHIPSCORE = 10;
        private const int KINGSCORE = 20;
        private const int SPEEDINCREASE = 5;
        private MotherShip motherShip;
        private List<EnemyShip> enemyShips;
        private List<Missile> missiles;
        private Graphics graphic;
        private Point bounds;
        private bool mute;
        private Random rand;
        private SoundPlayer shoot;
        private SoundPlayer shipHit;
        private List<Bomb> bombs;
        private int score;
        private SolidBrush scoreBrush;
        private Font scoreFont;
        private bool enemyDir; // true = right, false = left
        private int bombSpeed;
        private int kingBombSpeed;
        private int shipSpeed;

        public Game(Point bounds, Graphics graphic, Random rand, bool mute)
        {
            this.mute = mute;
            this.rand = rand;
            this.bounds = bounds;
            this.graphic = graphic;
            bombSpeed = BOMBSTARTSPEED;
            kingBombSpeed = KINGBOMBSPEED;
            shipSpeed = SHIPSTARTSPEED;
            scoreBrush = new SolidBrush(Color.Cyan);
            scoreFont = new Font("DINPro", 18, FontStyle.Bold);
            score = 0;
            shoot = new SoundPlayer(Properties.Resources.Shot);
            shipHit = new SoundPlayer(Properties.Resources.invaderkilled);
            enemyDir = true;
            motherShip = new MotherShip(graphic, bounds);
            enemyShips = new List<EnemyShip>();
            missiles = new List<Missile>();
            bombs = new List<Bomb>();
            SetUpShips();
        }
        public void MoveMotherShip(int mouseX) // gives the mouses location to the mother ship.
        {
            motherShip.Move(mouseX);
        }
        public void Run() // draws all game objects, checks for missile hits and moves everything but the mother ship.
        {
            motherShip.Draw();
            DrawMissile();
            CheckMissileHit();
            DrawEnemy();
            DropBombs();
            DrawBomb();
            DrawScore();
        }
        public void DrawScore() // draws the players score.
        {
            graphic.DrawString("SCORE:  " + Convert.ToString(score), scoreFont, scoreBrush, 0, 0);
        }
        public bool CheckBombHit() // checks of a bomb hits the motehr ship. Returns true if it did.
        {
            bool hit = false;
            foreach (Bomb value in bombs)
            {
                if (value.CheckForHit(motherShip.Pos))
                {
                    hit = true;
                }
            }
            return hit;
        }
        public void DropBombs() // adds a bomb to the bombs list if one is created.
        {
            foreach (EnemyShip value in enemyShips)
            {
                if (value.IsKing)
                {
                    if (rand.Next(KINGBOMBCHANCE) == 1)
                    {
                        bombs.Add(new Bomb(graphic, value.Pos, kingBombSpeed, Properties.Resources.kingBomb, new Point(KINGBOMBX, KINGBOMBY)));
                    }
                }
                else
                {
                    bool bot = true;
                    if (rand.Next(BOMBCHANCE) == 1)
                    {
                        foreach (EnemyShip ship in enemyShips)
                        {
                            if (value.Pos.Y < ship.Pos.Y)
                            {
                                bot = false;
                            }
                        }
                        if (bot)
                        {
                            bombs.Add(new Bomb(graphic, value.Pos, bombSpeed, Properties.Resources.bomb, new Point(BOMBX, BOMBY)));
                        }
                    }
                }
            }
        }
        public void MotherShipShoot() // checks if there is room for another missile. If there is a missile is added to the missiles list and a shoot sound is played.
        {
            if (missiles.Count < MAXMISSILES)
            {
                missiles.Add(new Missile(graphic, motherShip.Pos, rand.Next(MAXMISSILELIFE)));
                if (!mute)
                {
                    shoot.Play();
                }
            }
        }
        public bool NextLevel() // Checks if all enemy ships have been destroyed. Recreates all ships with increases speed and returns true if they are.
        {
            if (enemyShips.Count == 0)
            {
                shipSpeed = shipSpeed + SPEEDINCREASE;
                kingBombSpeed = kingBombSpeed + SPEEDINCREASE;
                bombSpeed = bombSpeed + SPEEDINCREASE;
                SetUpShips();
                return true;
            }
            return false;
        }
        public void SetUpShips() // fills the list of ships.
        {
            Point shipPos = new Point(0, SHIPSIZE / 2);
            for (int i = 0; i < SHIPROW; i++)
            {
                shipPos.X = SHIPSIZE / 2;
                for (int l = 0; l < SHIPCOLUMN; l++)
                {
                    if (rand.Next(KINGCHANCE) == 1)
                    {
                        enemyShips.Add(new EnemyShip(graphic, bounds, shipPos, Properties.Resources.kingShip, new Point(SHIPSIZE, KINGSIZEY), true, shipSpeed));
                    }
                    else
                    {
                        enemyShips.Add(new EnemyShip(graphic, bounds, shipPos, Properties.Resources.enemyShip, new Point(SHIPSIZE, ENEMYSIZEY), false, shipSpeed));
                    }
                    shipPos.X = shipPos.X + SHIPSIZE;
                }
                shipPos.Y = shipPos.Y + SHIPSIZE;
            }
        }
        public void DrawEnemy() // checks if any ships in the list will hit the screen boundary. Changes the direction and sets drop to true if any does. Moves the ships. Draws the ships. Removes the ships from the list if it is dead
        {
            bool drop = false;
            bool exit = false;
            int place = 0;
            while ((place < enemyShips.Count) && (exit == false))
            {
                if (enemyShips[place].HitWall())
                {
                    drop = true;
                    enemyDir = !enemyDir;
                    exit = true;
                }
                place++;
            }
            for (int i = 0; i < enemyShips.Count; i++)
            {
                enemyShips[i].Move(enemyDir, drop);
                if (enemyShips[i].Dead)
                {
                    enemyShips.RemoveAt(i);
                    i--;
                }
                else
                {
                    enemyShips[i].Draw();
                }
            }
        }
        public void DrawMissile() // Removes the missile from the list if it is dead. Draws the missile.
        {
            for (int i = 0; i < missiles.Count; i++)
            {
                missiles[i].Move();
                if (missiles[i].Dead)
                {
                    missiles.RemoveAt(i);
                    i--;
                }
                else
                {
                    missiles[i].Draw();
                }
            }
        }
        public void DrawBomb() // moves the bomb. Removes the bomb from the list if its dead. Draws the bomb.
        {
            for (int i = 0; i < bombs.Count; i++)
            {
                bombs[i].Move();
                bombs[i].OffScreen(bounds);
                if (bombs[i].Dead)
                {
                    bombs.RemoveAt(i);
                    i--;
                }
                else
                {
                    bombs[i].Draw();
                }
            }
        }
        public void CheckMissileHit() // Checks if the missile hit any ships. Increases the score it it did and playes the ship hit sound.
        {
            foreach (Missile missile in missiles)
            {
                foreach (EnemyShip value in enemyShips)
                {
                    if (missile.CheckForHit(value.Pos))
                    {
                        if (value.IsKing)
                        {
                            score = score + KINGSCORE;
                        }
                        else
                        {
                            score = score + SHIPSCORE;
                        }
                        value.Dead = true;
                        if (!mute)
                        {
                            shipHit.Play();
                        }
                    }
                }
            }
        }
        public bool GameOver() // Returns true if the enemy ships reach the Y level of the mother ship.
        {
            bool gameOver = false;
            foreach (EnemyShip value in enemyShips)
            {
                if (value.Pos.Bottom >= motherShip.Pos.Y)
                {
                    gameOver = true;
                    return gameOver;
                }
            }
            return gameOver;
        }
        public bool Mute //encapsulated field
        {
            set
            {
                mute = value;
            }
        }
    }
}
