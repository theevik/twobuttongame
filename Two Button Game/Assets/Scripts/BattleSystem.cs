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
    public CharacterState charState;

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

    public int healAbility = 1;

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

       
       
       GameObject playerGO = playerPrefab;
       playerUnit = playerGO.GetComponent<Unit>();

        GameObject player1GO = playerPrefab1;
        player1Unit = player1GO.GetComponent<Unit1>();

        GameObject player2GO = playerPrefab2;
        player2Unit = player2GO.GetComponent<Unit2>();

        GameObject player3GO = playerPrefab3;
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
        charState = CharacterState.Aero;
        PlayerTurn();
        
    }

    

    IEnumerator PlayerAttack()
    {
        if (charState == CharacterState.Aero)
        {
            bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

            enemyHUD.SetHP(enemyUnit.currentHP);


            //yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else if(charState == CharacterState.Naiden)
        {
            bool isDead = enemyUnit.TakeDamage(player1Unit.damage);

            enemyHUD.SetHP(enemyUnit.currentHP);


            //yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

        else if (charState == CharacterState.Beta)
        {
            bool isDead = enemyUnit.TakeDamage(player2Unit.damage);

            enemyHUD.SetHP(enemyUnit.currentHP);


            //yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

        else if (charState == CharacterState.Frost)
        {
            bool isDead = enemyUnit.TakeDamage(player3Unit.damage);

            enemyHUD.SetHP(enemyUnit.currentHP);


            yield return new WaitForSeconds(0f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

     
    //the enemy turn, currently the enemy can simply just hit back, NEEDS WORK!!!!
    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2f);

        //enemy heals once when under 60 health
        if (enemyUnit.currentHP <= 60 && healAbility > 0)
        {
            enemyUnit.Heal(enemyUnit.healing);
            healAbility = 0;

            enemyHUD.SetHP(enemyUnit.currentHP);
        }

        if (charState == CharacterState.Aero)
        {
            dialogueText.text = "Enemy Is Attacking";

            yield return new WaitForSeconds(2f);


          
           


            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
        else if (charState == CharacterState.Naiden)
        {
            dialogueText.text = "Enemy Is Attacking";

            yield return new WaitForSeconds(2f);




            bool isDead = player1Unit.TakeDamage(enemyUnit.damage);

            player1HUD.SetHP1(player1Unit.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
        else if (charState == CharacterState.Beta)
        {
            dialogueText.text = "Enemy Is Attacking";

            yield return new WaitForSeconds(2f);




            bool isDead = player2Unit.TakeDamage(enemyUnit.damage);

            player2HUD.SetHP2(player2Unit.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
        else if(charState == CharacterState.Frost)
        {
            dialogueText.text = "Enemy Is Attacking";

            yield return new WaitForSeconds(2f);




            bool isDead = player3Unit.TakeDamage(enemyUnit.damage);

            player3HUD.SetHP3(player3Unit.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
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

        if (Input.GetKeyDown(KeyCode.Space) & state == BattleState.PLAYERTURN)
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
            
            //the character switch button

            if (currentSelection == 2 && charState == CharacterState.Aero)
            {

                Debug.Log("Switch to Naiden");
                charState = CharacterState.Naiden;

                //switching stat panels
                player1HUD.transform.localPosition = new Vector3(-409  , 87, 0);
                playerHUD.transform.localPosition = new Vector3(-1600 ,0 ,0);

                //switching the characters
                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab1.transform.localPosition = new Vector3(-3, 1, -6);

            }
           

            else if (currentSelection == 2 && charState == CharacterState.Naiden)
            {

                Debug.Log("Switch to Beta");
                charState = CharacterState.Beta;

                //switching stat panels
                player2HUD.transform.localPosition = new Vector3(-409, 87, 0);
                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters
                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-3, 1, -6);
            }

            else if (currentSelection == 2 && charState == CharacterState.Beta)
            {

                Debug.Log("Switch to Frost");
                charState = CharacterState.Frost;

                //switching stat panels
                player3HUD.transform.localPosition = new Vector3(-409, 87, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-3, 1, -6);

            }

            else if (currentSelection == 2 && charState == CharacterState.Frost)
            {

                Debug.Log("Switch to Aero");
                charState = CharacterState.Aero;

                //switching stat panels
                playerHUD.transform.localPosition = new Vector3(-409, 87, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab.transform.localPosition = new Vector3(-3, 1, -6);

            }


          
            // Hide the panel
            //gameObject.SetActive(false);

            
        }
    }
}
