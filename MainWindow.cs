using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using MyMultiPlayerGame.Messages;

namespace MyMultiPlayerGame
{
	public partial class MainWindow : Form
	{
		private const int SERVERPORT = 1996;

		private TcpListener listener =
				new TcpListener(IPAddress.Parse("127.0.0.1"), SERVERPORT);

		private NetworkConnection connection;
		private Game.Game myGame = new Game.Game();
		private Canvas canvas;
		private bool isServer;

		private ConcurrentQueue<MessageBase> IncomingNetworkMessages = new ConcurrentQueue<MessageBase>();

		public MainWindow()
		{
			InitializeComponent();

			this.myGame.window = this; //Make this available

			this.canvas = new Canvas(this.myGame);
			this.canvas.Size = new Size(600, 300);
			this.canvas.Location = new Point(10, 100);
			this.Controls.Add(this.canvas);

			this.buttonStartGame.Enabled = false;
			this.buttonReady.Enabled = false;
		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			var client = new TcpClient();
			client.Connect("127.0.0.1", SERVERPORT);
			MessageBox.Show("Connected to Server!!!");

			connection = new NetworkConnection(client);
			connection.MessageReceived += ConnectionOnMessageReceived;

			this.buttonOpenServer.Enabled = false;
			this.buttonConnect.Enabled = false;
			this.textBoxUsername.Enabled = false; //Dont allow changing of username after a connection has been established
			this.buttonReady.Enabled = true;  //Allow readyness when connection has been established

			this.myGame.player1Name = this.textBoxUsername.Text; //Set the Playername for the rest of the game
		}

		private void buttonOpenServer_Click(object sender, EventArgs e)
		{
			isServer = true;
			listener.Start();
			var connectedClient = this.listener.AcceptTcpClient();

			MessageBox.Show("Client has connected!!!");

			connection = new NetworkConnection(connectedClient);
			connection.MessageReceived += ConnectionOnMessageReceived;

			this.buttonOpenServer.Enabled = false;
			this.buttonConnect.Enabled = false;
			this.textBoxUsername.Enabled = false; //Dont allow changing of username after a connection has been established
			this.myGame.player0Name = this.textBoxUsername.Text; //Set the Playername for the rest of the game

			//Automatically set Ready, since host needs to start anyways.
			listBoxChat.Items.Add(textBoxUsername.Text + " (you) " + "is ready");
			buttonReady.Text = "Unready";

			//Sends a Ready Message
			connection.Send(new ReadyGame() { Sender = textBoxUsername.Text, readyStatus = true });
		}

		private void ConnectionOnMessageReceived(MessageBase message)
		{
			// executed by network thread!
			IncomingNetworkMessages.Enqueue(message);
		}

		private void buttonSend_Click(object sender, EventArgs e)
		{
			if (connection != null)
			{
				//Show the Message in own Message feed
				listBoxChat.Items.Add(textBoxUsername.Text + " (you): " + textBoxChatInput.Text);

				connection.Send(new ChatMessage()
				{
					//Changed Sender to the Username Textbox to show the correct name
					Sender = textBoxUsername.Text,
					Text = textBoxChatInput.Text
				});
			}
		}

		//Here all Network Messages get processed - So every kind of communication takes place here
		private void ProcessNetworkMessages()
		{
			while (IncomingNetworkMessages.TryDequeue(out var message))
			{
				if (message is ChatMessage)
				{
					var c = (ChatMessage)message;
					listBoxChat.Items.Add(c.Sender + ": " + c.Text);
				}
				else if (message is StartGame)
				{
					StartGame m = (StartGame)message;
					// I am a client, StartGame comes from server
					this.myGame.Start(m.numPlayers, m.myPlayerNumber, new List<NetworkConnection>() { connection });

					//Show that the Game has been started by sending a message in chat
					listBoxChat.Items.Add("The Host has started the game!");

					buttonReady.Enabled = false; //Readyness is manditory at this point
				}
				else if (message is ReadyGame)
				{
					var r = (ReadyGame)message;

					if (isServer)
						this.myGame.player1Name = r.Sender;
					else
						this.myGame.player0Name = r.Sender;

					if (r.readyStatus) //If the other person sends a ready signal
					{
						listBoxChat.Items.Add(r.Sender + " is ready");
						if (isServer)//Unlock the ability to Start the game if you are host here
						{
							buttonStartGame.Enabled = true;
						}
					}
					else
					{
						listBoxChat.Items.Add(r.Sender + " is no longer ready");
						if (isServer)//Lock the ability to Start the game if you are host here
						{
							buttonStartGame.Enabled = false;
						}
					}
				}
				else if (message is GameInput)
				{
					this.myGame.ReceiveGameInput((GameInput)message);
				}
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			this.BeginInvoke(new Action(() =>
			{
				ProcessNetworkMessages();

				this.myGame.NextSimulationStep();

				this.canvas.Invalidate();
			}));
		}

		private void ApplicationWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (connection != null)
			{
				connection.Dispose();
			}
		}

		private void buttonStartGame_Click(object sender, EventArgs e)
		{
			if (isServer)
			{
				this.buttonStartGame.Enabled = false;

				this.BeginInvoke(new Action(() =>
				{
					myGame.Start(2, 0, new List<NetworkConnection>() { connection });
				}));


				this.connection.Send(new StartGame()
				{
					numPlayers = 2,
					myPlayerNumber = 1
				});

				//Push a Message in chat, just because it's always good to have all information logged
				listBoxChat.Items.Add("The Host has started the game!");
			}
		}

		//Used to send Readyness status
		private void buttonReady_Click(object sender, EventArgs e)
		{
			if (connection != null)
			{
				if (buttonReady.Text == "Ready")
				{
					//Show the Message in own Message feed
					listBoxChat.Items.Add(textBoxUsername.Text + " (you) " + "is ready");
					buttonReady.Text = "Unready";

					//Sends a Ready Message
					connection.Send(new ReadyGame() {Sender = textBoxUsername.Text, readyStatus = true} );
				}
				else
				{
					//Show the Message in own Message feed
					listBoxChat.Items.Add(textBoxUsername.Text + " (you) " + "is no longer ready");
					buttonReady.Text = "Ready";

					//Sends a Ready Message
					connection.Send(new ReadyGame() { Sender = textBoxUsername.Text, readyStatus = false });
				}
			}
		}

		private void textBoxUsername_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBoxServerAdress_TextChanged(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}
	}
}
