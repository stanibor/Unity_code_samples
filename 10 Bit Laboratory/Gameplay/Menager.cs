using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menager : MonoBehaviour
{
    GameObject[] Bits;
    GameObject Player;
    EnemyHealth CurrentBossHealth;

    int biton;
    int lastbiton;
    int WinProgress;

    int LastLevel = 10;

    bool Boss = false;
    bool Won = false;

    public GameObject RetryPanel;
    public GameObject WinPanel;

    public Slider BossHealth;
    public Text Shower;

    bool GameInProgress = true;

	// Use this for initialization
	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Bits = GameObject.FindGameObjectsWithTag("Bit");
        //Debug.Log(Bits.Length);
    }
	
	// Update is called once per frame
	void Update ()
    {
        lastbiton = Bits.Length;
        Bits = GameObject.FindGameObjectsWithTag("Bit");
        biton = Bits.Length;
        //Debug.Log("Bits: "+biton+" "+lastbiton);
        if(lastbiton>biton)
        {
            if (Master().GetComponent<EnemyHealth>() != null)
                CurrentBossHealth = Master().GetComponent<EnemyHealth>();

            Debug.Log(CurrentBossHealth.currentHealth);
            //WinProgress = Progress();
            Boss = BossEnter();
        }

        if (CurrentBossHealth != null)
        {
            BossHealth.value = CurrentBossHealth.currentHealth / CurrentBossHealth.startingHealth;
            Shower.text = (int)(CurrentBossHealth.currentHealth / 1000f) + "";
        }
        else
        {
            BossHealth.value = 0f;
            Shower.text = "Indectructable";
        }
            


        if(Boss && CurrentBossHealth.currentHealth <= 0f)
        {
            Won = true;
            GameInProgress = false;
            Debug.Log("You won");
            
        }


        if (Player.GetComponent<PlayerHealth>().isDead_t)
            GameInProgress = false;

        if (!GameInProgress)
            if (Won)
                WinPanel.SetActive(true);
            else
                RetryPanel.SetActive(true);
    }

    public void Retry()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Exit()
    {
        Application.LoadLevel(0);
    }

    public void Continue()
    {
        WinPanel.SetActive(false);
    }

    GameObject Master()
    {
        GameObject Candidate = Bits[0];
        Merger Max = null;
        if (Candidate.GetComponent<Merger>() != null)
            Max = Candidate.GetComponent<Merger>();
        for (int i = 0; i < Bits.Length; i++)
        {
            if (Bits[i].GetComponent<Merger>() != null)
                if (Bits[i].GetComponent<Merger>().level > Max.level)
                {
                    Max = Bits[i].GetComponent<Merger>();
                    Candidate = Bits[i];
                }
        }

        return Candidate;
    }

    int Progress()
    {
        int tempus = 0;
        for (int i = 0; i < Bits.Length; i++)
        {
            if (Bits[i].GetComponent<Merger>() != null)
                tempus += (int)Mathf.Pow(2, Bits[i].GetComponent<Merger>().level);
        }
        return tempus;
    }

    bool BossEnter()
    {
        for (int i = 0; i < Bits.Length; i++)
        {
            if (Bits[i].GetComponent<Merger>() != null)
                return (Bits[i].GetComponent<Merger>().level == LastLevel); 
        }
        return false;
    }

    
}
