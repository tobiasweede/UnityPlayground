using UnityEngine;
using UnityEditor;

public class ProductSpawner : MonoBehaviour
{
    private GameObject instance;
    private string path;
    private Shader shader;
    void Awake()
    {
        shader = Shader.Find("Universal Render Pipeline/Product");
        path = Application.dataPath + "/Resources/waschmittel/ariel2k";
        var product = "ariel2k";

        // Prototype
        GameObject prototype = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Resources/waschmittel/{product}.fbx");

        // Instance
        instance = GameObject.Instantiate(prototype);
        instance.AddComponent<MeshCollider>();
        GameObject handle = new GameObject();
        handle.transform.SetParent(instance.transform);
        handle.transform.localPosition = new Vector3(-0.0022f, -0.0022f, -0.0022f);
        Vector3 targetPosition = GameObject.Find("Spawn").transform.position;
        instance.transform.position = targetPosition + (instance.transform.position - handle.transform.position);
        MeshRenderer rend = instance.GetComponent<MeshRenderer>();
        rend.material.shader = shader;

        // Texture
        // Make texture readable
        // https://stackoverflow.com/questions/25175864/making-a-texture2d-readable-in-unity-via-code
        string texturePath = $"Assets/Resources/waschmittel/{product}/{product}_FeatureMap.png";
        TextureImporter textureImporter = AssetImporter.GetAtPath(texturePath) as TextureImporter;
        textureImporter.isReadable = true;
        AssetDatabase.ImportAsset(texturePath);
        AssetDatabase.Refresh();
        // Import texture
        Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
        rend.material.SetTexture("_FeatureMap", tex);
    }
}
