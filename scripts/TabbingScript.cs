
using UnityEngine;

public class TabbingScript : MonoBehaviour
{
    public bool tabbed = false;
    private desktopManager manager;
    public Animator animator;

    
    private int destroyedIndex = -1;
    private Transform iconsParent;
    GameObject[] inCaseOf;
    private void Start()
    {
        manager = GameObject.FindWithTag("GameController").GetComponent<desktopManager>();
        iconsParent = GameObject.FindWithTag("iconsParent").transform;
    }
    
    public void closeFile()
    {
        for(int i = 0; i<manager.openedTabs.Length; i++)
        {
            
            if (manager.openedTabs[i] + "(Clone)" == this.gameObject.name || manager.openedTabs[i] == this.gameObject.name || this.gameObject.name + "(Clone)" == manager.openedTabs[i])
            {
                for(int j = i; j< manager.openedTabs.Length-1; j++)
                {
                    manager.openedTabs[j] = manager.openedTabs[j + 1];
                }
                break;
            }
        }
        for(int i = 0; i<manager.tabsParent.childCount; i++)
        {
            if (manager.tabsParent.GetChild(i).gameObject.tag == this.gameObject.tag)
            {
                destroyedIndex = i;
                manager.tabsParent.GetChild(i).gameObject.GetComponent<Animator>().SetTrigger("close");
                Invoke("inv",0.1f);
                break;
            }

        }
        manager.index--;
        this.gameObject.SetActive(false);
    }
     void inv()
    {
        Destroy(iconsParent.GetChild(destroyedIndex).gameObject);
        recursiveArrangement();
        Destroy(this.gameObject);
    }
    private void recursiveArrangement()
    {
        iconsParent = GameObject.FindWithTag("iconsParent").transform;
        Debug.Log(destroyedIndex);
        Debug.Log(iconsParent.childCount);
        if (destroyedIndex < iconsParent.childCount)
        {
            Debug.Log(iconsParent.GetChild(destroyedIndex).gameObject.tag);
            iconsParent.GetChild(destroyedIndex).gameObject.transform.position = manager.logCases[destroyedIndex].position;
            destroyedIndex++;
            recursiveArrangement();
        }
    }
    
    public void bugSolverAlgorithm()
    {
        for(int i = 0; i<inCaseOf.Length-2; i++)
        {
            recursiveArrangement();
        }
        if (!isBug())
            Debug.Log("A bug founded and solved.--> tabbingScript-89");
        else
            Debug.Log("Bug founded but couldn't solved.--> tabbingScript-89");
    }
    private bool isBug()
    {
        bool a = false;
        for(int i = 0; i < manager.inCasei.Length; i++)
        {
            if (manager.inCasei[i] == null)
            {
                a = true;
            }
            else if(a == true)
            {
                return true;
            }
        }
        return false;
    }
    public void AltTabFile()
    {
        this.gameObject.SetActive(false);
        manager.altTabAnim.SetActive(true);
        Invoke("inv2", 0.2f);
        //tabda sýralý gösterme iþlemi.
        tabbed = true;
        int tabbedCount = 0;
        for(int i = 0; i< this.gameObject.transform.parent.childCount; i++)
        {
            if (this.gameObject.transform.parent.GetChild(i).GetComponent<TabbingScript>().tabbed) tabbedCount++;
        }
        manager.createAltTab(this.gameObject.tag, tabbedCount);
        //Invoke("inv3", 0.3f);
    }
    void inv3()
    {
        if(isBug())
            bugSolverAlgorithm();
    }
    void inv2()
    {
        manager.altTabAnim.SetActive(false);
    }
}
