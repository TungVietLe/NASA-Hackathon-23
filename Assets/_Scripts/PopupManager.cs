using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Popup = PopupSequence.Popup;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    public Canvas canvas;
    public GameObject TextPopup;
    public List<PopupSequence> PopupSequences;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //! DEBUGGING!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InitPopupSequence("Conclusion");
        }
    }

    public void ClearPopups()
    {
        // TODO: Not Implemented
    }

    private void DoublePopupHeight(ref GameObject popupObj)
    {
        // not very generic, but fine for now
        RectTransform rt = popupObj.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y * 1.5f);
    }

    // TODO: make this procedure much more generic - this is a great start, though
    /// <summary>
    /// Run the tutorial sequence of text boxes
    /// </summary>
    public void InitPopupSequence(string key, Action onEndOfSequence=null)
    {
        PopupSequence sequence = PopupSequences.FirstOrDefault(seq => seq.Key.Equals(key));

        if (sequence == null)
        {
            Debug.LogWarning($"No PopupSequence with key \"{key}\" found");
            return;
        }

        // init text across all popups in sequence
        List<(GameObject, PopupContent)> popupObjects = new();
        for (int i = 0; i < sequence.popups.Count; ++i)
        {
            Popup popup = sequence.popups[i];

            GameObject popupObj = Instantiate(TextPopup, canvas.transform.position, Quaternion.identity, canvas.transform);

            PopupContent content = popupObj.GetComponent<PopupContent>();
            content.InitText(popup.Title, popup.Body);

            // not best practice to have this hacky thing here, but otherwise harmless
            if (key.Equals("Conclusion") && i == sequence.popups.Count - 1)
            {
                DoublePopupHeight(ref popupObj);
            }

            if (i == 0)
            {
                // if first popup in sequence, set back button invisible
                content.BackButton.gameObject.SetActive(false);
            }
            else
            {
                popupObj.SetActive(false);
            }

            // set progress count at bottom of popup for this popup
            content.ProgressCount.text = $"{i + 1}/{sequence.popups.Count}";

            popupObjects.Add((popupObj, content));
        }

        //! invariant: popupObjects.Count == sequence.popups.Length

        // bind all continue button delegates to deactivate current popup and move on to next popup
        for (int i = 0; i < popupObjects.Count; ++i)
        {
            (GameObject obj, PopupContent content) = popupObjects[i];
            int iCopy = i; // c# lambda captures by reference, so we need to copy first

            content.BackButton.onClick.AddListener(() =>
            {
                if (iCopy == 0) return;

                // no need to check, since we already know iCopy != 0
                obj.SetActive(false);
                popupObjects[iCopy - 1].Item1.SetActive(true);
            });

            content.ContinueButton.onClick.AddListener(() =>
            {
                obj.SetActive(false);

                if (iCopy + 1 < popupObjects.Count)
                {
                    popupObjects[iCopy + 1].Item1.SetActive(true);
                }
                else
                {
                    foreach ((GameObject obj, PopupContent content) in popupObjects)
                    {
                        Destroy(obj);
                    }
                    if (onEndOfSequence != null)
                    {
                        onEndOfSequence();
                    }
                }
            });
        }
    }
}
