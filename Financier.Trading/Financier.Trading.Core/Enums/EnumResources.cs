//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System.Globalization;
using System.Resources;

namespace Financier.Trading
{
    public static class EnumResources
    {
        static ResourceManager _rm;

        static EnumResources()
        {
            _rm = new ResourceManager($"{nameof(Financier)}.{nameof(Financier.Trading)}.Resources", typeof(EnumResources).Assembly);
        }

        public static string ToDisplayString(this OrderEventType otet, CultureInfo ci = null)
        {
            return _rm.GetString($"{nameof(OrderEventType)}.{otet}");
        }

    }
}
