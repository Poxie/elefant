using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    [SerializeField] int currentHP = 1;

    public void TakeDamage() {
        currentHP -= 1;

        if(currentHP == 0) {
            Destroy(this.gameObject);
        }
    }
}
