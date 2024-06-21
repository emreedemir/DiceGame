using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUi : MonoBehaviour
{
    public Transform moveParent;

    public Image rewardImage;

    public TMPro.TMP_Text amount;

    public List<Sprite> rewardSprite;

    public void OnEnable()
    {
        PlayerDataProfile.OnHambuerAmountChangedValue += HandleAppleAmountChanged;

        PlayerDataProfile.OnPumpkinAmountChangedValue += HandlePearAmountChanged;

        PlayerDataProfile.OnPizzaAmountChangedValue += HandleStraberyAmountChanged;
    }

    public void OnDisable()
    {
        PlayerDataProfile.OnHambuerAmountChangedValue -= HandleAppleAmountChanged;

        PlayerDataProfile.OnPumpkinAmountChangedValue -= HandlePearAmountChanged;

        PlayerDataProfile.OnPizzaAmountChangedValue -= HandleStraberyAmountChanged;
    }


    public void HandleAppleAmountChanged(int rewardAmount)
    {
        amount.text = rewardAmount.ToString();

        rewardImage.sprite = rewardSprite[0];

        StartCoroutine(AnimationCoroutine());
    }

    public void HandlePearAmountChanged(int rewardAmount)
    {
        amount.text = rewardAmount.ToString();

        rewardImage.sprite = rewardSprite[1];

        StartCoroutine(AnimationCoroutine());
    }

    public void HandleStraberyAmountChanged(int rewardAmount)
    {
        amount.text = rewardAmount.ToString();

        rewardImage.sprite = rewardSprite[2];

        StartCoroutine(AnimationCoroutine());
    }


    IEnumerator AnimationCoroutine()
    {
        float duration = 1f;

        float elapsedTime = 0f;

        Vector3 initialPosition = moveParent.transform.position;

        Vector3 targetPosition = new Vector3(Screen.width/2f,initialPosition.y,initialPosition.z);

        while (elapsedTime<1f)
        {
            elapsedTime += Time.deltaTime;

            moveParent.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        elapsedTime = 0f;

        while (elapsedTime<1f)
        {
            elapsedTime += Time.deltaTime;

            moveParent.transform.position = Vector3.Lerp(targetPosition,initialPosition,elapsedTime/duration);

            yield return null;
        }
    }
}
