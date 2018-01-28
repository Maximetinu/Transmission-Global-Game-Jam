using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Narrative Set", menuName = "New Narrative Set")]
public class NarrativeSet : ScriptableObject {
	[TextArea]
	public string[] set;
}
