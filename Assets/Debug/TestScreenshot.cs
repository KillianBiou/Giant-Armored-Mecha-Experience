using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScreenshot : MonoBehaviour
{
    [SerializeField]
    private CanvasScreenshot cs;

    private byte[] image;

    private void Start()
    {
        CanvasScreenshot.OnPictureTaken += NewImage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            cs.takeScreenShot(GetComponent<Canvas>(), SCREENSHOT_TYPE.IMAGE_AND_TEXT, false);
        }
    }

    public void NewImage(byte[] image)
    {
        string path = Application.persistentDataPath + "/CanvasScreenShot.png";
        System.IO.File.WriteAllBytes(path, image);
        Debug.Log(path);
        GetComponent<Canvas>().enabled = false;
    }
}
