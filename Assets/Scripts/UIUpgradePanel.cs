using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class UIUpgradePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yesUpgrade;
    public Button noUpgrade;
    public static UpgradeButton currentUpgradeButton;
    public static GameObject currentUBText;

    void Start()
    {
        yesUpgrade.onClick.AddListener(() => UpdateUB());

    }
    public static void setUpgradeButton(UpgradeButton ub)
    {
        currentUpgradeButton = ub;
    }
    public static void setUBText(GameObject ubt)
    {
        currentUBText = ubt;
    }

    public static void UpdateUB()
    {
        if (currentUpgradeButton.bottomText == "Level")
        {
            currentUpgradeButton.addNumber();
        }else if (currentUpgradeButton.bottomText == "Cooldown")
        {
            currentUpgradeButton.subtractNumber();

        }
        currentUBText.GetComponent<TMP_Text>().text = currentUpgradeButton.number.ToString();


    }
    /*void OnEnable()
    {
        //upgradePanelObj.currentUpgradeButton
        //Register Button Events
        yesUpgrade.onClick.AddListener(() => currentUpgradeButton.upgradeNumber());
        //noUpgrade.onClick.AddListener(() => Upgrade(fireSkillCDBtn));
        //yesUpgrade.onClick.AddListener();
        //fireUltimateLevel.onClick.AddListener(() => buttonCallBack3());
        //fireUltimateCD.onClick.AddListener(() => buttonCallBack4());
    }*/
    // Update is called once per frame

    //calls the upgrade numebr in upgradeButton... 
    void Update()
    {
        
    }
}
