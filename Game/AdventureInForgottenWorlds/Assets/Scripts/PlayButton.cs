using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string scenename;
    [SerializeField] private Button button;
    void Main()
    {
        SceneManager.LoadScene(scenename);
    }

    // Update is called once per frame
    void Start() {
        button.onClick.AddListener(Main);
    }
}
