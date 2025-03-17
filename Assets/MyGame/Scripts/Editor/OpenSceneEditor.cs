using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class OpenSceneEditor : EditorWindow
{
    private static string SCENE_PATH = "Assets/MyGame/Scenes/{0}.unity";

    [MenuItem("Tools/Open Scene/LoadFirst", false, 1)]
    public static void OpenLoadFirst()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene
           (string.Format(SCENE_PATH, "LoadFirst"), OpenSceneMode.Single);
    }

    [MenuItem("Tools/Open Scene/Main", false, 1)]
    public static void OpenMain()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene
           (string.Format(SCENE_PATH, "Main"), OpenSceneMode.Single);
    }
}