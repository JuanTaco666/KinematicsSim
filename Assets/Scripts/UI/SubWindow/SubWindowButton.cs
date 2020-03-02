using UnityEngine.UI;

public class SubWindowButton : SubWindowComponent
{
    public Button button;

    void SetText(string text)
    {
        button.GetComponent<Text>().text = text;
    }

    public void AddListener(UnityEngine.Events.UnityAction call)
    {
        button.onClick.AddListener(call);
    }

}
