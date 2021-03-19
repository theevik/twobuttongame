using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Text dialogueText;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public GameObject SelectionIndicator;
    public GameObject OptionButton1;
    public GameObject OptionButton2;
    public GameObject OptionButton3;

    public int selectionChangeDelay = 2;
    public int currentSelection = 0;
    public float timer;
    public GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        timer = selectionChangeDelay;
        state = BattleState.START;
        StartCoroutine(SetupBattle());

    }


   IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "Battle Starts";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
        



    }

    IEnumerator PlayerAttack()
    {
        

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);

        

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        } else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = "Enemy Is Attacking";

        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "YOU WON";
        }
        else if (state == BattleState.LOST)
            {
            dialogueText.text = "YOU LOST";
        }
    }
      void PlayerTurn()
    {
        dialogueText.text = "YOUR TURN";
    }


    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    void Update()
    {
        timer = timer - Time.deltaTime;
        if (timer <= 0.0f)
        {
            timer = selectionChangeDelay;

            currentSelection = currentSelection + 1;
            if (currentSelection > 2)
            {
                currentSelection = 0;
            }
        }

        if (currentSelection == 0)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, 40, 0);
        }
        if (currentSelection == 1)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, 0, 0);
        }
        if (currentSelection == 2)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, -40, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentSelection == 0)
            {

                if (state != BattleState.PLAYERTURN)
                    return;

                StartCoroutine(PlayerAttack());

            }
            if (currentSelection == 1)
            {
                Debug.Log("you selected the second option");
            }
            if (currentSelection == 2)
            {
                Debug.Log("you selected the third option");
            }

            // Hide the panel
           gameObject.SetActive(false);


        }
    }
}
