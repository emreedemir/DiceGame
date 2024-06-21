using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceUi : MonoBehaviour
{
    public List<Sprite> dices;

    public Image dice1Image;

    public Image dice2Image;

    public TutorialHand tutorialHand;

    public  DiceController diceController;

    public Camera camera;

    public Button retyButton;

    public GameObject warningText;

    private void OnEnable()
    {
        DiceController.OnDice1ValueChanged += SetDice1View;

        DiceController.OnDice2ValueChanged += SetDice2View;

        SetDice1View(DiceController.diceTarget1);

        SetDice2View(DiceController.diceTarget2);

        if (PlayerDataProfile.IsDiceChangeTutorialCompleted == false)
        {

            StartCoroutine(DiceTutorialCoroutine());
        }
    }

    IEnumerator DiceTutorialCoroutine()
    {
        retyButton.gameObject.SetActive(false);
        int diceTarget1 = DiceController.diceTarget1;

        int diceTarget2 = DiceController.diceTarget2;

        tutorialHand.swipe = false;

        tutorialHand.gameObject.SetActive(true);

        diceController.dices[0].diceThrow.locked = true;

        diceController.dices[1].diceThrow.locked = true;

        yield return new WaitUntil(() => (DiceController.diceTarget1 != diceTarget1 || DiceController.diceTarget2 != diceTarget2) == true);

        PlayerDataProfile.IsDiceChangeTutorialCompleted = true;

        tutorialHand.gameObject.SetActive(false);

        tutorialHand.swipe = true;

        Dice dice = diceController.dices[0];

        tutorialHand.doMoveStartPosition = camera.WorldToScreenPoint(dice.transform.position+new Vector3(1,0,0));

        tutorialHand.doMoveFinishPositio = camera.WorldToScreenPoint(dice.transform.position+new Vector3(1,3,0));

        tutorialHand.gameObject.SetActive(true);

        diceController.dices[0].diceThrow.locked = false;

        diceController.dices[1].diceThrow.locked = false;

        yield return new WaitUntil(() => diceController.IsAnyDiceThrowed()==true);

        tutorialHand.gameObject.SetActive(false);

        retyButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        DiceController.OnDice1ValueChanged -= SetDice1View;

        DiceController.OnDice1ValueChanged -= SetDice2View;
    }

    private void CloseWarning()
    {
        warningText.gameObject.SetActive(false);
    }

    public void HandlePressedDice1()
    {
        if (!DiceController.dicePlayable)
        {
            warningText.gameObject.SetActive(true);
            CancelInvoke();
            Invoke("CloseWarning", 1f);
            return;
        }
           

         DiceController.diceTarget1++;
    }

    public void HandlePressedDice2()
    {
        if (!DiceController.dicePlayable)
        {
            warningText.gameObject.SetActive(true);

            CancelInvoke();

            Invoke("CloseWarning",1f);
            return;
        }
           

        DiceController.diceTarget2++;
    }

    public void SetDice1View(int value)
    {
        value--;

        dice1Image.sprite = dices[value];
    }


    public void SetDice2View(int value)
    {
        value--;

        dice2Image.sprite = dices[value];
    }

    

}
