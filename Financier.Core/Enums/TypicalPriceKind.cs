//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier
{
    public enum TypicalPriceKind
    {
        /// <summary>
        /// Close price (default)
        /// </summary>
        Close,

        /// <summary>
        /// (High + Low + Close) / 3
        /// </summary>
        TypicalPrice,

        /// <summary>
        /// (Open + High + Low + Close) / 4.0
        /// </summary>
        OHLC,

        /// <summary>
        /// (High + Low + Close + Close) / 4.0
        /// </summary>
        HLCC,

        /// <summary>
        /// (High + Low + Open + Open) / 4.0
        /// </summary>
        HLOO,

        /// <summary>
        /// Volume Weighted Average Price. If source does not support, substitute by Typical price.
        /// </summary>
        VWAP,
    }
}
