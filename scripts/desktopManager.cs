using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class desktopManager : MonoBehaviour
{
    [SerializeField] private GameObject xpGiriþ;
    [SerializeField] private GameObject boot;
    public GameObject currentTabs;
    public GameObject myFilesIcon;
    public GameObject messagesIcon;
    public GameObject InvestorIcon;
    public GameObject LeagueOfLittlesIcon;
    public Transform[] logCases = new Transform[12];
    public Transform tabsParent;
    public string[] openedTabs = new string[10];
    public int index = 0;

    public GameObject altTabAnim;

    public GameObject[] inCasei = new GameObject[10];
    void Start()
    {
        if (xpGiriþ.activeSelf)
            Invoke("xpGiriþKapatma", 8);
    }
    void xpGiriþKapatma()
    {
        xpGiriþ.SetActive(false);
        boot.SetActive(true);
        Invoke("bootkapama", 11);
    }
    void bootkapama()
    {
        boot.SetActive(false);
    }
    public void openFile(GameObject currentObject)
    {
        int whereIs = -1;
        for (int i = 0; i < openedTabs.Length; i++)
        {
            if (openedTabs[i] == currentObject.name)
                whereIs = i;
        }
        if (whereIs == -1)
        {
            GameObject Instantiated = Instantiate(currentObject);
            Instantiated.transform.position = RandomVec3ForFilePos();
            Instantiated.transform.SetParent(currentTabs.transform, false);
            openedTabs[index] = currentObject.name;
            index++;
        }
        else
        {
            currentTabs.transform.GetChild(whereIs).gameObject.SetActive(true);
            tabFrontier(currentTabs.transform.GetChild(whereIs).gameObject);
            currentTabs.transform.GetChild(whereIs).GetComponent<TabbingScript>().tabbed = false;
        }
    }
    public void createAltTab(string tabType, int tabCount)
    {
        GameObject newObj = null;
        switch (tabType)
        {
            case "fileTab":
                if (!doesContain("fileTab"))
                    newObj = Instantiate(myFilesIcon, new Vector3(500, 0, 0), Quaternion.identity);
                break;
            case "investorTab":
                if (!doesContain("investorTab"))
                    newObj = Instantiate(InvestorIcon, new Vector3(500, 0, 0), Quaternion.identity);
                break;
            case "lolTab":
                if (!doesContain("lolTab"))
                    newObj = Instantiate(LeagueOfLittlesIcon, new Vector3(500, 0, 0), Quaternion.identity);
                break;
            case "messagesTab":
                if (!doesContain("messagesTab"))
                    newObj = Instantiate(messagesIcon, new Vector3(500, 0, 0), Quaternion.identity);
                break;
        }

        if (newObj != null)
        {
            newObj.transform.SetParent(tabsParent, false);
            newObj.transform.position = logCases[tabCount - 1].position;
            inCasei[tabCount - 1] = newObj;
        }
    }
    private bool doesContain(String tag)
    {
        bool output = false;
        for (int i = 0; i < tabsParent.childCount; i++)
        {
            if (tabsParent.GetChild(i).gameObject.tag == tag)
                output = true;
        }
        return output;
    }
    private Vector3 RandomVec3ForFilePos()
    {
        Vector3 Vect = new Vector3(UnityEngine.Random.Range(-150, 90), UnityEngine.Random.Range(0, 180), 0);
        return Vect;
    }
    public void tabFrontier(GameObject tab)
    {
        if (itsfirstOpening(tab))
            return;
        int tabsLastIndex = notNullCount(openedTabs) - 1;
        for (int i = 0; i < openedTabs.Length; i++)
        {
            if (openedTabs[i] + "(Clone)" == tab.name || openedTabs[i] == tab.name || tab.name + "(Clone)" == openedTabs[i])
            {
                if (i != tabsLastIndex)
                {
                    String temp = openedTabs[i];
                    for (int j = i; j < openedTabs.Length - 1; j++)
                    {
                        openedTabs[j] = openedTabs[j + 1];
                    }
                    openedTabs[tabsLastIndex] = temp;
                    break;
                }
            }
        }
        for (int i = 0; i < inCasei.Length; i++)
        {
            if (inCasei[i].CompareTag(tab.tag))
            {
                GameObject temp = inCasei[i];
                for (int j = i; j < inCasei.Length - 1; j++)
                {
                    inCasei[j] = inCasei[j + 1];
                }
                inCasei[tabsParent.childCount - 1] = temp;
                break;
            }
        }
        tab.transform.SetAsLastSibling();
    }
    private bool itsfirstOpening(GameObject obj)
    {
        for (int i = 0; i < tabsParent.transform.childCount; i++)
        {
            if (tabsParent.transform.GetChild(i).gameObject.CompareTag(obj.tag))
            {
                return false;
            }
        }
        return true;
    }
    private int notNullCount(String[] ar)
    {
        int cnt = 0;
        for (int i = 0; i < ar.Length; i++)
        {
            if (ar[i] != "")
            {
                cnt++;
            }
        }
        return cnt;
    }
}