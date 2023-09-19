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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeScene());
    }

    // Update is called once per frame
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(t);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
