﻿using System;
using ESS.FW.Bpm.Model.Bpmn.instance.camunda;
using ESS.FW.Bpm.Model.Xml;
using ESS.FW.Bpm.Model.Xml.impl.instance;
using ESS.FW.Bpm.Model.Xml.type;
using ESS.FW.Bpm.Model.Xml.type.attribute;

namespace ESS.FW.Bpm.Model.Bpmn.impl.instance.camunda
{
    public class CamundaEntryImpl : CamundaGenericValueElementImpl, ICamundaEntry
	{

	  protected internal static IAttribute/*<string>*/ CamundaKeyAttribute;

	  public new static void RegisterType(ModelBuilder modelBuilder)
	  {
		IModelElementTypeBuilder typeBuilder = modelBuilder.DefineType<ICamundaEntry>(/*typeof(ICamundaEntry),*/ BpmnModelConstants.CamundaElementEntry)
                .NamespaceUri(BpmnModelConstants.CamundaNs)
                .InstanceProvider(new ModelTypeInstanceProviderAnonymousInnerClass());

		CamundaKeyAttribute = typeBuilder.StringAttribute(BpmnModelConstants.CamundaAttributeKey).Namespace(BpmnModelConstants.CamundaNs).Required().Build();

		typeBuilder.Build();
	  }

	  private class ModelTypeInstanceProviderAnonymousInnerClass : IModelTypeInstanceProvider<ICamundaEntry>
	  {
		  public virtual ICamundaEntry NewInstance(ModelTypeInstanceContext instanceContext)
		  {
			return new CamundaEntryImpl(instanceContext);
		  }
	  }

	  public CamundaEntryImpl(ModelTypeInstanceContext instanceContext) : base(instanceContext)
	  {
	  }

	  public virtual string CamundaKey
	  {
		  get { return CamundaKeyAttribute.GetValue<String>(this); }
		  set { CamundaKeyAttribute.SetValue(this, value); }
	  }


	}

}