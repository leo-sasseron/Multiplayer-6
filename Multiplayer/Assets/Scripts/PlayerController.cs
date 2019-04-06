using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform muzzle;

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer) return;
        var rot = Input.GetAxis("Horizontal") * 150 * Time.deltaTime;
        var speed = Input.GetAxis("Vertical") * 3 * Time.deltaTime;

        transform.Rotate(0, rot, 0);
        transform.Translate(0, 0, speed);

        if(Input.GetMouseButtonDown(0))
        {
            CmdFire();
        }
	}

    [Command]
    public void CmdFire()
    {
        var bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as GameObject;

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 5);

    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.cyan;
    }
}
