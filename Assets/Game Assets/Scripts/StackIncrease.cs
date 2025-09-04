using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackIncrease : MonoBehaviour
{
    [SerializeField] private GameObject stackPos;
    [SerializeField] private GameObject dollarEffect;
    [SerializeField] private GameObject moneyIndicator;
    [SerializeField] private GameObject moneyIndicatorPos;
    [SerializeField] private GameObject playerBlock;

    private float stackCheckpoint = 100f;
    public bool endingCalculating = false;

    private void Update()
    {
        if (endingCalculating) return;

        var stackController = stackPos.GetComponent<StackPosController>();
        var playerPower = GetComponent<PlayerPowerController>();

        for (int i = 0; i < stackController.moneyStack.Length; i++)
        {
            if (playerPower.moneyAmount < stackCheckpoint)
            {
                stackCheckpoint = 100f;
                break;
            }

            stackController.moneyStack[i].SetActive(true);
            stackCheckpoint += 100f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Uncollected")) return;

        var target = collision.gameObject;
        target.SetActive(false);

        // effect
        Instantiate(dollarEffect, target.transform.position, Quaternion.identity);

        // indicator
        var indicatorObj = Instantiate(moneyIndicator, moneyIndicatorPos.transform.position, Quaternion.identity);
        var stackValueComp = target.GetComponent<MoneyStackValue>();
        indicatorObj.GetComponent<MoneyIndicatorValue>().impactValue.text = $"+{stackValueComp.moneyValue:0}";
        indicatorObj.transform.SetParent(playerBlock.transform);

        StartCoroutine(DisableIndicatorLater(indicatorObj));

        // money gain
        var playerPower = GetComponent<PlayerPowerController>();
        playerPower.moneyAmount += stackValueComp.moneyValue;

        // gem update
        GameManager.instance.gemWithStackMoney += stackValueComp.stackValue;

        // count stacks
        GetComponent<EndingCalculation>().stackCollected++;
    }

    private IEnumerator DisableIndicatorLater(GameObject indicator)
    {
        yield return new WaitForSeconds(0.36f);
        Destroy(indicator);
    }
}
