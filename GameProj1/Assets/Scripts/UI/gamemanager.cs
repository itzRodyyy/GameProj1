using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    [Header("*** GameManager Instance ***")]
    public static gamemanager instance;
    [Header("*** Player Components ***")]
    public GameObject player;
    public playerController script;
    [Header("*** Active UI Components ***")]
    public Image playerHPBar;
    public Image playerSPBar;
    public GameObject menuActive;

    [Header("*** Menus ***")]
    public GameObject menuPause;

    float timeScaleOrig;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        player = GameObject.FindWithTag("Player");
        script = player.GetComponent<playerController>();
        timeScaleOrig = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && menuActive == null)
        {
            statePause();
            menuActive = menuPause;
            menuActive.SetActive(true);
        } 
        else if (Input.GetButtonDown("Cancel") && menuActive != null)
        {
            stateUnpause();
        }
    }

    public void updatePlayerUI()
    {
        playerHPBar.fillAmount = script.HP / script.MaxHP;
        playerSPBar.fillAmount = script.SP / script.MaxSP;
    }

    public void statePause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void stateUnpause()
    {
        menuActive.SetActive(false);
        menuActive = null;
        Time.timeScale = timeScaleOrig;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
