﻿

using ESS.FW.Bpm.Model.Dmn.instance;
using ESS.FW.Bpm.Model.Xml;
using ESS.FW.Bpm.Model.Xml.impl.instance;
using ESS.FW.Bpm.Model.Xml.type;

namespace ESS.FW.Bpm.Model.Dmn.impl.instance
{

    //JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
    //	import static org.camunda.bpm.model.dmn.impl.DmnModelConstants.DMN11_NS;
    //JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
    //	import static org.camunda.bpm.model.dmn.impl.DmnModelConstants.DMN_ELEMENT_DRG_ELEMENT;

    using ModelBuilder = ModelBuilder;
    using ModelTypeInstanceContext = ModelTypeInstanceContext;

    public abstract class DrgElementImpl : NamedElementImpl, IDrgElement
	{

	  public DrgElementImpl(ModelTypeInstanceContext instanceContext) : base(instanceContext)
	  {
	  }

	  public static void RegisterType(ModelBuilder modelBuilder)
	  {
		IModelElementTypeBuilder typeBuilder = modelBuilder.DefineType<IDrgElement>(/*typeof(IDrgElement),*/ DmnModelConstants.DmnElementDrgElement).NamespaceUri(DmnModelConstants.Dmn11Ns).ExtendsType(typeof(INamedElement)).AbstractType();

		typeBuilder.Build();
	  }

	}

}