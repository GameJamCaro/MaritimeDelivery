using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject startPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(WaitASecond());
       
    }

    IEnumerator WaitASecond()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        yield return new WaitForSeconds(1);
        startPanel.SetActive(false);

    }
    

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
