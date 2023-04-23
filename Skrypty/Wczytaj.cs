using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadFile : MonoBehaviour
{
    public Text filePathText;
    public Button loadButton;
    public GameObject loadedObject;

    private string filePath;
    private GameObject loadedPrefab;

    void Start()
    {
        loadButton.onClick.AddListener(LoadOnClick);
    }

    void LoadOnClick()
    {
        filePath = UnityEditor.EditorUtility.OpenFilePanel("Open File", "", "");
        filePathText.text = filePath;

        if (File.Exists(filePath))
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            loadedPrefab = new GameObject("LoadedPrefab");
            loadedObject = new GameObject("LoadedObject");
            loadedObject.AddComponent<MeshFilter>();
            loadedObject.AddComponent<MeshRenderer>();
            loadedObject.AddComponent<MeshCollider>();
            loadedObject.transform.SetParent(loadedPrefab.transform);
            loadedObject.GetComponent<MeshFilter>().mesh = new Mesh();
            loadedObject.GetComponent<MeshFilter>().mesh.name = Path.GetFileNameWithoutExtension(filePath);
            loadedObject.GetComponent<MeshFilter>().mesh.LoadFromMemory(fileData);
            loadedObject.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
            loadedObject.GetComponent<MeshCollider>().sharedMesh = loadedObject.GetComponent<MeshFilter>().mesh;
        }
    }
}
