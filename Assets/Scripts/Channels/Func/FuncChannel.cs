using System;
using Boilerplate.Generic;

namespace Boilerplate.FuncChannels
{
    public abstract class FuncChannel<R, A> : DescriptionScriptableObject
    {
        internal Func<A, R> FuncDelegate;
    }

    public abstract class FuncChannel<R> : DescriptionScriptableObject
    {
        internal Func<R> FuncDelegate;
    }
}