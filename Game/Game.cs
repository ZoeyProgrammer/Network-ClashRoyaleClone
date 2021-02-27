using System;
using System.Collections.Generic;
using System.Drawing;
using MyMultiPlayerGame.Messages;
using System.Windows.Forms;

namespace MyMultiPlayerGame.Game
{
	class Game
	{
		bool running;
		int simStepCount;
		List<GameObject> allGameObjects = new List<GameObject>();
		List<GameObject> deadGameObjects = new List<GameObject>();

		public class InputEvent
		{
			public byte UnitTypePlaced;         // 0 for none
			public float X;
			public float Y;
		}

		//All variables needed for the game
		public MainWindow window { get; set; }
		public int currentlySelectedUnit { get; set; }
		public int numPlayers { get; private set; }
		public int myPlayerNumber { get; private set; }
		public int boardWidth { get; private set; }
		public int boardHeight { get; private set; }
		public int player0HP { get; private set; } //Host HP
		public string player0Name { get; set; } //Host Username
		public int player1HP { get; set; } //User HP
		public string player1Name { get; set; } //User Username
		public float playerEnergy { get; set; }	//Player Energy
		public float playerMaxEnergy { get; private set; } //Maximum amount of Energy

		InputEvent[] collectedInputEvents;                              // events needed for next simulation
		List<GameInput> futureEvents = new List<GameInput>();           // events needed after next simulation

		InputEvent myInput = null;
		List<NetworkConnection> peers;

		//Setting all the standart Variables etc.
		public void Start(int numPlayers, int myPlayerNumber, List<NetworkConnection> peers)
		{
			System.Diagnostics.Debug.WriteLine("Start()");
			this.currentlySelectedUnit = 0;
			this.player0HP = 100;
			this.player1HP = 100;
			this.playerEnergy = 10;
			this.playerMaxEnergy = 10;
			this.boardHeight = 300;
			this.boardWidth = 600;
			this.running = true;
			this.simStepCount = 0;
			this.numPlayers = numPlayers;
			this.myPlayerNumber = myPlayerNumber;
			this.collectedInputEvents = new InputEvent[numPlayers];
			this.peers = peers;

			//Inititilize the visible HP values and other UI things
			this.window.labelPlayer1HP.Text = this.player1Name + ": " + this.player1HP;
			this.window.labelPlayer0HP.Text = this.player0Name + ": " + this.player0HP;
			this.window.labelPlayerEnergy.Text = "Energy: " + this.playerEnergy;
			this.window.buttonUnit1.Enabled = true;
			this.window.buttonUnit2.Enabled = true;
			this.window.buttonUnit3.Enabled = true;

			this.allGameObjects.Clear();

			SendInput();

			System.Diagnostics.Debug.WriteLine("Start() end");
		}

		public void Stop()
		{
			this.running = false;
		}

		protected void SendInput()
		{
			System.Diagnostics.Debug.WriteLine("SendInput()");

			// no input yet? to late, my input is "do nothing"
			if (this.myInput == null)
			{
				this.myInput = new InputEvent() { UnitTypePlaced = 0 };
			}
			// collect my own input (we are not going to send it to ourselves over the network!)
			this.collectedInputEvents[this.myPlayerNumber] = this.myInput;

			var message = new GameInput()
			{
				simStep = this.simStepCount + 1,
				myPlayerNumber = this.myPlayerNumber,
				simulationStepNumber = this.simStepCount,
				UnitPlaced = this.myInput.UnitTypePlaced,
				X = this.myInput.X,
				Y = this.myInput.Y
			};

			// client will only send to server (and server will distribute to all clients) 
			// server will send to all clients
			foreach (var peer in this.peers)
			{
				peer.Send(message);
			}

			this.myInput = null;
		}

		public void ReceiveGameInput(GameInput message)
		{
			System.Diagnostics.Debug.WriteLine("ReceiveGameInput()");

			if (!running)
			{
				System.Diagnostics.Debug.WriteLine("error called ReceiveGameInput() but game is not running");
			}

			if (message.simStep == simStepCount + 1)
			{
				// this is needed to do the next simulation step
				this.collectedInputEvents[message.myPlayerNumber] = new InputEvent
				{
					UnitTypePlaced = message.UnitPlaced,
					X = message.X,
					Y = message.Y
				};
			}
			else if (message.simStep > simStepCount + 1)
			{
				// not needed yet, delay
				this.futureEvents.Add(message);
			}
			else
			{
				// this should never happen - received input for a sim step that is already over
				throw new ApplicationException("received message from the past :(");
			}
		}

		public void NextSimulationStep()
		{
			System.Diagnostics.Debug.WriteLine("NextSimulationStep()");

			//Energy Regerneration
			if (this.playerEnergy < this.playerMaxEnergy)
				this.playerEnergy += 0.1f;
			else if (this.playerEnergy > this.playerMaxEnergy)
				this.playerEnergy = this.playerMaxEnergy;

			this.window.labelPlayerEnergy.Text = "Energy: " + Math.Round(this.playerEnergy, 1);

			if (!this.running)
				return;

			// proceed to next step ONLY if all inputs have arrived!
			foreach (var input in this.collectedInputEvents)
			{
				if (input == null)
				{
					// at least one input is missing
					return;
				}
			}

			// evaluate inputs
			for (int i = 0; i < this.collectedInputEvents.Length; ++i)
			{
				var input = this.collectedInputEvents[i];
				//Check what type of unit to place
				if (input.UnitTypePlaced == 1)
				{
					Soldier s = new Soldier1(this, i);
					if (i == this.myPlayerNumber)
						this.playerEnergy -= s.EnergyCost;

					this.allGameObjects.Add(new Soldier1(this, i)
					{
						X = input.X,
						Y = input.Y
					});
				}
				else if (input.UnitTypePlaced == 10)
				{
					Soldier s = new Soldier2(this, i);
					if (i == this.myPlayerNumber)
						this.playerEnergy -= s.EnergyCost;

					this.allGameObjects.Add(new Soldier2(this, i)
					{
						X = input.X,
						Y = input.Y
					});
				}
				else if (input.UnitTypePlaced == 11)
				{
					Soldier s = new Soldier3(this, i);
					if (i == this.myPlayerNumber)
						this.playerEnergy -= s.EnergyCost;

					this.allGameObjects.Add(new Soldier3(this, i)
					{
						X = input.X,
						Y = input.Y
					});
				}


				this.collectedInputEvents[i] = null;
			}

			// next simulation step
			this.simStepCount++;

			foreach (var g in allGameObjects)
			{
				g.NextSimulationStep();
			}

			//Clearup all corpses
			foreach (var g in deadGameObjects)
			{
				this.allGameObjects.Remove(g);
			}

			SendInput();

			// if we already have inputs for the new simulation step, use them now
			if (this.futureEvents.Count > 0)
			{
				var oldList = this.futureEvents;
				this.futureEvents = new List<GameInput>();
				foreach (var message in oldList)
				{
					ReceiveGameInput(message);
				}
			}

			//Check if the game has ended
			if (player0HP <= 0)
			{
				this.Stop();

				if (this.myPlayerNumber == 0)
					MessageBox.Show("Du habst verloren");
				else
					MessageBox.Show("Du habst gewonnen");
			}
			else if (player1HP <= 0)
			{
				this.Stop();

				if (this.myPlayerNumber == 0)
					MessageBox.Show("Du habst gewonnen");
				else
					MessageBox.Show("Du habst verloren");
			}
		}

		public void Render(Graphics g)
		{
			foreach (var o in allGameObjects)
			{
				o.Render(g);
			}
		}

		public void DestroySoldier(Soldier soldier)
		{
			this.deadGameObjects.Add(soldier);
		}

		//Used to Deal Damage to Players
		public void DealPlayerDamge(int playerNum, int dmg)
		{
			if (playerNum == 0)
			{
				this.player0HP -= dmg;
				this.window.labelPlayer0HP.Text = this.player0Name + ": " + this.player0HP;
			}
			else if (playerNum == 1)
			{
				this.player1HP -= dmg;
				this.window.labelPlayer1HP.Text = this.player1Name + ": " + this.player1HP;
			}
		}

		//Used to Find the Enemy within distance with lowest HP
		public Soldier FindEnemy(Soldier soldier, float distance)
		{
			Soldier output = null;
			foreach (GameObject obj in this.allGameObjects)
			{
				if (obj is Soldier)
				{
					Soldier s = (Soldier)obj;
					if (soldier.Player != s.Player && distance > Math.Sqrt(Math.Pow(soldier.X - s.X, 2) + Math.Pow(soldier.Y - s.Y, 2)) )
					{
						//Is withhin Range and an Enemy
						if (output == null || output.HP > s.HP) //And has the least HP
							output = s;
					}
				}
			}
			return output;
		}

		public void SpawnSoldier(float x, float y, int unitType)
		{
			if (!running)
				return;

			byte UnitPlaced = 0;

			switch (unitType) //Check if enough energy is available
			{
				case 1:
					Soldier sold = new Soldier1(this, this.myPlayerNumber);
					if (sold.EnergyCost <= this.playerEnergy)
					{
						UnitPlaced = 1;
					}
					break;
				case 2:
					Soldier sold2 = new Soldier2(this, this.myPlayerNumber);
					if (sold2.EnergyCost <= this.playerEnergy)
					{
						UnitPlaced = 10;
					}
					break;
				case 3:
					Soldier sold3 = new Soldier3(this, this.myPlayerNumber);
					if (sold3.EnergyCost <= this.playerEnergy)
					{
						UnitPlaced = 11;
					}
					break;
				default:
					UnitPlaced = 0;
					break;
			}

			this.myInput = new InputEvent()
			{
				UnitTypePlaced = UnitPlaced,
				X = x,
				Y = y
			};
		}

	}
}
