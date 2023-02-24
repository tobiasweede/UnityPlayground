using UnityEngine;
using UnityEditor;

public class ProductSpawner : MonoBehaviour
{
    private GameObject instance;
    private string path;
    private Shader shader;
    void Start()
    {
        shader = Shader.Find("Universal Render Pipeline/Product");
        path = Application.dataPath + "/Resources/waschmittel/ariel2k";
        print(path);
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
        Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/Resources/waschmittel/{product}/{product}_FeatureMap.png");
        rend.material.SetTexture("_FeatureMap", tex);
    }
}
