using System.IO;

namespace MyMultiPlayerGame.Messages
{
	class ReadyGame : MessageBase
	{
		public override MessageTypes MessageType { get { return MessageTypes.ReadyGame; } }

		public string Sender;
		public bool readyStatus;

		public override void Send(BinaryWriter writer)
		{
			writer.Write(this.Sender);
			writer.Write(this.readyStatus);
		}

		public override void Receive(BinaryReader reader)
		{
			this.Sender = reader.ReadString();
			this.readyStatus = reader.ReadBoolean();
		}
	}
}
