﻿using ESS.FW.Bpm.Engine.Delegate;
using ESS.FW.Bpm.Engine.History.Impl.Event;
using ESS.FW.Bpm.Engine.History.Impl.Producer;

namespace ESS.FW.Bpm.Engine.History.Impl.Transformer
{
    /// <summary>
    ///     
    /// </summary>
    public class CaseActivityInstanceUpdateListener : HistoryCaseExecutionListener
    {
        public CaseActivityInstanceUpdateListener(ICmmnHistoryEventProducer historyEventProducer,
            IHistoryLevel historyLevel) : base(historyEventProducer, historyLevel)
        {
        }

        protected internal override HistoryEvent CreateHistoryEvent(IDelegateCaseExecution caseExecution)
        {
            if (HistoryLevel.IsHistoryEventProduced(HistoryEventTypes.CaseActivityInstanceUpdate, caseExecution))
                return EventProducer.CreateCaseActivityInstanceUpdateEvt(caseExecution);
            return null;
        }
    }
}