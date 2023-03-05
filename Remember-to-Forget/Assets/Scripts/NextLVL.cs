using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLVL : MonoBehaviour
{
    public string loadName;
    public string unloadName;

    private void OnTriggerEnter(Collider other)
    {
        if (loadName != "")
            DynamicLoadingScript.Instance.Load(loadName);

        if (unloadName != "")
            StartCoroutine("UnloadScene");
    }

    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(.10f);
        DynamicLoadingScript.Instance.Unload(unloadName);
    }
}
