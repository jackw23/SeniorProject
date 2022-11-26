using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject map;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        map.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            InventoryManager.Instance.ListItem();

            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            if (!pauseMenu.activeSelf)
            {
                InventoryManager.Instance.cleanInventory();
            }

        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            map.SetActive(!map.activeInHierarchy);
        }
    }
}
