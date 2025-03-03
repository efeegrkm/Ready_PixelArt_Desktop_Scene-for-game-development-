using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuScripts : MonoBehaviour
{
    [SerializeField] private Button StartBut;
    private GameObject Menu;
    public void menuOpening(GameObject menu)
    {
        menu.SetActive(true);
    }
    public void menuClosing(GameObject menu)
    {
        Menu = menu;
        StartBut.interactable = false;
        menu.GetComponent<Animator>().SetTrigger("Closed");
        Invoke("invik", 0.15f);
    }
    private void invik()
    {
        StartBut.interactable = true;
        Menu.SetActive(false);
    }
}
