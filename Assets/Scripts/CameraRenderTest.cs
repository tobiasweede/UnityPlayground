using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRenderTest : MonoBehaviour
{
    [SerializeField]
    RenderTexture t;  // drag the render texture onto this field in the Inspector
    [SerializeField]
    Camera c; // drag the security camera onto this field in the inspector
    int resWidth;
    int resHeight;
    RenderTexture rt;
    Texture2D screenshot;
    // Start is called before the first frame update
    void Start()
    {
        c = GameObject.Find("Camera").GetComponent<Camera>();
        resWidth = Screen.width;
        resHeight = Screen.height;
        rt = new RenderTexture(resWidth, resHeight, 24);
        c.targetTexture = rt; //Create new renderTexture and assign to camera
    }

    void LateUpdate()
    {
            StartCoroutine(SaveCameraView());
    }

    public IEnumerator SaveCameraView()
    {
        yield return new WaitForEndOfFrame();
        // get the camera's render texture
        RenderTexture rendText = RenderTexture.active;
        RenderTexture.active = c.targetTexture;
        // render the texture
        c.Render();
        // create a new Texture2D with the camera's texture, using its height and width
        Texture2D cameraImage = new Texture2D(c.targetTexture.width, c.targetTexture.height, TextureFormat.RGB24, false);
        cameraImage.ReadPixels(new Rect(0, 0, c.targetTexture.width, c.targetTexture.height), 0, 0);
        cameraImage.Apply();
        RenderTexture.active = rendText;
        // store the texture into a .PNG file
        // byte[] bytes = cameraImage.EncodeToPNG();
        // save the encoded image to a file
        // System.IO.File.WriteAllBytes(Application.persistentDataPath + "/camera_image.png", bytes);
    }
}
