//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financial.Extensions
{
    public class TradingOrderFactoryModel : TradingOrderFactoryBase
    {
        public override ITradingSimpleOrder CreateMarketPriceOrder(TradeSide side, decimal size)
        {
            return new TradingOrderModel(TradingOrderType.MarketPrice, side, size);
        }

        public override ITradingSimpleOrder CreateLimitPriceOrder(TradeSide side, decimal price, decimal size)
        {
            return new TradingOrderModel(TradingOrderType.LimitPrice, side, price, size);
        }

        public override ITradingConditionalOrder CreateIFD(ITradingSimpleOrder first, ITradingSimpleOrder second)
        {
            return new TradingConditionalOrderModel(TradeConditionalOrderType.IFD, first, second);
        }

        public override ITradingConditionalOrder CreateIFDOCO(ITradingSimpleOrder ifdone, ITradingSimpleOrder ocoFirst, ITradingSimpleOrder ocoSecond)
        {
            var oco = new TradingConditionalOrderModel(TradeConditionalOrderType.OCO, ocoFirst, ocoSecond);
            return new TradingConditionalOrderModel(TradeConditionalOrderType.IFD, oco);
        }
    }
}
