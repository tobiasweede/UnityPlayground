using System;
using UnityEngine;
using UnityEngine.Events;

public class RaycastTest : MonoBehaviour
{
    public event Action<Color> OnFeatureMapColor;
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 20f))
        {
            // Debug.Log($"Hit {hit.collider.transform.name}");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            Vector2 pixelUV = hit.textureCoord;

            // Just in case, also make sure the collider also has a renderer
            // material and texture.
            Renderer rend = hit.transform.GetComponent<Renderer>();

            // Do not ignore primitive colliders
            Collider collider = hit.collider;

            // Null guard
            if (rend == null || rend.sharedMaterial == null ||
                rend.sharedMaterial.shader.name != "Universal Render Pipeline/Product" || collider == null)
                return;

            // Instantiate object
            Texture2D tex = rend.material.GetTexture("_FeatureMap") as Texture2D;
            Texture2D duplicate = duplicateTexture(tex);
            pixelUV.x *= duplicate.width;
            pixelUV.y *= duplicate.height;
            Color color = duplicate.GetPixel((int)pixelUV.x, (int)pixelUV.y);

            // Emit
            OnFeatureMapColor?.Invoke(color);

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
        }
    }
    Texture2D duplicateTexture(Texture2D source)
    {
        // https://stackoverflow.com/questions/44733841/how-to-make-texture2d-readable-via-script
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}