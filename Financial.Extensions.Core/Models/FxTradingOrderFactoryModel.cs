//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/feedex/
//

namespace Financial.Extensions
{
    public class FxTradingOrderFactoryModel : FxTradingOrderFactoryBase
    {
        public override IFxTradingSimpleOrder CreateMarketPriceOrder(FxTradeSide side, decimal size)
        {
            return new FxTradingOrderModel(FxTradingOrderType.Market, side, size);
        }

        public override IFxTradingSimpleOrder CreateLimitPriceOrder(FxTradeSide side, decimal price, decimal size)
        {
            return new FxTradingOrderModel(FxTradingOrderType.Limit, side, price, size);
        }

        public override IFxTradingConditionalOrder CreateIFD(IFxTradingSimpleOrder first, IFxTradingSimpleOrder second)
        {
            return new FxTradingConditionalOrderModel(FxTradeConditionalOrderType.IFD, first, second);
        }

        public override IFxTradingConditionalOrder CreateIFDOCO(IFxTradingSimpleOrder ifdone, IFxTradingSimpleOrder ocoFirst, IFxTradingSimpleOrder ocoSecond)
        {
            var oco = new FxTradingConditionalOrderModel(FxTradeConditionalOrderType.OCO, ocoFirst, ocoSecond);
            return new FxTradingConditionalOrderModel(FxTradeConditionalOrderType.IFD, oco);
        }
    }
}
