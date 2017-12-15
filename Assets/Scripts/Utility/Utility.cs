using UnityEngine;

public static class Utility {

    //          --- Math Tools ---

    // Percentages
    public static float Percent(float dividende, float diviseur) { return (dividende / diviseur); }
    public static float Percent(float dividende, float diviseur, float scale) { return ((dividende / diviseur) * scale); }
    public static float InvertPercent(float dividende, float diviseur) { return (1 - (dividende / diviseur)); }
    public static float InvertPercent(float dividende, float diviseur, float scale) { return ((1 - (dividende / diviseur)) * scale); }

    // Int caps
    public static int Cap(ref int value, int min, int max) { value = ((value < min) ? min : ((value > max) ? max : value)); return value; }
    public static int CapMax(ref int value, int max) { value = ((value > max) ? max : value); return value; }
    public static int CapMin(ref int value, int min) { value = ((value < min) ? min : value); return value; }

    // Float caps
    public static float Cap(ref float value, float min, float max) { value = ((value < min) ? min : ((value > max) ? max : value)); return value; }
    public static float CapMax(ref float value, float max) { value = ((value > max) ? max : value); return value; }
    public static float CapMin(ref float value, float min) { value = ((value < min) ? min : value); return value; }

    // Range checkers
    public static bool IsInRange(float value, float min, float max) { return (value >= min && value <= max); }

    //          --- GameObject extensions ---

    /// <summary>
    /// Get the T component of the gameobject. If the component doesn't exist, the component is created in the process.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static T SecureGetComponent<T>(this GameObject gameObject)
    {
        if (!gameObject.GetComponent(typeof(T)))
            gameObject.AddComponent(typeof(T));
        return gameObject.GetComponent<T>();
    }

    //          --- Vector2 extensions ---

    public static void SetX(this Vector2 vector, float x_value) { vector = new Vector2(x_value, vector.y); }
    public static void SetY(this Vector2 vector, float y_value) { vector = new Vector2(vector.x, y_value); }

    //          --- Vector3 extensions ---

    public static void SetX(this Vector3 vector, float x_value) { vector = new Vector3(x_value, vector.y, vector.z); }
    public static void SetY(this Vector3 vector, float y_value) { vector = new Vector3(vector.x, y_value, vector.z); }
    public static void SetZ(this Vector3 vector, float z_value) { vector = new Vector3(vector.x, vector.y, z_value); }

    //         --- Color extensions ---

    public static void SetR(this Color color, float r_value) { color = new Color(r_value, color.g, color.b, color.a); }
    public static void SetG(this Color color, float g_value) { color = new Color(color.r, g_value, color.b, color.a); }
    public static void SetB(this Color color, float b_value) { color = new Color(color.r, color.g, b_value, color.a); }
    public static void SetA(this Color color, float a_value) { color = new Color(color.r, color.g, color.b, a_value); }
}
