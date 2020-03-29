//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financial.Extensions
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
