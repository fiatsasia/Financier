using System;
using System.Collections.Generic;
using System.Text;

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

        public override IFxTradingParentOrder CreateIFD(IFxTradingSimpleOrder first, IFxTradingSimpleOrder second)
        {
            return new FxTradingParentOrderModel(FxTradeParentOrderType.IFD, first, second);
        }

        public override IFxTradingParentOrder CreateIFDOCO(IFxTradingSimpleOrder ifdone, IFxTradingSimpleOrder ocoFirst, IFxTradingSimpleOrder ocoSecond)
        {
            var oco = new FxTradingParentOrderModel(FxTradeParentOrderType.OCO, ocoFirst, ocoSecond);
            return new FxTradingParentOrderModel(FxTradeParentOrderType.IFD, oco);
        }
    }
}
