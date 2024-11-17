using UnityEngine;

public enum LevelName { Or, And, Not, NandWithAnd, Nor, NorWithNand }

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public LevelName currentLevel;

    private void Awake()
    {
        CreateSinglton();
    }

    void CreateSinglton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
