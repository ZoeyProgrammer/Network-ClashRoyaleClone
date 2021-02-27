using System;
using System.Drawing;

namespace MyMultiPlayerGame.Game
{
	class Soldier : GameObject
	{
		public float EnergyCost { get; private set; }
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
			this.EnergyCost = 2f;
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
			if (this.HP <= 0)
			{
				this.Game.DestroySoldier(this);
				return;
			}

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
			else if (this.Game.FindEnemy(this, this.FireRange) != null)
			{
				// enemy unit in range!
				Soldier enemy = this.Game.FindEnemy(this, this.FireRange);

				if (this.FireCooldown <= 0) //And the Cooldown has ticked down
				{
					this.FireCooldown = this.FireFrequency;
					enemy.HP -= this.Damage;
				}
			}
			else if (this.Game.FindEnemy(this, this.ViewRange) != null)
			{
				// enemy unit in sight!
				Soldier enemy = this.Game.FindEnemy(this, this.ViewRange);
				float Xdif = enemy.X - this.X;
				float Ydif = enemy.Y - this.Y;
				float distance = (float)Math.Sqrt(Math.Pow(enemy.X - this.X, 2) + Math.Pow(enemy.Y - this.Y, 2));

				//Go towards that unit
				this.X += this.Speed * (Xdif / distance);
				this.Y += this.Speed * (Ydif / distance);
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
