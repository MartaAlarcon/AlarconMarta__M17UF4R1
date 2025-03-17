using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ChaseState", menuName = "StatesSO/Chase")]
public class ChaseState : StateSO
{
    public override void OnStateEnter(EnemyController ec)
    {
    }

    public override void OnStateExit(EnemyController ec)
    {
        ec.GetComponent<ChaseBehaviour>().StopChasing();
    }

    public override void OnStateUpdate(EnemyController ec)
    {
        GameManager.gm.UpdateText("Ven que te quiero decir una cosa");
        ec.GetComponent<ChaseBehaviour>().Chase(ec.target.transform, ec.transform);
    }
}
