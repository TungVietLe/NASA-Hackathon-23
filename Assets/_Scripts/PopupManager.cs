using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    public GameObject TextPopup;
    public List<PopupSequence> PopupSequences;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Tutorial();
        }
    }

    /// <summary>
    /// Run the tutorial sequence of text boxes
    /// </summary>
    public void Tutorial()
    {
        PopupSequence sequence = PopupSequences.FirstOrDefault(seq => seq.Key.Equals("Tutorial"));

        if (sequence == null)
        {
            Debug.LogWarning("No PopupSequence with name \"Tutorial\" found");
            return;
        }

        // init text across all popups in sequence
        foreach (PopupSequence.Popup popup in sequence.popups)
        {
            GameObject currentPopup = Instantiate(TextPopup, Vector3.zero, Quaternion.identity);

            currentPopup.GetComponent<PopupRunner>().InitText(popup.Title, popup.Body);

            currentPopup.SetActive(false);
        }

        // bind all continue button delegates to deactivate current popup and move on to next
        // TODO
    }
}
