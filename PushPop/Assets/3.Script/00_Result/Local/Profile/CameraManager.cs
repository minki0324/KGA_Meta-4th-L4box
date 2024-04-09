using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ܺ� Camera ���� Class
/// </summary>
public class CameraManager : MonoBehaviour
{
    private Texture2D captureTexture; // Create Image

    public void CameraOpen(Image _captureImage) // Camera Open method
    {
        if (NativeCamera.IsCameraBusy()) return; // camera X 
        TakePicture(_captureImage);
    }

    private void TakePicture(Image _captureImage)
    {
        string _filePath = $"{Application.persistentDataPath}/Profile";
        if (!File.Exists(_filePath))
        { // �ش� Directory ���� �� ����
            Directory.CreateDirectory(_filePath);
        }

        // Camera
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            Debug.Log("Path: " + path);
            if (path != null)
            {
                // Create Image
                Texture2D texture = NativeCamera.LoadImageAtPath(path, 512);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // Camera load
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.AddComponent<MeshCollider>();
                quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                quad.transform.forward = Camera.main.transform.forward;
                quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                #region Material
                Material material = quad.GetComponent<Renderer>().material;
                if (!material.shader.isSupported) material.shader = Shader.Find("Legacy Shaders/Diffuse");
                material.mainTexture = texture;
                #endregion

                // capture texture ǥ��
                captureTexture = texture;
                Rect rect = new Rect(0, 0, captureTexture.width, captureTexture.height);
                _captureImage.sprite = Sprite.Create(captureTexture, rect, new Vector2(0.5f, 0.5f));

                // image name ������ profile index ����
                ProfileManager.Instance.AddProfile(false);

                // capture texture save
                Texture2D readableTexture = GetReadableTexture(texture); // Texture ��ȯ
                byte[] texturePNGByte = readableTexture.EncodeToPNG(); // texture to pngByte encode
                string fileName = $"{_filePath}/{ProfileManager.Instance.UID}_{ProfileManager.Instance.TempUserIndex}.png";
                try
                {
                    File.WriteAllBytes(fileName, texturePNGByte); // file save
                }
                catch (Exception e)
                {
                    Debug.LogError($"File save error: {e.Message}");
                }
                ProfileManager.Instance.ImageSet(false, null);
                Destroy(quad, 5f);
            }
        }, 512, true, NativeCamera.PreferredCamera.Front);
        Debug.Log("Enum: " + permission);
    }

    private Texture2D GetReadableTexture(Texture2D source)
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear
            );

        Graphics.Blit(source, renderTexture); // Texture�� �°� Render Texture ����
        RenderTexture.active = renderTexture;

        Texture2D readableTexture = new Texture2D(source.width, source.height, TextureFormat.RGB24, false);
        readableTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        readableTexture.Apply();

        return readableTexture;
    }
}
