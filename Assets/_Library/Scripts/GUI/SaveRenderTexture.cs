using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRenderTexture : MonoBehaviour
{
    [SerializeField]
    private RenderTexture m_Texture;

    [SerializeField]
    private string path;

    [SerializeField]
    private ScoreManager scoreManager;

    private void Start()
    {
    }

    public void CaptureScore()
    {
        Texture2D tex = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        RenderTexture.active = m_Texture;
        tex.ReadPixels(new Rect(0, 0, m_Texture.width, m_Texture.height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();

        System.IO.File.WriteAllBytes(Application.persistentDataPath + path, bytes);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            CaptureScore();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            scoreManager.AddScore("PILOT", Random.Range(1000, 5000));
        }
    }
}
