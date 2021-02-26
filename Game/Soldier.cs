using System.Drawing;

namespace MyMultiPlayerGame.Game
{
	class Soldier : GameObject
	{
		public int HP { get; private set; }
		public float Speed { get; private set; }
		public float ViewRange { get; private set; }
		public float FireRange { get; private set; }
		public float FireFrequency { get; private set; }
		public int Damage { get; private set; }
		public int Player { get; private set; }
		private float FireCooldown;

		public Soldier(Game game, int player)
				: base(game)
		{
			this.Player = player;
			this.HP = 10;
			this.Speed = 5;
			this.Damage = 3;
			this.FireFrequency = 0.5f;
			this.ViewRange = 200;
			this.FireRange = 100;
			this.FireCooldown = 0;
		}

		public override void NextSimulationStep()
		{
			if (FireCooldown > 0)
				FireCooldown -= 0.1f;

			if ((this.Player == 0 && this.X + this.FireRange >= this.Game.boardWidth) ||
					(this.Player == 1 && this.X - this.FireRange <= 0))
			{
				// enemy player in range!
				if (this.FireCooldown <= 0) //And the Cooldown has ticked down
				{
					this.FireCooldown = this.FireFrequency;

					if (this.Player == 0)
						this.Game.DealPlayerDamge(1, this.Damage);
					else if (this.Player == 1)
						this.Game.DealPlayerDamge(0, this.Damage);
				}
			}
			else if (false)
			{
				// enemy unit in range!

			}
			else if (false)
			{
				// enemy unit in sight!
			}
			else
			{
				// move to opponent
				if (this.Player == 0)
					this.X += this.Speed;
				else
					this.X -= this.Speed;
			}
		}

		public override void Render(Graphics g)
		{
			const float radius = 10;

			g.FillEllipse(this.Player == 0 ? Brushes.Red : Brushes.Yellow, this.X - radius, this.Y - radius, radius * 2, radius * 2);
		}

	}
}
