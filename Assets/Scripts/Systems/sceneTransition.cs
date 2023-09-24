using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    [SerializeField]
    float t;

    public bool onStart = true;
    // Start is called before the first frame update
    void Start()
    {
        if (onStart)
            StartCoroutine(changeSceneC(t));
    }

    // Update is called once per frame
    IEnumerator changeSceneC(float t_)
    {
        yield return new WaitForSeconds(t_);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void changeScene(float t_)
    {
        StartCoroutine(changeSceneC(t_));
    }
}
