﻿
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ESS.FW.Bpm.Engine.Impl.Javax.EL
{
    /// <summary>
    ///     Defines property resolution behavior on arrays. This resolver handles base objects that are Java
    ///     language arrays. It accepts any object as a property and coerces that object into an integer
    ///     index into the array. The resulting value is the value in the array at that index. This resolver
    ///     can be constructed in read-only mode, which means that isReadOnly will always return true and
    ///     <seealso cref="#setValue(ELContext, Object, Object, Object)" /> will always throw
    ///     PropertyNotWritableException. ELResolvers are combined together using <seealso cref="CompositeELResolver" />
    ///     s, to define rich semantics for evaluating an expression. See the javadocs for <seealso cref="ELResolver" />
    ///     for details.
    /// </summary>
    public class ArrayELResolver : ELResolver
    {
        private readonly bool _readOnly;

        /// <summary>
        ///     Creates a new read/write ArrayELResolver.
        /// </summary>
        public ArrayELResolver() : this(false)
        {
        }

        /// <summary>
        ///     Creates a new ArrayELResolver whose read-only status is determined by the given parameter.
        /// </summary>
        /// <param name="readOnly">
        ///     true if this resolver cannot modify arrays; false otherwise.
        /// </param>
        public ArrayELResolver(bool readOnly)
        {
            this._readOnly = readOnly;
        }

        /// <summary>
        ///     If the base object is a Java language array, returns the most general type that this resolver
        ///     accepts for the property argument. Otherwise, returns null. Assuming the base is an array,
        ///     this method will always return Integer.class. This is because arrays accept integers for
        ///     their index.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The array to analyze. Only bases that are a Java language array are handled by
        ///     this resolver.
        /// </param>
        /// <returns> null if base is not a Java language array; otherwise Integer.class. </returns>
        public override Type GetCommonPropertyType(ELContext context, object @base)
        {
            return IsResolvable(@base) ? typeof(int) : null;
        }
        public override IEnumerator<MemberInfo> GetMemberInfos(ELContext context, Object @base)
        {
            return null;
        }
        /// <summary>
        ///     Always returns null, since there is no reason to iterate through set set of all integers. The
        ///     getCommonPropertyType(ELContext, Object)8 method returns sufficient information about what
        ///     properties this resolver accepts.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The array to analyze. Only bases that are a Java language array are handled by
        ///     this resolver.
        /// </param>
        /// <returns> null. </returns>
        /// <summary>
        ///     If the base object is an array, returns the most general acceptable type for a value in this
        ///     array. If the base is a array, the propertyResolved property of the ELContext object must be
        ///     set to true by this resolver, before returning. If this property is not true after this
        ///     method is called, the caller should ignore the return value. Assuming the base is an array,
        ///     this method will always return base.getClass().getComponentType(), which is the most general
        ///     type of component that can be stored at any given index in the array.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The array to analyze. Only bases that are a Java language array are handled by
        ///     this resolver.
        /// </param>
        /// <param name="property">
        ///     The index of the element in the array to return the acceptable type for. Will be
        ///     coerced into an integer, but otherwise ignored by this resolver.
        /// </param>
        /// <returns>
        ///     If the propertyResolved property of ELContext was set to true, then the most general
        ///     acceptable type; otherwise undefined.
        /// </returns>
        /// <exception cref="PropertyNotFoundException">
        ///     if the given index is out of bounds for this array.
        /// </exception>
        /// <exception cref="NullPointerException">
        ///     if context is null
        /// </exception>
        /// <exception cref="ELException">
        ///     if an exception was thrown while performing the property or variable resolution.
        ///     The thrown exception must be included as the cause property of this exception, if
        ///     available.
        /// </exception>
        public override Type GetType(ELContext context, object @base, object property)
        {
            if (context == null)
                throw new NullReferenceException("context is null");
            Type result = null;
            if (IsResolvable(@base))
            {
                ToIndex(@base, property);
                result = @base.GetType().GetElementType();
                context.PropertyResolved = true;
            }
            return result;
        }

        /// <summary>
        ///     If the base object is a Java language array, returns the value at the given index. The index
        ///     is specified by the property argument, and coerced into an integer. If the coercion could not
        ///     be performed, an IllegalArgumentException is thrown. If the index is out of bounds, null is
        ///     returned. If the base is a Java language array, the propertyResolved property of the
        ///     ELContext object must be set to true by this resolver, before returning. If this property is
        ///     not true after this method is called, the caller should ignore the return value.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The array to analyze. Only bases that are a Java language array are handled by
        ///     this resolver.
        /// </param>
        /// <param name="property">
        ///     The index of the element in the array to return the acceptable type for. Will be
        ///     coerced into an integer, but otherwise ignored by this resolver.
        /// </param>
        /// <returns>
        ///     If the propertyResolved property of ELContext was set to true, then the value at the
        ///     given index or null if the index was out of bounds. Otherwise, undefined.
        /// </returns>
        /// <exception cref="PropertyNotFoundException">
        ///     if the given index is out of bounds for this array.
        /// </exception>
        /// <exception cref="NullPointerException">
        ///     if context is null
        /// </exception>
        /// <exception cref="ELException">
        ///     if an exception was thrown while performing the property or variable resolution.
        ///     The thrown exception must be included as the cause property of this exception, if
        ///     available.
        /// </exception>
        public override object GetValue(ELContext context, object @base, object property)
        {
            if (context == null)
                throw new NullReferenceException("context is null");
            object result = null;
            if (IsResolvable(@base))
            {
                var index = ToIndex(null, property);
                var array = (Array) @base;
                //result = index < 0 || index >= Array.getLength(@base) ? null : Array.get(@base, index);
                result = index < 0 || index >= array.Length ? null : array.GetValue(index);
                context.PropertyResolved = true;
            }
            return result;
        }

        /// <summary>
        ///     If the base object is a Java language array, returns whether a call to
        ///     <seealso cref="#setValue(ELContext, Object, Object, Object)" /> will always fail. If the base is a Java
        ///     language array, the propertyResolved property of the ELContext object must be set to true by
        ///     this resolver, before returning. If this property is not true after this method is called,
        ///     the caller should ignore the return value. If this resolver was constructed in read-only
        ///     mode, this method will always return true. Otherwise, it returns false.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The array to analyze. Only bases that are a Java language array are handled by
        ///     this resolver.
        /// </param>
        /// <param name="property">
        ///     The index of the element in the array to return the acceptable type for. Will be
        ///     coerced into an integer, but otherwise ignored by this resolver.
        /// </param>
        /// <returns>
        ///     If the propertyResolved property of ELContext was set to true, then true if calling
        ///     the setValue method will always fail or false if it is possible that such a call may
        ///     succeed; otherwise undefined.
        /// </returns>
        /// <exception cref="PropertyNotFoundException">
        ///     if the given index is out of bounds for this array.
        /// </exception>
        /// <exception cref="NullPointerException">
        ///     if context is null
        /// </exception>
        /// <exception cref="ELException">
        ///     if an exception was thrown while performing the property or variable resolution.
        ///     The thrown exception must be included as the cause property of this exception, if
        ///     available.
        /// </exception>
        public override bool IsReadOnly(ELContext context, object @base, object property)
        {
            if (context == null)
                throw new NullReferenceException("context is null");
            if (IsResolvable(@base))
            {
                ToIndex(@base, property);
                context.PropertyResolved = true;
            }
            return _readOnly;
        }

        /// <summary>
        ///     If the base object is a Java language array, attempts to set the value at the given index
        ///     with the given value. The index is specified by the property argument, and coerced into an
        ///     integer. If the coercion could not be performed, an IllegalArgumentException is thrown. If
        ///     the index is out of bounds, a PropertyNotFoundException is thrown. If the base is a Java
        ///     language array, the propertyResolved property of the ELContext object must be set to true by
        ///     this resolver, before returning. If this property is not true after this method is called,
        ///     the caller can safely assume no value was set. If this resolver was constructed in read-only
        ///     mode, this method will always throw PropertyNotWritableException.
        /// </summary>
        /// <param name="context">
        ///     The context of this evaluation.
        /// </param>
        /// <param name="base">
        ///     The array to analyze. Only bases that are a Java language array are handled by
        ///     this resolver.
        /// </param>
        /// <param name="property">
        ///     The index of the element in the array to return the acceptable type for. Will be
        ///     coerced into an integer, but otherwise ignored by this resolver.
        /// </param>
        /// <param name="value">
        ///     The value to be set at the given index.
        /// </param>
        /// <exception cref="PropertyNotFoundException">
        ///     if the given index is out of bounds for this array.
        /// </exception>
        /// <exception cref="ClassCastException">
        ///     if the class of the specified element prevents it from being added to this array.
        /// </exception>
        /// <exception cref="NullPointerException">
        ///     if context is null
        /// </exception>
        /// <exception cref="IllegalArgumentException">
        ///     if the property could not be coerced into an integer, or if some aspect of the
        ///     specified element prevents it from being added to this array.
        /// </exception>
        /// <exception cref="PropertyNotWritableException">
        ///     if this resolver was constructed in read-only mode.
        /// </exception>
        /// <exception cref="ELException">
        ///     if an exception was thrown while performing the property or variable resolution.
        ///     The thrown exception must be included as the cause property of this exception, if
        ///     available.
        /// </exception>
        public override void SetValue(ELContext context, object @base, object property, object value)
        {
            if (context == null)
                throw new NullReferenceException("context is null");
            if (IsResolvable(@base))
            {
                if (_readOnly)
                    throw new PropertyNotWritableException("resolver is read-only");
                ((Array) @base).SetValue(value, ToIndex(@base, property));
                context.PropertyResolved = true;
            }
        }

        /// <summary>
        ///     Test whether the given base should be resolved by this ELResolver.
        /// </summary>
        /// <param name="base">
        ///     The bean to analyze.
        /// </param>
        /// <param name="property">
        ///     The name of the property to analyze. Will be coerced to a String.
        /// </param>
        /// <returns> base != null && base.getClass().isArray() </returns>
        private bool IsResolvable(object @base)
        {
            return (@base != null) && @base.GetType().IsArray;
        }

        /// <summary>
        ///     Convert the given property to an index in (array) base.
        /// </summary>
        /// <param name="base">
        ///     The bean to analyze.
        /// </param>
        /// <param name="property">
        ///     The name of the property to analyze. Will be coerced to a String.
        /// </param>
        /// <returns> The index of property in base. </returns>
        /// <exception cref="IllegalArgumentException">
        ///     if base property cannot be coerced to an integer or base is not an array.
        /// </exception>
        /// <exception cref="PropertyNotFoundException">
        ///     if the computed index is out of bounds for base.
        /// </exception>
        private int ToIndex(object @base, object property)
        {
            var index = 0;
            if (property is decimal || property is int || property is long)
                index = (int) (decimal) property;
            else if (property is string)
                try
                {
                    index = Convert.ToInt32((string) property);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Cannot parse array index: " + property);
                }
            else if (property is char?)
                index = ((char?) property).Value;
            else if (property is bool?)
                index = ((bool?) property).Value ? 1 : 0;
            else
                throw new ArgumentException("Cannot coerce property to array index: " + property);
            //if (@base != null && (index < 0 || index >= Array.getLength(@base)))
            //{
            //	throw new PropertyNotFoundException("Array index out of bounds: " + index);
            //}
            return index;
        }
    }
}