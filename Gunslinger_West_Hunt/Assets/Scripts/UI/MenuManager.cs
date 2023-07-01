using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]List<MenuGroup> menuGroups = new List<MenuGroup>();
    private GameObject activeMenu = null;
    public SoundManager soundManager;
    public static MenuManager Instance { get; private set; }
    private void Start()
    {
        //foreach (var group in menuGroups)
        //{
        //    group.GetCurrentGroup().SetActive(false);
        //}
        EnterMenu("Main Group");
        if(soundManager != null)
        {
            soundManager.PlaySound("main_menu_theme");
        }
        Instance = this;
    }

    public void EnterMenu(string menuName)
    {
        if(activeMenu != null)
        {
            activeMenu.GetComponent<MenuGroup>().ExitAnimation();
        }
        foreach (var menuGroup in menuGroups)
        {
            if(menuGroup.GetCurrentGroup().name == menuName)
            {
                activeMenu = menuGroup.GetCurrentGroup();
                LoadEnterAnimation(menuGroup);
            }
        }
    }

    public void EnterMenu(GameObject newMenuGroup)
    {
        LoadExitAnimation(activeMenu.GetComponent<MenuGroup>());
        LoadEnterAnimation(newMenuGroup.GetComponent<MenuGroup>());
        activeMenu = newMenuGroup;
    }


    public void LoadEnterAnimation(MenuGroup menuGroup)
    {
        menuGroup.EnterAnimation();
    }

    public void LoadExitAnimation(MenuGroup menuGroup)
    {
        menuGroup.ExitAnimation();
    }



    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
