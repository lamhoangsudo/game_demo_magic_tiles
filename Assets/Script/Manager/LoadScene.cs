using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadScene
{
    public static Enum.Scene targetScene;

    public static void Load(Enum.Scene scene)
    {
        targetScene = scene;
        SceneManager.LoadScene(Enum.Scene.LoadingScene.ToString());
    }

    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
