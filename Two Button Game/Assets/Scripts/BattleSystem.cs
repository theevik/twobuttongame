using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {START, PLAYERTURN, ENEMYTURN }

public class BattleSystem : MonoBehaviour
{

    public BattleState state;


    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        state = BattleState.START;
        StartCoroutine(SetupBattle());

    }

   IEnumerator SetupBattle()
    {

        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
        

            }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2f);

        anim.Play("enemyhit");
    }


    void PlayerTurn()
    {

    }

    public void OnButtonOne()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

}
