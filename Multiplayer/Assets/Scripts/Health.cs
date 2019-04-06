using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour {
    public const float MAX_HEALTH = 100;


    [SyncVar(hook = "OnChangeHealth")]
    public float hp = MAX_HEALTH;

    public Image healthBar;
    public bool destroyOnDeath;

    public void TakeDamage(float damage)
    {
        if (!isServer) return; 

        hp -= damage;
        if (hp<=0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                hp = MAX_HEALTH;
                RpcRespawn();
            }          
        }
    }

    [ClientRpc]
    public void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = new Vector3(UnityEngine.Random.Range(-15.0F, 15.00F), 0, Random.Range(-15.0F, 15.00F));
        }
    }

    public void OnChangeHealth(float currentHealth)
    {
        healthBar.fillAmount = currentHealth / MAX_HEALTH;
    }
}
