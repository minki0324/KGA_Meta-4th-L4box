using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
	[SerializeField] private Image captureImage;
	private Texture2D captureTexture; // Create Image

	public void CameraOpen() // Camera Open method
    {
		if (NativeCamera.IsCameraBusy()) return; // camera X 
		TakePicture();
	}

	private void TakePicture()
    {
		string _filePath = Application.persistentDataPath + "/Profile";
		if (!File.Exists(_filePath))
		{ // 해당 Directory 없을 시 생성
			Directory.CreateDirectory(_filePath);
		}

		// Camera
		NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
		{
			if (path != null)
			{
				// Create Image
				Texture2D texture = NativeCamera.LoadImageAtPath(path, 2048);
				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}

				// Camera load
				GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
				quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
				quad.transform.forward = Camera.main.transform.forward;
				quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                #region Material
                Material material = quad.GetComponent<Renderer>().material;
				if (!material.shader.isSupported) material.shader = Shader.Find("Legacy Shaders/Diffuse");
				material.mainTexture = texture;
				#endregion

				// capture texture 표시
				captureTexture = texture;
				Rect rect = new Rect(0, 0, captureTexture.width, captureTexture.height);
				captureImage.sprite = Sprite.Create(captureTexture, rect, new Vector2(0.5f, 0.5f));

				// capture texture save
				Texture2D readableTexture = GetReadableTexture(texture); // Texture 변환
				byte[] texturePNGByte = readableTexture.EncodeToPNG(); // texture to pngByte encode
				string fileName = $"{_filePath}/00_00.png";
				// File.WriteAllBytes($"{GameManager.instance.UID}_{GameManager.instance.Profile_Index}.png", snap.EncodeToPNG());
				File.WriteAllBytes(fileName, texturePNGByte); // file save

				Destroy(quad, 5f);
			}
		}, 2048, true, NativeCamera.PreferredCamera.Front);
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

		Graphics.Blit(source, renderTexture); // Texture에 맞게 Render Texture 복사
		RenderTexture.active = renderTexture;

		Texture2D readableTexture = new Texture2D(source.width, source.height, TextureFormat.RGB24, false);
		readableTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
		readableTexture.Apply();

		return readableTexture;
    }
}
