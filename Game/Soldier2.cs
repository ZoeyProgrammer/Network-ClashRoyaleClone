using System;
using System.Drawing;

namespace MyMultiPlayerGame.Game
{
	class Soldier2 : Soldier
	{
		public Soldier2(Game game, int player)
				: base(game, player)
		{
			//Design: Medium Damage, Medium HP, Medium Mobility, Medium Visibility - I bet you play Champion Fighter as well
			this.EnergyCost = 2f;
			this.Player = player;
			this.HP = 25;
			this.Speed = 5;
			this.Damage = 5;
			this.FireFrequency = 0.5f;
			this.ViewRange = 200;
			this.FireRange = 150;
		}

		public override void Render(Graphics g)
		{
			const float radius = 10;

			g.FillEllipse(this.Player == 0 ? Brushes.Cyan : Brushes.Orange, this.X - radius, this.Y - radius, radius * 2, radius * 2);
		}
	}
}
