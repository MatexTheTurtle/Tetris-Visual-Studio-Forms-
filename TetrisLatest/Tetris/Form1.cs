using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        //DrawNextBlock() Block next = blockQueue.NextBlock;
        //Add a proper score system
        //Dont code holding you don't have time

        private GameState gameState = new GameState();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            pictureBox1.Left = this.Width / 2 - 210;
            pictureBox2.Left = this.Width / 2 + 200;
            pictureBox3.Left = this.Width / 2 - 210;
            for (int i = 0; i < 22; i++)
            {
                PictureBox p = new PictureBox();
                p.BackColor = Color.DimGray;
                p.Width = 400;
                p.Height = 1;
                p.Left = this.Width / 2 - 200;
                p.Top = i * 40;
                this.Controls.Add(p);
            }

            for (int i = 0; i < 10; i++)
            {
                PictureBox p = new PictureBox();
                p.BackColor = Color.DimGray;
                p.Width = 1;
                p.Height = 880;
                p.Left = this.Width / 2 - 200 + i * 40;
                p.Top = 0;
                this.Controls.Add(p);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver) { 
                MessageBox.Show("You Lost");
                gameState = new GameState();
                return;
            }
            switch (e.KeyCode)
            {
                case Keys.Left:
                    gameState.MoveBlockLeft(); break;
                case Keys.Right:
                    gameState.MoveBlockRight(); break;
                case Keys.Down:
                    gameState.MoveBlockDown(); break;
                case Keys.Up:
                    gameState.RotateBlockCW(); break;
                case Keys.Z:
                    gameState.RotateBlockCCW(); break;
                case Keys.Space:
                    while (gameState.MoveBlockDown()) { } Interval(); break;
                case Keys.C:
                    gameState.HoldBlock(); break;
                default:
                    return;
            }

            Redraw();
        }

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            
        }

        private void Redraw()
        {
            label1.Text = "Lines Cleared: " + gameState.score.ToString();

            this.Refresh();
            Graphics g = this.CreateGraphics();
            SolidBrush b = new SolidBrush(Color.Red);

            //Drawing Game Grid
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (gameState.GameGrid[i, j] != 0)
                    {
                        switch (gameState.GameGrid[i, j])
                        {
                            case 1: b.Color = Color.Cyan; break;
                            case 2: b.Color = Color.Blue; break;
                            case 3: b.Color = Color.Orange; break;
                            case 4: b.Color = Color.Yellow; break;
                            case 5: b.Color = Color.Green; break;
                            case 6: b.Color = Color.Purple; break;
                            case 7: b.Color = Color.Red; break;
                            default: b.Color = Color.Black; break;
                        }
                        g.FillRectangle(b, j * 40 + (this.Width / 2 - 200), i * 40, 40, 40);
                    }
                }
            }

            //Drawing Current Block
            switch (gameState.CurrentBlock.ID)
            {
                case 1: b.Color = Color.Cyan; break;
                case 2: b.Color = Color.Blue; break;
                case 3: b.Color = Color.Orange; break;
                case 4: b.Color = Color.Yellow; break;
                case 5: b.Color = Color.Green; break;
                case 6: b.Color = Color.Purple; break;
                case 7: b.Color = Color.Red; break;
                default: b.Color = Color.Black; break;
            }

            foreach (Position p in gameState.CurrentBlock.TilePositions())
            {
                g.FillRectangle(b, p.Column * 40 + (this.Width / 2 - 200), p.Row * 40, 40, 40);
            }

            //Drawing Next Block
            switch (gameState.BlockQueue.NextBlock.ID)
            {
                case 1: b.Color = Color.Cyan;
                    g.FillRectangle(b, this.Width / 2 + 300, 40, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 300, 80, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 300, 120, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 300, 160, 40, 40);
                    break;
                case 2: b.Color = Color.Blue;
                    g.FillRectangle(b, this.Width / 2 + 340, 40, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 340, 80, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 340, 120, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 300, 120, 40, 40);
                    break;
                case 3: b.Color = Color.Orange;
                    g.FillRectangle(b, this.Width / 2 + 340, 40, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 340, 80, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 340, 120, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 380, 120, 40, 40);
                    break;
                case 4: b.Color = Color.Yellow;
                    g.FillRectangle(b, this.Width / 2 + 300, 40, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 340, 40, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 300, 80, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 340, 80, 40, 40);
                    break;
                case 5: b.Color = Color.Green;
                    g.FillRectangle(b, this.Width / 2 + 340, 40, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 380, 40, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 340, 80, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 300, 80, 40, 40);
                    break;
                case 6: b.Color = Color.Purple;
                    g.FillRectangle(b, this.Width / 2 + 340, 40, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 300, 80, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 340, 80, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 380, 80, 40, 40);
                    break;
                case 7: b.Color = Color.Red;
                    g.FillRectangle(b, this.Width / 2 + 300, 40, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 340, 40, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 340, 80, 40, 40);
                    g.FillRectangle(b, this.Width / 2 + 380, 80, 40, 40);
                    break;
                default: break;
            } //do not expand

            //Drawing Held Block
            if (gameState.heldBlock != null)
            {
                switch (gameState.heldBlock.ID)
                {
                    case 1:
                        b.Color = Color.Cyan;
                        g.FillRectangle(b, this.Width / 2 - 300, 40, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 300, 80, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 300, 120, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 300, 160, 40, 40);
                        break;
                    case 2:
                        b.Color = Color.Blue;
                        g.FillRectangle(b, this.Width / 2 - 340, 40, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 340, 80, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 340, 120, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 300, 120, 40, 40);
                        break;
                    case 3:
                        b.Color = Color.Orange;
                        g.FillRectangle(b, this.Width / 2 - 340, 40, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 340, 80, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 340, 120, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 380, 120, 40, 40);
                        break;
                    case 4:
                        b.Color = Color.Yellow;
                        g.FillRectangle(b, this.Width / 2 - 300, 40, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 340, 40, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 300, 80, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 340, 80, 40, 40);
                        break;
                    case 5:
                        b.Color = Color.Green;
                        g.FillRectangle(b, this.Width / 2 - 340, 40, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 380, 40, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 340, 80, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 300, 80, 40, 40);
                        break;
                    case 6:
                        b.Color = Color.Purple;
                        g.FillRectangle(b, this.Width / 2 - 340, 40, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 300, 80, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 340, 80, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 380, 80, 40, 40);
                        break;
                    case 7:
                        b.Color = Color.Red;
                        g.FillRectangle(b, this.Width / 2 - 300, 40, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 340, 40, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 340, 80, 40, 40);
                        g.FillRectangle(b, this.Width / 2 - 380, 80, 40, 40);
                        break;
                    default: break;
                }
            } //do not expand
        }

        private void FallSpeed_Tick(object sender, EventArgs e)
        {
            if (gameState.MoveBlockDown() && FallSpeed.Interval > 300) { Interval(); };
            Redraw();
        }

        private void Interval()
        {
            FallSpeed.Interval = 1000 - gameState.score * 10;
        }
    }
}
