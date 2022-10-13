using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    float yOffset = 5f;

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null) return false;

        if(bank.CurrentBalance >= cost) { 
            Vector3 pos = new Vector3(position.x, position.y + yOffset, position.z);
            Instantiate(tower, pos, Quaternion.identity);

            bank.Withdraw(cost);

            return true;
        }

        return false;
    }
}
