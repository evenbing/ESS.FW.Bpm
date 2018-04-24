﻿

using System;
using System.Collections.Generic;
using System.Reflection;

namespace ESS.FW.Bpm.Engine.Impl.Javax.EL
{
    /// <summary>
    ///     Enables customization of variable and property resolution behavior for EL expression evaluation.
    ///     While evaluating an expression, the ELResolver associated with the ELContext is consulted to do
    ///     the initial resolution of the first variable of an expression. It is also consulted when a . or
    ///     [] operator is encountered, except for the last such operator in a method expression, in which
    ///     case the resolution rules are hard coded.
    ///     For example, in the EL expression ${employee.lastName}, the ELResolver determines what object
    ///     employee refers to, and what it means to get the lastName property on that object.
    ///     Most methods in this class accept a base and property parameter. In the case of variable
    ///     resolution (e.g. determining what employee refers to in ${employee.lastName}), the base parameter
    ///     will be null and the property parameter will always be of type String. In this case, if the
    ///     property is not a String, the behavior of the ELResolver is undefined.
    ///     In the case of property resolution, the base parameter identifies the base object and the
    ///     property object identifies the property on that base. For example, in the expression
    ///     ${employee.lastName}, base is the result of the variable resolution for employee and property is
    ///     the string "lastName". In the expression ${y[x]}, base is the result of the variable resolution
    ///     for y and property is the result of the variable resolution for x.
    ///     Though only a single ELResolver is associated with an ELContext, there are usually multiple
    ///     resolvers considered for any given variable or property resolution. ELResolvers are combined
    ///     together using <seealso cref="CompositeELResolver" />s, to define rich semantics for evaluating an
    ///     expression. For the <seealso cref=".getValue(ELContext, Object, Object)" />,
    ///     <seealso cref=".getType(ELContext, Object, Object)" />,
    ///     <seealso cref=".setValue(ELContext, Object, Object, Object)" />
    ///     and <seealso cref=".isReadOnly(ELContext, Object, Object)" /> methods, an ELResolver is not responsible for
    ///     resolving all possible (base, property) pairs. In fact, most resolvers will only handle a base of
    ///     a single type. To indicate that a resolver has successfully resolved a particular (base,
    ///     property) pair, it must set the propertyResolved property of the ELContext to true. If it could
    ///     not handle the given pair, it must leave this property alone. The caller must ignore the return
    ///     value of the method if propertyResolved is false.
    ///     The <seealso cref=".getMemberInfos(ELContext, Object)" /> and
    ///     <seealso cref=".getCommonPropertyType(ELContext, Object)" /> methods are primarily designed for design-time
    ///     tool support, but must handle invocation at runtime as well. The java.beans.Beans.isDesignTime()
    ///     method can be used to determine if the resolver is being consulted at design-time or runtime.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public abstract class ELResolver
    {
        /// <summary>
        ///     The attribute name of the named attribute in the MemberDescriptor that specifies whether the
        ///     variable or property can be resolved at runtime.
        /// </summary>
        public const string ResolvableAtDesignTime = "resolvableAtDesignTime";

        /// <summary>
        ///     The attribute name of the named attribute in the MemberDescriptor that specifies the runtime
        ///     type of the variable or property.
        /// </summary>
        public const string Type = "type";

        /// <summary>
        ///     Returns the most general type that this resolver accepts for the property argument, given a
        ///     base object. One use for this method is to assist tools in auto-completion. This assists
        ///     tools in auto-completion and also provides a way to express that the resolver accepts a
        ///     primitive value, such as an integer index into an array. For example, the
        ///     <seealso cref="ArrayELResolver" /> will accept any int as a property, so the return value would be
        ///     Integer.class.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The base object to return the most general property type for, or null to enumerate
        ///     the set of top-level variables that this resolver can evaluate.
        /// </param>
        /// <returns>
        ///     null if this ELResolver does not know how to handle the given base object; otherwise
        ///     Object.class if any type of property is accepted; otherwise the most general property
        ///     type accepted for the given base.
        /// </returns>
        public abstract Type GetCommonPropertyType(ELContext context, object @base);

        /// <summary>
        /// Returns information about the set of variables or properties that can be resolved for the
        /// given base object. One use for this method is to assist tools in auto-completion. If the base
        /// parameter is null, the resolver must enumerate the list of top-level variables it can
        /// resolve. The Iterator returned must contain zero or more instances of
        /// java.beans.FeatureDescriptor, in no guaranteed order. In the case of primitive types such as
        /// int, the value null must be returned. This is to prevent the useless iteration through all
        /// possible primitive values. A return value of null indicates that this resolver does not
        /// handle the given base object or that the results are too complex to represent with this
        /// method and the <seealso cref=".getCommonPropertyType(ELContext, Object)"/> method should be used
        /// instead. Each FeatureDescriptor will contain information about a single variable or property.
        /// In addition to the standard properties, the FeatureDescriptor must have two named attributes
        /// (as set by the setValue method):
        /// <ul>
        /// <li><seealso cref=".TYPE"/> - The value of this named attribute must be an instance of java.lang.Class
        /// and specify the runtime type of the variable or property.</li>
        /// <li><seealso cref=".RESOLVABLE_AT_DESIGN_TIME"/> - The value of this named attribute must be an
        /// instance of java.lang.Boolean and indicates whether it is safe to attempt to resolve this
        /// property at designtime. For instance, it may be unsafe to attempt a resolution at design time
        /// if the ELResolver needs access to a resource that is only available at runtime and no
        /// acceptable simulated value can be provided.</li>
        /// </ul>
        /// The caller should be aware that the Iterator returned might iterate through a very large or
        /// even infinitely large set of properties. Care should be taken by the caller to not get stuck
        /// in an infinite loop. This is a "best-effort" list. Not all ELResolvers will return completely
        /// accurate results, but all must be callable at both design-time and runtime (i.e. whether or
        /// not Beans.isDesignTime() returns true), without causing errors. The propertyResolved property
        /// of the ELContext is not relevant to this method. The results of all ELResolvers are
        /// concatenated in the case of composite resolvers.
        /// </summary>
        /// <param name="context">
        ///            The context of this evaluation. </param>
        /// <param name="base">
        ///            The base object whose set of valid properties is to be enumerated, or null to
        ///            enumerate the set of top-level variables that this resolver can evaluate. </param>
        /// <returns> An Iterator containing zero or more (possibly infinitely more) FeatureDescriptor
        ///         objects, or null if this resolver does not handle the given base object or that the
        ///         results are too complex to represent with this method </returns>
        public abstract IEnumerator<MemberInfo> GetMemberInfos(ELContext context, object @base);

        /// <summary>
        ///     Returns information about the set of variables or properties that can be resolved for the
        ///     given base object. One use for this method is to assist tools in auto-completion. If the base
        ///     parameter is null, the resolver must enumerate the list of top-level variables it can
        ///     resolve. The Iterator returned must contain zero or more instances of
        ///     java.beans.MemberDescriptor, in no guaranteed order. In the case of primitive types such as
        ///     int, the value null must be returned. This is to prevent the useless iteration through all
        ///     possible primitive values. A return value of null indicates that this resolver does not
        ///     handle the given base object or that the results are too complex to represent with this
        ///     method and the <seealso cref=".getCommonPropertyType(ELContext, Object)" /> method should be used
        ///     instead. Each MemberDescriptor will contain information about a single variable or property.
        ///     In addition to the standard properties, the MemberDescriptor must have two named attributes
        ///     (as set by the setValue method):
        ///     <ul>
        ///         <li>
        ///             <seealso cref=".TYPE" /> - The value of this named attribute must be an instance of java.lang.Class
        ///             and specify the runtime type of the variable or property.
        ///         </li>
        ///         <li>
        ///             <seealso cref=".RESOLVABLE_AT_DESIGN_TIME" /> - The value of this named attribute must be an
        ///             instance of java.lang.Boolean and indicates whether it is safe to attempt to resolve this
        ///             property at designtime. For instance, it may be unsafe to attempt a resolution at design time
        ///             if the ELResolver needs access to a resource that is only available at runtime and no
        ///             acceptable simulated value can be provided.
        ///         </li>
        ///     </ul>
        ///     The caller should be aware that the Iterator returned might iterate through a very large or
        ///     even infinitely large set of properties. Care should be taken by the caller to not get stuck
        ///     in an infinite loop. This is a "best-effort" list. Not all ELResolvers will return completely
        ///     accurate results, but all must be callable at both design-time and runtime (i.e. whether or
        ///     not Beans.isDesignTime() returns true), without causing errors. The propertyResolved property
        ///     of the ELContext is not relevant to this method. The results of all ELResolvers are
        ///     concatenated in the case of composite resolvers.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The base object whose set of valid properties is to be enumerated, or null to
        ///     enumerate the set of top-level variables that this resolver can evaluate.
        /// </param>
        /// <returns>
        ///     An Iterator containing zero or more (possibly infinitely more) MemberDescriptor
        ///     objects, or null if this resolver does not handle the given base object or that the
        ///     results are too complex to represent with this method
        /// </returns>
        /// <summary>
        ///     For a given base and property, attempts to identify the most general type that is acceptable
        ///     for an object to be passed as the value parameter in a future call to the
        ///     <seealso cref=".setValue(ELContext, Object, Object, Object)" /> method. If this resolver handles the
        ///     given (base, property) pair, the propertyResolved property of the ELContext object must be
        ///     set to true by the resolver, before returning. If this property is not true after this method
        ///     is called, the caller should ignore the return value. This is not always the same as
        ///     getValue().getClass(). For example, in the case of an <seealso cref="ArrayELResolver" />, the getType
        ///     method will return the element type of the array, which might be a superclass of the type of
        ///     the actual element that is currently in the specified array element.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The base object whose property value is to be analyzed, or null to analyze a
        ///     top-level variable.
        /// </param>
        /// <param name="property">
        ///     The property or variable to return the acceptable type for.
        /// </param>
        /// <returns>
        ///     If the propertyResolved property of ELContext was set to true, then the most general
        ///     acceptable type; otherwise undefined.
        /// </returns>
        /// <exception cref="java.lang.NullPointerException">
        ///     if context is null
        /// </exception>
        /// <exception cref="PropertyNotFoundException">
        ///     if the given (base, property) pair is handled by this ELResolver but the
        ///     specified variable or property does not exist or is not readable.
        /// </exception>
        /// <exception cref="ELException">
        ///     if an exception was thrown while performing the property or variable resolution.
        ///     The thrown exception must be included as the cause property of this exception, if
        ///     available.
        /// </exception>
        public abstract Type GetType(ELContext context, object @base, object property);

        /// <summary>
        ///     Attempts to resolve the given property object on the given base object. If this resolver
        ///     handles the given (base, property) pair, the propertyResolved property of the ELContext
        ///     object must be set to true by the resolver, before returning. If this property is not true
        ///     after this method is called, the caller should ignore the return value.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The base object whose property value is to be returned, or null to resolve a
        ///     top-level variable.
        /// </param>
        /// <param name="property">
        ///     The property or variable to be resolved.
        /// </param>
        /// <returns>
        ///     If the propertyResolved property of ELContext was set to true, then the result of the
        ///     variable or property resolution; otherwise undefined.
        /// </returns>
        /// <exception cref="NullPointerException">
        ///     if context is null
        /// </exception>
        /// <exception cref="PropertyNotFoundException">
        ///     if the given (base, property) pair is handled by this ELResolver but the
        ///     specified variable or property does not exist or is not readable.
        /// </exception>
        /// <exception cref="ELException">
        ///     if an exception was thrown while performing the property or variable resolution.
        ///     The thrown exception must be included as the cause property of this exception, if
        ///     available.
        /// </exception>
        public abstract object GetValue(ELContext context, object @base, object property);

        /// <summary>
        ///     For a given base and property, attempts to determine whether a call to
        ///     <seealso cref=".setValue(ELContext, Object, Object, Object)" /> will always fail. If this resolver
        ///     handles the given (base, property) pair, the propertyResolved property of the ELContext
        ///     object must be set to true by the resolver, before returning. If this property is not true
        ///     after this method is called, the caller should ignore the return value.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The base object whose property value is to be analyzed, or null to analyze a
        ///     top-level variable.
        /// </param>
        /// <param name="property">
        ///     The property or variable to return the read-only status for.
        /// </param>
        /// <returns>
        ///     If the propertyResolved property of ELContext was set to true, then true if the
        ///     property is read-only or false if not; otherwise undefined.
        /// </returns>
        /// <exception cref="NullPointerException">
        ///     if context is null
        /// </exception>
        /// <exception cref="PropertyNotFoundException">
        ///     if the given (base, property) pair is handled by this ELResolver but the
        ///     specified variable or property does not exist.
        /// </exception>
        /// <exception cref="ELException">
        ///     if an exception was thrown while performing the property or variable resolution.
        ///     The thrown exception must be included as the cause property of this exception, if
        ///     available.
        /// </exception>
        public abstract bool IsReadOnly(ELContext context, object @base, object property);

        /// <summary>
        ///     Attempts to set the value of the given property object on the given base object. If this
        ///     resolver handles the given (base, property) pair, the propertyResolved property of the
        ///     ELContext object must be set to true by the resolver, before returning. If this property is
        ///     not true after this method is called, the caller can safely assume no value has been set.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The base object whose property value is to be set, or null to set a top-level
        ///     variable.
        /// </param>
        /// <param name="property">
        ///     The property or variable to be set.
        /// </param>
        /// <param name="value">
        ///     The value to set the property or variable to.
        /// </param>
        /// <exception cref="NullPointerException">
        ///     if context is null
        /// </exception>
        /// <exception cref="PropertyNotFoundException">
        ///     if the given (base, property) pair is handled by this ELResolver but the
        ///     specified variable or property does not exist.
        /// </exception>
        /// <exception cref="PropertyNotWritableException">
        ///     if the given (base, property) pair is handled by this ELResolver but the
        ///     specified variable or property is not writable.
        /// </exception>
        /// <exception cref="ELException">
        ///     if an exception was thrown while attempting to set the property or variable. The
        ///     thrown exception must be included as the cause property of this exception, if
        ///     available.
        /// </exception>
        public abstract void SetValue(ELContext context, object @base, object property, object value);

        /// <summary>
        ///     Attempts to resolve and invoke the given <code>method</code> on the given <code>base</code>
        ///     object.
        ///     <para>
        ///         If this resolver handles the given (base, method) pair, the <code>propertyResolved</code>
        ///         property of the <code>ELContext</code> object must be set to <code>true</code> by the
        ///         resolver, before returning. If this property is not <code>true</code> after this method is
        ///         called, the caller should ignore the return value.
        ///     </para>
        ///     <para>
        ///         A default implementation is provided that returns null so that existing classes that extend
        ///         ELResolver can continue to function.
        ///     </para>
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The bean on which to invoke the method
        /// </param>
        /// <param name="method">
        ///     The simple name of the method to invoke. Will be coerced to a <code>String</code>.
        /// </param>
        /// <param name="paramTypes">
        ///     An array of Class objects identifying the method's formal parameter types, in
        ///     declared order. Use an empty array if the method has no parameters. Can be
        ///     <code>null</code>, in which case the method's formal parameter types are assumed
        ///     to be unknown.
        /// </param>
        /// <param name="params">
        ///     The parameters to pass to the method, or <code>null</code> if no parameters.
        /// </param>
        /// <returns>
        ///     The result of the method invocation (<code>null</code> if the method has a
        ///     <code>void</code> return type).
        /// </returns>
        /// <exception cref="MethodNotFoundException">
        ///     if no suitable method can be found.
        /// </exception>
        /// <exception cref="ELException">
        ///     if an exception was thrown while performing (base, method) resolution. The thrown
        ///     exception must be included as the cause property of this exception, if available.
        ///     If the exception thrown is an <code>TargetInvocationException</code>, extract its
        ///     <code>cause</code> and pass it to the <code>ELException</code> constructor.
        /// </exception>
        public virtual object Invoke(ELContext context, object @base, object method, Type[] paramTypes, object[] @params)
        {
            return null;
        }
    }
}