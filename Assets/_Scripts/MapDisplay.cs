using UnityEngine;

public class MapDisplay : Singleton<MapDisplay> {
    [SerializeField] private Renderer _textureRenderer;

    public void DrawTexture(Texture2D texture) {

        _textureRenderer.sharedMaterial.mainTexture = texture;
        _textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
}