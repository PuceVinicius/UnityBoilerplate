using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boilerplate.Generic
{
    public abstract class DescriptionScriptableObject : ScriptableObject
    {
        [TextArea, SerializeField] private string description;
    }
}