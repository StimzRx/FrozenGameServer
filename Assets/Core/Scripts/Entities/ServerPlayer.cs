using System;
using System.Net.Sockets;

using Core.Scripts.Entities;
using Core.Scripts.Events.Entity;
using Core.Scripts.Networking;

using KableNet.Common;
using KableNet.Math;

using UnityEngine;

public class ServerPlayer : ServerEntity
{
    public ServerPlayer( EntityWrapper wrapper, NetId netId, NetPlayer netPlayer ) : base( wrapper, netId )
    {
        this.NetPlayer = netPlayer;
    }
    
    public NetPlayer NetPlayer { get; }
}
