using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class SimpleAutoSave : EditorWindow
{
    public float saveTime = 300;
    public float nextSave = 0;
    [MenuItem("Example/Simple autoSave")]
    static void Init()
    {
        SimpleAutoSave window = (SimpleAutoSave)EditorWindow.GetWindowWithRect(typeof(SimpleAutoSave), new Rect(0, 0, 200, 50));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Save Each:", saveTime + " Secs");
        float timeToSave = nextSave - (float)EditorApplication.timeSinceStartup;
        EditorGUILayout.LabelField("Next Save:", timeToSave.ToString() + " Sec");
        Repaint();
        if (EditorApplication.timeSinceStartup > nextSave)
        {
            string[] path = EditorSceneManager.GetActiveScene().path.Split(char.Parse("/"));
            path[path.Length - 1] = "AutoSave_" + path[path.Length - 1];
            bool saveOK = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), string.Join("/", path));
            Debug.Log("Saved Scene " + (saveOK ? "OK" : "Error!"));
            nextSave = (float)EditorApplication.timeSinceStartup + saveTime;
        }
    }
}