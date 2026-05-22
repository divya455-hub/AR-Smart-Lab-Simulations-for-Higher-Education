#define CC_TOOLS
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Rendering;

namespace Convai.Scripts.Editor.CustomPackage
{
    [InitializeOnLoad]
    public static class CCToolsImporter
    {
        private const string CC_TOOLS_SYMBOL = "CC_TOOLS";

        static CCToolsImporter()
        {
            AddCCToolsDefineSymbol();
            Debug.Log("✅ CC Tools marked as installed (manual mode).");
        }

        private static void AddCCToolsDefineSymbol()
        {
            foreach (BuildTarget target in Enum.GetValues(typeof(BuildTarget)))
            {
                BuildTargetGroup group = BuildPipeline.GetBuildTargetGroup(target);

                if (group == BuildTargetGroup.Unknown)
                    continue;

                NamedBuildTarget namedTarget = NamedBuildTarget.FromBuildTargetGroup(group);
                List<string> symbols = PlayerSettings.GetScriptingDefineSymbols(namedTarget)
                    .Split(';')
                    .Select(d => d.Trim())
                    .ToList();

                if (!symbols.Contains(CC_TOOLS_SYMBOL))
                    symbols.Add(CC_TOOLS_SYMBOL);

                PlayerSettings.SetScriptingDefineSymbols(namedTarget, string.Join(";", symbols.ToArray()));
            }
        }
    }
}
