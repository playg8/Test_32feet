/**
 * $File: Test_Bluetooth.cs $
 * $Date: #CREATIONDATE# $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *	                 Copyright © #CREATEYEAR# by Shen, Jen-Chieh $
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Bluetooth.Sdp;
using InTheHand.Net.Bluetooth.AttributeIds;
using System;
using System.Text;
using System.IO;

/// <summary>
/// Test for 32feet library.
/// </summary>
public class Test_Bluetooth
    : MonoBehaviour
{
    /* Variables */

    private BluetoothDeviceInfo mDevice = null;

    /* Setter & Getter */

    /* Functions */

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Test();
    }

    private string mac = "B8:27:EB:65:1B:47";

    private void Test()
    {
        var cli = new BluetoothClient();
        IReadOnlyCollection<BluetoothDeviceInfo> peers = cli.DiscoverDevices();

        print("Peers Length: " + peers.Count);

        foreach (var bdi in peers)
        {
            print("DN: " + bdi.DeviceName);

            if (bdi.DeviceName == "raspberrypi")
                mDevice = bdi;
        }

        if (mDevice != null)
            print("Address: " + mDevice.DeviceAddress);

        cli.Connect(mDevice.DeviceAddress, BluetoothService.SerialPort);

        NetworkStream stream = cli.GetStream();

        string msg = "Hello World!~";
        byte[] data = Encoding.ASCII.GetBytes(msg);
        stream.Write(data, 0, data.Length);
    }
}
