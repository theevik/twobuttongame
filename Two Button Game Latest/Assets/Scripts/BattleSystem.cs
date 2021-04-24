using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//the battle state are the turns of the game. This is what makes this game's combat turn-based
public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST }

//the Character state is so we can switch between characters.
public enum CharacterState { Aero, Naiden, Beta, Frost }

<<<<<<< HEAD
=======
public enum ButtonState {Combat, Switch }

>>>>>>> Viktor
public class BattleSystem : MonoBehaviour
{

    public BattleState state;
    public CharacterState charState;
<<<<<<< HEAD
=======
    public ButtonState bState;

>>>>>>> Viktor

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

    public GameObject ActionsMenu;
<<<<<<< HEAD
=======
    public GameObject CharacterSelectionMenu;
>>>>>>> Viktor

    public int selectionChangeDelay = 2;
    public int currentSelection = 0;
    public float timer;
    public GameObject Player;

<<<<<<< HEAD
=======
    //chararcter selection button variables
    public GameObject CharSelectionIndicator;

    public int CharselectionChangeDelay = 2;
    public int CharcurrentSelection = 0;
    public float Chartimer;



>>>>>>> Viktor
    public int healAbility = 1;

    // Start is called before the first frame update
    void Start()
    {
        timer = selectionChangeDelay;
        state = BattleState.START;
        StartCoroutine(SetupBattle());

    }


    //this is the initial state of the game, spawns the characters that will participate in the battle.

<<<<<<< HEAD
    
   IEnumerator SetupBattle()
    {

       
       
       GameObject playerGO = playerPrefab;
       playerUnit = playerGO.GetComponent<Unit>();
=======

    IEnumerator SetupBattle()
    {

        CharacterSelectionMenu.SetActive(false);

        GameObject playerGO = playerPrefab;
        playerUnit = playerGO.GetComponent<Unit>();
>>>>>>> Viktor

        GameObject player1GO = playerPrefab1;
        player1Unit = player1GO.GetComponent<Unit1>();

        GameObject player2GO = playerPrefab2;
        player2Unit = player2GO.GetComponent<Unit2>();

        GameObject player3GO = playerPrefab3;
        player3Unit = player3GO.GetComponent<Unit3>();

        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = enemyGO.GetComponent<Unit>();
<<<<<<< HEAD
       
=======

>>>>>>> Viktor

        dialogueText.text = "PREPARE FOR BATTLE";

        playerHUD.SetHUD(playerUnit);
        player1HUD.SetHUD1(player1Unit);
        player2HUD.SetHUD2(player2Unit);
        player3HUD.SetHUD3(player3Unit);

        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        charState = CharacterState.Aero;
<<<<<<< HEAD
        PlayerTurn();
        
    }

    

    IEnumerator PlayerAttack()
    {
        

        if (charState == CharacterState.Aero)
        {
            
=======
        bState = ButtonState.Combat;
        PlayerTurn();

    }



    IEnumerator PlayerAttack()
    {


        if (charState == CharacterState.Aero)
        {

>>>>>>> Viktor

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
<<<<<<< HEAD
        else if(charState == CharacterState.Naiden)
=======
        else if (charState == CharacterState.Naiden)
>>>>>>> Viktor
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

<<<<<<< HEAD
     
=======

>>>>>>> Viktor
    //the enemy turn, currently the enemy can simply just hit back, NEEDS WORK!!!!
    IEnumerator EnemyTurn()
    {
        ActionsMenu.SetActive(false);


        yield return new WaitForSeconds(2f);

<<<<<<< HEAD
      

        

        if (playerUnit.mana <= 90 )
=======




        if (playerUnit.mana <= 90)
>>>>>>> Viktor
        {
            playerUnit.GainMana(playerUnit.gainmana);
            playerHUD.SetMana(playerUnit.mana);
        }

<<<<<<< HEAD
        if (player1Unit.mana <= 90 )
=======
        if (player1Unit.mana <= 90)
>>>>>>> Viktor
        {
            player1Unit.GainMana1(player1Unit.gainmana);
            player1HUD.SetMana1(player1Unit.mana);
        }

        if (player2Unit.mana <= 90)
        {
            player2Unit.GainMana2(player2Unit.gainmana);
            player2HUD.SetMana2(player2Unit.mana);
        }

        if (player3Unit.mana <= 90)
        {
            player3Unit.GainMana3(player3Unit.gainmana);
            player3HUD.SetMana3(player3Unit.mana);
        }

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


<<<<<<< HEAD
          
           
=======


>>>>>>> Viktor


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
<<<<<<< HEAD
        else if(charState == CharacterState.Frost)
=======
        else if (charState == CharacterState.Frost)
>>>>>>> Viktor
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
<<<<<<< HEAD
    
=======

>>>>>>> Viktor
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "YOU WON";
        }
        else if (state == BattleState.LOST)
<<<<<<< HEAD
            {
            dialogueText.text = "YOU LOST";
        }
    }
      void PlayerTurn()
=======
        {
            dialogueText.text = "YOU LOST";
        }
    }
    void PlayerTurn()
>>>>>>> Viktor
    {
        dialogueText.text = "YOUR TURN";
        ActionsMenu.SetActive(true);
    }


    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

<<<<<<< HEAD
    
=======

>>>>>>> Viktor

    void Update()
    {
        timer = timer - Time.deltaTime;
        if (timer <= 0.0f)
        {
            timer = selectionChangeDelay;

            currentSelection = currentSelection + 1;
<<<<<<< HEAD
            if (currentSelection > 2)
=======
            if (currentSelection > 3)
>>>>>>> Viktor
            {
                currentSelection = 0;
            }
        }

        if (currentSelection == 0)
        {
<<<<<<< HEAD
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
=======
            SelectionIndicator.transform.localPosition = new Vector3(-100, 60, 0);
        }
        if (currentSelection == 1)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, 20, 0);
        }
        if (currentSelection == 2)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, -20, 0);
        }

        if (currentSelection == 3)
        {
            SelectionIndicator.transform.localPosition = new Vector3(-100, -60, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) & state == BattleState.PLAYERTURN & bState == ButtonState.Combat)
>>>>>>> Viktor
        {
            if (currentSelection == 0)
            {

                if (state != BattleState.PLAYERTURN)
                    return;

                StartCoroutine(PlayerAttack());

            }
            if (currentSelection == 1 && playerUnit.mana >= 33)
            {
                if (charState == CharacterState.Aero)
                {

                    playerUnit.ConsumeMana(playerUnit.losemana);
                    playerHUD.SetMana(playerUnit.mana);

                    StartCoroutine(PlayerAttack());
                }
<<<<<<< HEAD
                
=======

>>>>>>> Viktor
                else if (charState == CharacterState.Naiden)
                {
                    player1Unit.ConsumeMana1(player1Unit.losemana);
                    player1HUD.SetMana1(player1Unit.mana);

                    StartCoroutine(PlayerAttack());
                }

                else if (charState == CharacterState.Beta)
                {
                    player2Unit.ConsumeMana2(player2Unit.losemana);
                    player2HUD.SetMana2(player2Unit.mana);

                    StartCoroutine(PlayerAttack());
                }

                else if (charState == CharacterState.Frost)
                {
                    player3Unit.ConsumeMana3(player3Unit.losemana);
                    player3HUD.SetMana3(player3Unit.mana);

                    StartCoroutine(PlayerAttack());
                }

            }

            //the character switch button

<<<<<<< HEAD
            if (currentSelection == 2 && charState == CharacterState.Aero)
=======
            /*if (currentSelection == 2 && charState == CharacterState.Aero)
>>>>>>> Viktor
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
<<<<<<< HEAD


          
            // Hide the panel
            //gameObject.SetActive(false);

            
=======
            */


            // Hide the panel
            //gameObject.SetActive(false);

            if (currentSelection == 3)
            {
                ActionsMenu.SetActive(false);
                CharacterSelectionMenu.SetActive(true);
                bState = ButtonState.Switch;
            }



        }


        /* Chartimer = Chartimer - Time.deltaTime;
         if (Chartimer <= 0.0f)
         {
             Chartimer = CharselectionChangeDelay;

             CharcurrentSelection = CharcurrentSelection + 1;
             if (CharcurrentSelection > 3)
             {
                 CharcurrentSelection = 0;
             }

         }*/

        CharcurrentSelection = currentSelection;


        if (CharcurrentSelection == 0)
        {
            CharSelectionIndicator.transform.localPosition = new Vector3(-100, 60, 0);
        }
        if (CharcurrentSelection == 1)
        {
            CharSelectionIndicator.transform.localPosition = new Vector3(-100, 20, 0);
        }
        if (CharcurrentSelection == 2)
        {
            CharSelectionIndicator.transform.localPosition = new Vector3(-100, -20, 0);
        }
        if (CharcurrentSelection == 3)
        {
            CharSelectionIndicator.transform.localPosition = new Vector3(-100, -60, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) & state == BattleState.PLAYERTURN)
        {
            if (CharcurrentSelection == 0 & bState == ButtonState.Switch)
            {
                //aero
                charState = CharacterState.Frost;
                bState = ButtonState.Combat;


                ActionsMenu.SetActive(true);
                CharacterSelectionMenu.SetActive(false);

                //switching stat panels
                player3HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);


                //switching the characters                
                playerPrefab3.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);

            }
            if (CharcurrentSelection == 1 & bState == ButtonState.Switch)
            {
                //naiden
                charState = CharacterState.Naiden;
                bState = ButtonState.Combat;

                ActionsMenu.SetActive(true);
                CharacterSelectionMenu.SetActive(false);

                //switching stat panels
                player1HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);

                //switching the characters
                playerPrefab1.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);
            }
            else if (CharcurrentSelection == 2 & bState == ButtonState.Switch)
            {
                //beta
                charState = CharacterState.Beta;
                bState = ButtonState.Combat;

                ActionsMenu.SetActive(true);
                CharacterSelectionMenu.SetActive(false);

                //switching stat panels
                player2HUD.transform.localPosition = new Vector3(-409, 87, 0);

                playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                player3HUD.transform.localPosition = new Vector3(-1600, 0, 0);


                //switching the characters                
                playerPrefab2.transform.localPosition = new Vector3(-3, 1, -6);

                playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                playerPrefab3.transform.localPosition = new Vector3(-20, 1, -6);
            }
            if (CharcurrentSelection == 3 & bState == ButtonState.Switch)
            {
                //frost
                /* charState = CharacterState.Frost;
                 bState = ButtonState.Combat;


                 ActionsMenu.SetActive(true);
                 CharacterSelectionMenu.SetActive(false);

                 //switching stat panels
                 player3HUD.transform.localPosition = new Vector3(-409, 87, 0);

                 playerHUD.transform.localPosition = new Vector3(-1600, 0, 0);
                 player1HUD.transform.localPosition = new Vector3(-1600, 0, 0);
                 player2HUD.transform.localPosition = new Vector3(-1600, 0, 0);


                 //switching the characters                
                 playerPrefab3.transform.localPosition = new Vector3(-3, 1, -6);

                 playerPrefab.transform.localPosition = new Vector3(-20, 1, -6);
                 playerPrefab1.transform.localPosition = new Vector3(-20, 1, -6);
                 playerPrefab2.transform.localPosition = new Vector3(-20, 1, -6);
             }*/


                //gameObject.SetActive(false);
            }
>>>>>>> Viktor
        }
    }
}
