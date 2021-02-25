using System.IO;

namespace MyMultiPlayerGame.Messages
{
    class StartGame : MessageBase
    {
        public override MessageTypes MessageType { get { return MessageTypes.StartGame; } }

        public int numPlayers;
        public int myPlayerNumber;

        public override void Send(BinaryWriter writer)
        {
            writer.Write(this.numPlayers);
            writer.Write(this.myPlayerNumber);
        }

        public override void Receive(BinaryReader reader)
        {
            this.numPlayers = reader.ReadInt32();
            this.myPlayerNumber = reader.ReadInt32();
        }
    }
}
