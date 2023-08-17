using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string targetSceneName;

    public GameObject objectToAppear;
    public float delayInSeconds = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowObject", delayInSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadTargetScene();
        }
    }

    private void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }

    private void ShowObject()
    {
        if (objectToAppear != null)
        {
            objectToAppear.SetActive(true);
        }
    }
}
