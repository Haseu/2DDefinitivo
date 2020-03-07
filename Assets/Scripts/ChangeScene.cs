using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string destinationScene;
    private Fade fade;

    private void Start() {
        fade = FindObjectOfType(typeof(Fade)) as Fade;   
    }

    public void interact()
    {
        StartCoroutine(this.triggerFadeAction());
    }

    private IEnumerator triggerFadeAction()
    {
        fade.fadeIn();
        yield return new WaitWhile(() => fade.darkImage.color.a < 0.95f);
        SceneManager.LoadScene(destinationScene);
    }
}
