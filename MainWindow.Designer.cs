namespace MyMultiPlayerGame
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.textBoxServerAdress = new System.Windows.Forms.TextBox();
			this.buttonOpenServer = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxChatInput = new System.Windows.Forms.TextBox();
			this.buttonSend = new System.Windows.Forms.Button();
			this.listBoxChat = new System.Windows.Forms.ListBox();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.buttonStartGame = new System.Windows.Forms.Button();
			this.buttonReady = new System.Windows.Forms.Button();
			this.textBoxUsername = new System.Windows.Forms.TextBox();
			this.labelPlayer0HP = new System.Windows.Forms.Label();
			this.labelPlayer1HP = new System.Windows.Forms.Label();
			this.labelPlayerEnergy = new System.Windows.Forms.Label();
			this.buttonUnit1 = new System.Windows.Forms.Button();
			this.buttonUnit2 = new System.Windows.Forms.Button();
			this.buttonUnit3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(118, 9);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(75, 23);
			this.buttonConnect.TabIndex = 0;
			this.buttonConnect.Text = "Connect";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// textBoxServerAdress
			// 
			this.textBoxServerAdress.Location = new System.Drawing.Point(12, 12);
			this.textBoxServerAdress.Name = "textBoxServerAdress";
			this.textBoxServerAdress.Size = new System.Drawing.Size(100, 20);
			this.textBoxServerAdress.TabIndex = 1;
			this.textBoxServerAdress.Text = "127.0.0.1";
			this.textBoxServerAdress.TextChanged += new System.EventHandler(this.textBoxServerAdress_TextChanged);
			// 
			// buttonOpenServer
			// 
			this.buttonOpenServer.Location = new System.Drawing.Point(276, 9);
			this.buttonOpenServer.Name = "buttonOpenServer";
			this.buttonOpenServer.Size = new System.Drawing.Size(75, 23);
			this.buttonOpenServer.TabIndex = 2;
			this.buttonOpenServer.Text = "Open Server";
			this.buttonOpenServer.UseVisualStyleBackColor = true;
			this.buttonOpenServer.Click += new System.EventHandler(this.buttonOpenServer_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(217, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "... or ...";
			// 
			// textBoxChatInput
			// 
			this.textBoxChatInput.Location = new System.Drawing.Point(12, 596);
			this.textBoxChatInput.Name = "textBoxChatInput";
			this.textBoxChatInput.Size = new System.Drawing.Size(461, 20);
			this.textBoxChatInput.TabIndex = 4;
			// 
			// buttonSend
			// 
			this.buttonSend.Location = new System.Drawing.Point(479, 593);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(75, 23);
			this.buttonSend.TabIndex = 5;
			this.buttonSend.Text = "Send";
			this.buttonSend.UseVisualStyleBackColor = true;
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// listBoxChat
			// 
			this.listBoxChat.FormattingEnabled = true;
			this.listBoxChat.Location = new System.Drawing.Point(12, 482);
			this.listBoxChat.Name = "listBoxChat";
			this.listBoxChat.Size = new System.Drawing.Size(542, 108);
			this.listBoxChat.TabIndex = 6;
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// buttonStartGame
			// 
			this.buttonStartGame.Location = new System.Drawing.Point(398, 10);
			this.buttonStartGame.Name = "buttonStartGame";
			this.buttonStartGame.Size = new System.Drawing.Size(75, 23);
			this.buttonStartGame.TabIndex = 7;
			this.buttonStartGame.Text = "Start Game";
			this.buttonStartGame.UseVisualStyleBackColor = true;
			this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
			// 
			// buttonReady
			// 
			this.buttonReady.Location = new System.Drawing.Point(491, 11);
			this.buttonReady.Name = "buttonReady";
			this.buttonReady.Size = new System.Drawing.Size(75, 23);
			this.buttonReady.TabIndex = 8;
			this.buttonReady.Text = "Ready";
			this.buttonReady.UseVisualStyleBackColor = true;
			this.buttonReady.Click += new System.EventHandler(this.buttonReady_Click);
			// 
			// textBoxUsername
			// 
			this.textBoxUsername.Location = new System.Drawing.Point(12, 38);
			this.textBoxUsername.Name = "textBoxUsername";
			this.textBoxUsername.Size = new System.Drawing.Size(100, 20);
			this.textBoxUsername.TabIndex = 9;
			this.textBoxUsername.Text = "username";
			this.textBoxUsername.TextChanged += new System.EventHandler(this.textBoxUsername_TextChanged);
			// 
			// labelPlayer0HP
			// 
			this.labelPlayer0HP.AutoSize = true;
			this.labelPlayer0HP.Location = new System.Drawing.Point(71, 79);
			this.labelPlayer0HP.Name = "labelPlayer0HP";
			this.labelPlayer0HP.Size = new System.Drawing.Size(82, 13);
			this.labelPlayer0HP.TabIndex = 10;
			this.labelPlayer0HP.Text = "Username0: HP";
			// 
			// labelPlayer1HP
			// 
			this.labelPlayer1HP.AutoSize = true;
			this.labelPlayer1HP.Location = new System.Drawing.Point(414, 79);
			this.labelPlayer1HP.Name = "labelPlayer1HP";
			this.labelPlayer1HP.Size = new System.Drawing.Size(82, 13);
			this.labelPlayer1HP.TabIndex = 11;
			this.labelPlayer1HP.Text = "Username1: HP";
			// 
			// labelPlayerEnergy
			// 
			this.labelPlayerEnergy.AutoSize = true;
			this.labelPlayerEnergy.Location = new System.Drawing.Point(635, 98);
			this.labelPlayerEnergy.Name = "labelPlayerEnergy";
			this.labelPlayerEnergy.Size = new System.Drawing.Size(79, 13);
			this.labelPlayerEnergy.TabIndex = 12;
			this.labelPlayerEnergy.Text = "Energy: Energy";
			this.labelPlayerEnergy.Click += new System.EventHandler(this.label2_Click);
			// 
			// buttonUnit1
			// 
			this.buttonUnit1.Location = new System.Drawing.Point(638, 130);
			this.buttonUnit1.Name = "buttonUnit1";
			this.buttonUnit1.Size = new System.Drawing.Size(75, 23);
			this.buttonUnit1.TabIndex = 13;
			this.buttonUnit1.Text = "Unit1";
			this.buttonUnit1.UseVisualStyleBackColor = true;
			this.buttonUnit1.Click += new System.EventHandler(this.buttonUnit1_Click);
			// 
			// buttonUnit2
			// 
			this.buttonUnit2.Location = new System.Drawing.Point(638, 159);
			this.buttonUnit2.Name = "buttonUnit2";
			this.buttonUnit2.Size = new System.Drawing.Size(75, 23);
			this.buttonUnit2.TabIndex = 14;
			this.buttonUnit2.Text = "Unit2";
			this.buttonUnit2.UseVisualStyleBackColor = true;
			this.buttonUnit2.Click += new System.EventHandler(this.buttonUnit2_Click);
			// 
			// buttonUnit3
			// 
			this.buttonUnit3.Location = new System.Drawing.Point(639, 188);
			this.buttonUnit3.Name = "buttonUnit3";
			this.buttonUnit3.Size = new System.Drawing.Size(75, 23);
			this.buttonUnit3.TabIndex = 15;
			this.buttonUnit3.Text = "Unit3";
			this.buttonUnit3.UseVisualStyleBackColor = true;
			this.buttonUnit3.Click += new System.EventHandler(this.buttonUnit3_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(829, 628);
			this.Controls.Add(this.buttonUnit3);
			this.Controls.Add(this.buttonUnit2);
			this.Controls.Add(this.buttonUnit1);
			this.Controls.Add(this.labelPlayerEnergy);
			this.Controls.Add(this.labelPlayer1HP);
			this.Controls.Add(this.labelPlayer0HP);
			this.Controls.Add(this.textBoxUsername);
			this.Controls.Add(this.buttonReady);
			this.Controls.Add(this.buttonStartGame);
			this.Controls.Add(this.listBoxChat);
			this.Controls.Add(this.buttonSend);
			this.Controls.Add(this.textBoxChatInput);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonOpenServer);
			this.Controls.Add(this.textBoxServerAdress);
			this.Controls.Add(this.buttonConnect);
			this.Name = "MainWindow";
			this.Text = "Form1";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ApplicationWindow_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxServerAdress;
        private System.Windows.Forms.Button buttonOpenServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxChatInput;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ListBox listBoxChat;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button buttonStartGame;
		private System.Windows.Forms.Button buttonReady;
		private System.Windows.Forms.TextBox textBoxUsername;
		public System.Windows.Forms.Label labelPlayer0HP;
		public System.Windows.Forms.Label labelPlayer1HP;
		public System.Windows.Forms.Label labelPlayerEnergy;
		public System.Windows.Forms.Button buttonUnit1;
		public System.Windows.Forms.Button buttonUnit2;
		public System.Windows.Forms.Button buttonUnit3;
	}
}

