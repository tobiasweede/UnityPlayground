using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D textureAsset;
    [HideInInspector] public Vector2 textureSize;
    [HideInInspector] public Texture2D texture;

    // Start is called before the first frame update
    void Start()
    {
        var r = GetComponent<Renderer>();
        if (texture == null)
        {
            textureSize = new Vector2(2048, 2048);
            texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        }
        else
        {
            // clone texture to not overwrite it
            texture = Instantiate(textureAsset);
            textureSize = new Vector2(texture.width, texture.height);
        }

        // apply texture to material
        r.material.mainTexture = texture;

    }

    // Update is called once per frame
    void Update()
    {

    }
}

