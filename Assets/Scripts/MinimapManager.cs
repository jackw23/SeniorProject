using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinimapManager : MonoBehaviour
{
    static MinimapManager instance;
    [SerializeField] public List<GameObject> areas;
    [SerializeField] private List<int> visitedAreas;

    private void Awake()
    {
        //Create singleton so progress will be preserved across scenes
        SetUpSingleton();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //get current scene number
        int sceneNum = scene.buildIndex;
        //add to visitedAreas if not present
        if (!visitedAreas.Contains(sceneNum))
        {
            visitedAreas.Add(sceneNum);
        }

        //re-attach areas
        areas[0] = GameObject.Find("Graveyard");
        areas[1] = GameObject.Find("Well");
        areas[2] = GameObject.Find("Underground (1)");

        UnHideVisitedScenes();
    }

    private void SetUpSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void UnHideVisitedScenes()
    {
        for (int i = 0; i < areas.Count; i++)
        {
            if (visitedAreas.Contains(i+1))
            {
                areas[i].SetActive(false);
            }
        }
    }
}
