using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public enum FxMovingAverageKind
    {
        Simple,
        Modified,
        Exponential,
        TripleSmoothedExponential,      // TRIX
    }
}
