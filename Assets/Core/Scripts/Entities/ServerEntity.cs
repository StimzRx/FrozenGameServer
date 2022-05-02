using System.Collections;
using System.Collections.Generic;

using KableNet.Math;

using UnityEngine;

public class ServerEntity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Initialize( NetId netId )
    {
        this.NetId = netId;
    }
    
    public NetId NetId { get; private set; }
}
