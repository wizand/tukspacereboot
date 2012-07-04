using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Lidgren.Network;

namespace tukSpace
{
    public class tukNetwork
    {
        public bool UseNetworking = false;
        private bool NetworkInitialized;

        private NetPeerConfiguration Config;
        private NetServer Server;
        private NetClient Client;

        private Scenarios.Scenario theScenario;

        private bool ServerMode;

        public void Initialize(bool IamServer, Scenarios.Scenario tScenario)
        {
            //make sure we are using networking and we haven't initialized yet
            if (!UseNetworking && !NetworkInitialized)
            {
                return;
            }

            Config = new NetPeerConfiguration("tukSpace");
            Config.Port = 14511;

            if (IamServer)
            {
                Server = new NetServer(Config);
                Server.Start();
            }
            else
            {
                Client = new NetClient(Config);
                Client.Start();
            }

            theScenario = tScenario;
            ServerMode = IamServer;
            NetworkInitialized = true;
        }

        public void Update(GameTime gameTime)
        {
            if (ServerMode)
            {
                //we will probably have to thread this, chances are itll end up being too slow (one message per Update() call)
                NetIncomingMessage im = Server.ReadMessage();

                UpdateWorld(im);

                //now lets send data out to clients
                foreach (NetConnection Client in Server.Connections)
                {
                    //make some message, grab data from world is what we will do
                    NetOutgoingMessage om = Server.CreateMessage();
                    //add our shit here
                    SendMessage(om, Client); //send it!
                }
            }
            else
            {
                //since we're a client, we just need to read the data then apply it everywhere we need to!
                NetIncomingMessage im = Client.ReadMessage();
                UpdateWorld(im);
            }
        }

        public void UpdateWorld(NetIncomingMessage im)
        {
            //we have to extract data from im and apply it to world
        }


        public NetIncomingMessage ReadMessage()
        {
            if (ServerMode)
            {
                return Server.ReadMessage();
            }
            else
            {
                return Client.ReadMessage();
            }
        }

        //SendMessage has 2 versions, no recipient means we're in client mode
        //recipient means we're in server mode
        public void SendMessage(NetOutgoingMessage om)
        {
            Client.SendMessage(om, NetDeliveryMethod.UnreliableSequenced);
        }

        public void SendMessage(NetOutgoingMessage om, NetConnection recipient)
        {
            Server.SendMessage(om, recipient, NetDeliveryMethod.UnreliableSequenced);
        }

    }
}
