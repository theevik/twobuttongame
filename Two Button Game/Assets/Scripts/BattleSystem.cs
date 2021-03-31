using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//the battle state are the turns of the game. This is what makes this game's combat turn-based
public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST }

//the Character state is so we can switch between characters.
public enum CharacterState { Aero, Naiden, Beta, Frost }

public class BattleSystem : MonoBehaviour
{

    public BattleState state;

    public GameObject playerPrefab, playerPrefab1, playerPrefab2, playerPrefab3;
    public GameObject enemyPrefab;

    public Text dialogueText;

    //refers to the unit scripts (we need them to give stats to each character/enemy)

    Unit playerUnit;
    Unit1 player1Unit;
    Unit2 player2Unit;
    Unit3 player3Unit;

    Unit enemyUnit;

    //refers to the battle hud scripts (with them we transfer the character/enemy stats to their huds)

    public BattleHUD playerHUD;
    public BattleHUD1 player1HUD;
    public BattleHUD2 player2HUD;
    public BattleHUD3 player3HUD;

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


    //this is the initial state of the game, spawns the characters that will participate in the battle.

   IEnumerator SetupBattle()
    {

       
       
        GameObject playerGO = Instantiate(playerPrefab);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject player1GO = Instantiate(playerPrefab1);
        player1Unit = player1GO.GetComponent<Unit1>();

        GameObject player2GO = Instantiate(playerPrefab2);
        player2Unit = player2GO.GetComponent<Unit2>();

        GameObject player3GO = Instantiate(playerPrefab3);
        player3Unit = player3GO.GetComponent<Unit3>();

        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "PREPARE FOR BATTLE";

        playerHUD.SetHUD(playerUnit);
        player1HUD.SetHUD1(player1Unit);
        player2HUD.SetHUD2(player2Unit);
        player3HUD.SetHUD3(player3Unit);

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


    //the enemy turn, currently the enemy can simply just hit back, NEEDS WORK!!!!
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

                


              
            }

            // Hide the panel
           //gameObject.SetActive(false);


        }
    }
}
