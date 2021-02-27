using System;
using System.Drawing;

namespace MyMultiPlayerGame.Game
{
	class Soldier3 : Soldier
	{
		public Soldier3(Game game, int player)
				: base(game, player)
		{
			//Design: High Damage, High HP, Low Mobility, Low Visibility - Tank type
			this.EnergyCost = 3f;
			this.Player = player;
			this.HP = 100;
			this.Speed = 2;
			this.Damage = 30;
			this.FireFrequency = 1f;
			this.ViewRange = 120;
			this.FireRange = 100;
		}

		public override void Render(Graphics g)
		{
			const float radius = 12;

			g.FillEllipse(this.Player == 0 ? Brushes.Blue : Brushes.Red, this.X - radius, this.Y - radius, radius * 2, radius * 2);
		}
	}
}
