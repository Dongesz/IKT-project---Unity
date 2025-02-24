using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneReference
{
    public SceneAsset scene; // Unity-ben választható Scene
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<SceneReference> sceneOrder; // SceneAsset alapú lista

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int currentIndex = sceneOrder.FindIndex(s => s.scene.name == currentScene); // Keresés a listában

        if (currentIndex != -1 && currentIndex + 1 < sceneOrder.Count)
        {
            string nextSceneName = sceneOrder[currentIndex + 1].scene.name;
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("Nincs több elérhető pálya! Játék vége.");
        }
    }
}
