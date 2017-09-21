using System.Collections;
using System.Collections.Generic;
using Assets.GameAssets._Scripts.Managers;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public void Spawned()
    {
        Invoke("ReturnArrow", 1);
    }

    void ReturnArrow()
    {
        UnitPoolManager.Instance.ReturnArrow(this.gameObject);
        CancelInvoke();
    }
}
