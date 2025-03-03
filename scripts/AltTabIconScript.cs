using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AltTabIconScript : MonoBehaviour
{ 
    private desktopManager manager;
    int whereIs = 0;
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<desktopManager>();
    }
    public void openTabFromShortcut(String toClose)
    {
        whereIs = -1;
        for (int i = 0; i < manager.openedTabs.Length; i++)
        {
            if (manager.openedTabs[i] == toClose)
            {
                whereIs = i;
                break;
            }
        }
        manager.currentTabs.transform.GetChild(whereIs).gameObject.SetActive(true);
        manager.tabFrontier(manager.currentTabs.transform.GetChild(whereIs).gameObject);
        manager.currentTabs.transform.GetChild(whereIs).GetComponent<TabbingScript>().tabbed = false;
    }
   
}
