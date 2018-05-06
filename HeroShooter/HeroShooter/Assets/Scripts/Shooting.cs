﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour
{
    public Transform cirvle;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Camera myCamera;
    public int Bulletspeed = 6;
    public float BulletLife = 2.0f;
    public bool ableToShoot = true;

    void Update()
    {  
        if (!isLocalPlayer)
        {
            return;
        }

        Aim();

        if(ableToShoot){
            if (Input.GetMouseButton(0))
            {
                CmdFire();
            }
        }
    }

    // This [Command] code is called on the Client …
    // … but it is run on the Server!
    [Command]
    public void CmdFire()
    {   
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * Bulletspeed;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, BulletLife);
    }
    
    public void Aim(){
        float x = Screen.width / 2;
        float y = Screen.height / 2;
        
        bulletSpawn.transform.LookAt(cirvle);
    }
}
