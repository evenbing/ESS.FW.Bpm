﻿

using ESS.FW.Bpm.Model.Xml;
using ESS.FW.Bpm.Model.Xml.impl.instance;
using ESS.FW.Bpm.Model.Xml.type;

namespace ESS.FW.Bpm.Model.Bpmn.impl.instance
{
    public class OperationRef : BpmnModelElementInstanceImpl
    {

        public new static void RegisterType(ModelBuilder modelBuilder)
        {
            IModelElementTypeBuilder typeBuilder = modelBuilder.DefineType<OperationRef>(/*typeof(OperationRef),*/ BpmnModelConstants.BpmnElementOperationRef)
                .NamespaceUri(BpmnModelConstants.Bpmn20Ns)
                .InstanceProvider(new ModelTypeInstanceProviderAnonymousInnerClass());

            typeBuilder.Build();
        }

        private class ModelTypeInstanceProviderAnonymousInnerClass : IModelTypeInstanceProvider<OperationRef>
        {
            public virtual OperationRef NewInstance(ModelTypeInstanceContext instanceContext)
            {
                return new OperationRef(instanceContext);
            }
        }

        public OperationRef(ModelTypeInstanceContext instanceContext) : base(instanceContext)
        {
        }
    }
}