using UnityEngine;
using System.Collections;

public interface IAnimationController {

    AbilityContext abilityContext { get; set; }
	void Execute ();
}
