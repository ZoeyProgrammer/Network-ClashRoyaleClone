using System;
using System.Drawing;

namespace MyMultiPlayerGame.Game
{
	class Soldier1 : Soldier
	{
		public Soldier1(Game game, int player)
				: base(game, player)
		{
			//Design: Low Damage, Low HP, Very High Mobility, High Visibility - Rushdown
			this.EnergyCost = 1f;
			this.Player = player;
			this.HP = 10;
			this.Speed = 8;
			this.Damage = 2;
			this.FireFrequency = 0.3f;
			this.ViewRange = 300;
			this.FireRange = 80;
		}

		public override void Render(Graphics g)
		{
			const float radius = 8;

			g.FillEllipse(this.Player == 0 ? Brushes.Lime : Brushes.Yellow, this.X - radius, this.Y - radius, radius * 2, radius * 2);
		}
	}
}
