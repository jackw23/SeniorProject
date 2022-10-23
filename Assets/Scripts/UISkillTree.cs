using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class  UISkillTree: MonoBehaviour
{
    public Button fireSkillLevelBtn;
    public Button fireSkillCDBtn;
    public Button fireUltimateLevel;
    public Button fireUltimateCD;

    public Button waterSkillLevel;
    public Button waterSkillCD;
    public Button waterUltimateLevel;
    public Button waterUltimateCD;

    public Button airSkillLevel;
    public Button airSkillCD;
    public Button airUltimateLevel;
    public Button airUltimateCD;

    public Button earthSkillLevel;
    public Button earthSkillCD;
    public Button earthUltimateLevel;
    public Button earthUltimateCD;
    
    public GameObject upgradePanel;
    public GameObject upgradeMessage;
    private Dictionary<Item, int> ItemAmounts = new Dictionary<Item, int>();
    UpgradeButton fireSkillLevel = new UpgradeButton("Fire", "Skill", "Level", 0);
    UpgradeButton fireSkillCD = new UpgradeButton("Fire", "Skill", "Cooldown", 10);


    private void Start()
    {
        //UpgradeButton fireSkillLevel = new UpgradeButton("Fire", "Skill", "Level", 0);

    }


    void OnEnable()
    {
        //Register Button Events
        fireSkillLevelBtn.onClick.AddListener(() => Upgrade(fireSkillLevelBtn));
        fireSkillCDBtn.onClick.AddListener(() => Upgrade(fireSkillCDBtn));
        //fireUltimateLevel.onClick.AddListener(() => buttonCallBack3());
        //fireUltimateCD.onClick.AddListener(() => buttonCallBack4());
    }

    private void Upgrade(Button buttonPressed) 
    {
        if (buttonPressed == fireSkillLevelBtn)
        {
            Debug.Log("Clicked: " + fireSkillLevelBtn.name);
            //TMP_Text text = "Debug idk";
            string tempMessage = "Would you like to upgrade " + fireSkillLevel.element + " " + fireSkillLevel.attackType + " to " +
                fireSkillLevel.bottomText + " " + (fireSkillLevel.number+1)  + "?";
            
            upgradeMessage.GetComponent<TMP_Text>().text = tempMessage;

            OpenUpgradePanel();
           
            //upgradeMessage.GetComponent<TMP_Text>().text = "asdfasdfasfasfasdf";


        }

        if (buttonPressed == fireSkillCDBtn)
        {
            Debug.Log("Clicked: " + fireSkillCDBtn.name);
            string tempMessage = "Would you like to reduce " + fireSkillCD.element + " " + fireSkillCD.attackType + " cooldown to " +
                + (fireSkillCD.number - 1) + "seconds ?";

            upgradeMessage.GetComponent<TMP_Text>().text = tempMessage;
        }

    }
    public void OpenUpgradePanel()
    {
        if (upgradePanel != null)
        {
            bool isActive = upgradePanel.activeSelf;
            upgradePanel.SetActive(!isActive);
        }
    }


}
