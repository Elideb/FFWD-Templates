using System;
using UnityEngine;
using System.Collections.Generic;
using FluentAssertions;

public class CloningAGameObject : MonoBehaviour {

	bool? tested;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!tested.HasValue) {
			tested = true;
			
			GameObject go = new GameObject();
			CloningHelper help = go.AddComponent<CloningHelper>();
			help.val = 100;
			help.reference = gameObject;
			help.referenceList = new List<GameObject>();
			help.referenceList.Add(gameObject);
			help.objReference = new CloningHelper.CloneObject();
			help.objReferenceList = new List<CloningHelper.CloneObject>();
			help.objReferenceList.Add(help.objReference);
			
			CloningHelper helpClone = (CloningHelper)Instantiate(help);
			
			try {
				helpClone.val.Should().Be(100);
				helpClone.reference.Should().Be(gameObject);
				((object)helpClone.referenceList).Should().NotBe(help.referenceList);
				helpClone.referenceList.Should().NotBeEmpty();
				helpClone.referenceList.Should().Contain(gameObject);
				helpClone.objReference.Should().NotBe(help.objReference);
				helpClone.objReference.Should().BeNull();
				((object)helpClone.objReferenceList).Should().NotBe(help.objReferenceList);
				((object)helpClone.objReferenceList).Should().BeNull();
			} catch ( Exception ex ) {
				// Log the merror
				Debug.LogError(ex.Message);
			} finally {
				Destroy(go);
				Destroy(helpClone.gameObject);
				Debug.Log(GetType().Name + " succeeded");
			}
		}
	}
}
