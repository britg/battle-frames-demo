using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.Linq;

public class Proc : JSONResource {
    
    public enum Target {
        Inherit, // inherit from ability or special target
        Caster,
        EnemySide,
        FriendlySide
    }
    
    public const string RestoreHealth = "restoreHealth";
    public const string ResetAggro = "resetAggro";
    public const string ChangeAggro = "changeAggro";
    
    public static Dictionary<string, IProcResolver> ResolverMapping
        = new Dictionary<string, IProcResolver>() {
            { RestoreHealth, BaseProcResolver.Instance },
            { ResetAggro, new ResetAggroProcResolver() },
            { ChangeAggro, new ChangeAggroProcResolver() }
        };
    
    public static IProcResolver Resolver (string procKey) {
        Debug.Log("Finding resolver for proc " + procKey);
        if (ResolverMapping.ContainsKey(procKey)) {
            return ResolverMapping[procKey];
        }
        
        Debug.Log("Could not find resolver, using base");
        return BaseProcResolver.Instance;
    }

	public Proc (string _key) : base(_key) {}
	public Proc (string _key, JSONNode __sourceNode) : base(_key, __sourceNode) {
		LoadBaseStatChanges();
	}
	
    public Target target;
	public int occurances;
	public float occuranceRate;
    public float amount;
	
	public List<StatChange> baseStatChanges;
	
	void LoadBaseStatChanges () {
		baseStatChanges = new List<StatChange>();
		var baseStatChangesNode = sourceNode["baseStatChanges"];
		if (baseStatChangesNode == null) {
			return;
		}
		
		foreach (KeyValuePair<string, JSONNode> kv in baseStatChangesNode.AsObject) {
			var stat = new StatChange(kv.Key, kv.Value);
			baseStatChanges.Add(stat);
		}
	}
	
	public StatChange GenerateStatChange (string statKey) {
		var baseStatChange = baseStatChanges.Where(s => (s.key == statKey)).FirstOrDefault();
		return new StatChange(statKey, baseStatChange.sourceNode);
	}
	
}
