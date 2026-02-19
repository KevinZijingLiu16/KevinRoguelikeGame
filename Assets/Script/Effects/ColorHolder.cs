using Unity.VisualScripting;
using UnityEngine;

public class ColorHolder : MonoBehaviour
{
    [SerializeField] private PaletteSO palette;
   public static ColorHolder instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   public static Color GetColor(int level)
    {
        level = Mathf.Clamp(level, 0, instance.palette.levelColors.Length );
        return instance.palette.levelColors[level];
    }
    public static Color GetOutlineColor(int level)
    {
        level = Mathf.Clamp(level, 0, instance.palette.levelOutlineColors.Length );
        return instance.palette.levelOutlineColors[level];
    }
}
