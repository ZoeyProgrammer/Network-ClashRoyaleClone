using System;
using System.Collections.Generic;
using System.Drawing;
using MyMultiPlayerGame.Messages;

namespace MyMultiPlayerGame.Game
{
	class Game
	{
		bool running;
		int simStepCount;
		List<GameObject> allGameObjects = new List<GameObject>();

		public class InputEvent
		{
			public byte UnitTypePlaced;         // 0 for none
			public float X;
			public float Y;
		}

		//All variables needed for the game
		public MainWindow window { get; set; }
		public int numPlayers { get; private set; }
		public int myPlayerNumber { get; private set; }
		public int boardWidth { get; private set; }
		public int boardHeight { get; private set; }
		public int player0HP { get; private set; } //Host HP
		public string player0Name { get; set; } //Host Username
		public int player1HP { get; private set; } //User HP
		public string player1Name { get; set; } //User Username
		public int playerEnergy { get; private set; }	//Player Energy
		public int playerMaxEnergy { get; private set; } //Maximum amount of Energy

		InputEvent[] collectedInputEvents;                              // events needed for next simulation
		List<GameInput> futureEvents = new List<GameInput>();           // events needed after next simulation

		InputEvent myInput = null;
		List<NetworkConnection> peers;

		//Setting all the standart Variables etc.
		public void Start(int numPlayers, int myPlayerNumber, List<NetworkConnection> peers)
		{
			System.Diagnostics.Debug.WriteLine("Start()");
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

			//Inititilize the visible HP values
			window.labelPlayer1HP.Text = this.player1Name + ": " + this.player1HP;
			window.labelPlayer0HP.Text = this.player0Name + ": " + this.player0HP;

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
				if (input.UnitTypePlaced > 0)
				{
					this.allGameObjects.Add(new Soldier(this, i)
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
		}

		public void Render(Graphics g)
		{
			foreach (var o in allGameObjects)
			{
				o.Render(g);
			}
		}

		public void SpawnSoldier(float x, float y)
		{
			if (!running)
				return;

			this.myInput = new InputEvent()
			{
				UnitTypePlaced = 1,
				X = x,
				Y = y
			};
		}

	}
}
