using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyMultiPlayerGame
{
	partial class Canvas : UserControl
	{
		Game.Game game;

		public Canvas(Game.Game game)
		{
			InitializeComponent();
			this.game = game;
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.Blue, new RectangleF(0, 0, 5000, 5000));
			//e.Graphics.DrawLine(Pens.Red, 10, 10, 100, 50);

			this.game.Render(e.Graphics);
		}

		private void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.game.myPlayerNumber == 0 && e.X < this.game.boardWidth / 2)	//Player 1 can only place on the Left Half of the Screen
			{
				this.BeginInvoke(new Action(() =>
				{
					this.game.SpawnSoldier(e.X, e.Y);
				}));
			}
			else if (this.game.myPlayerNumber == 1 && e.X > this.game.boardWidth / 2) //Player 2 can only place on the Right Half of the Screen
			{
				this.BeginInvoke(new Action(() =>
				{
					this.game.SpawnSoldier(e.X, e.Y);
				}));
			}
		}

		private void Canvas_Load(object sender, EventArgs e)
		{

		}
	}
}
